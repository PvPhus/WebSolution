using ImageResizer;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace WebSolution.WebHelper
{
    public static class ImageProcess
    {
        public static string Crop(string path, string originPath, string pathThumb, string pathTemp, string imageName, int x, int y, int w, int h, string community)
        {
            GC.Collect();
            string[] arr1 = imageName.Split('/');
            string oldfilename = arr1[arr1.Length - 1];
            imageName = imageName.Remove(0, 1).Replace("/", "\\");
            string creatdate = DateTime.Now.ToString("yyyyMMddhhmmss");
            string Fromfile = path + imageName;
            string filename = community;
            string[] arr = oldfilename.Split('.');
            filename += ("." + arr[arr.Length - 1]);
            filename = creatdate + "_" + filename;
            if (Fromfile != originPath + filename)
                File.Copy(Fromfile, originPath + filename, true);
            //Save Origin Image
            string fileOrigin = originPath + filename;
            string fileCropResize = pathThumb + filename;
            string tempFile = pathTemp + "temp_" + filename;
            //Get Cropped Image
            Image cropImage = Image.FromFile(fileOrigin);
            ImageFormat fmtImageFormat = ImageFormat.Jpeg;
            var bmp = new Bitmap(w, h, cropImage.PixelFormat);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(cropImage, new Rectangle(0, 0, w, h), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
            bmp.Save(tempFile, fmtImageFormat);
            cropImage.Dispose();
            bmp.Dispose();
            g.Dispose();
            GC.Collect();
            if (File.Exists(tempFile))
            {
                ResizeFromStream(fileCropResize, 205,
                                      File.Open(tempFile, FileMode.Open, FileAccess.ReadWrite));
                File.Delete(tempFile);
            }
            return filename;
        }

        public static ImageDimension ResizeFromStream(string imageSavePath, int maxSideSize, Stream buffer)
        {
            try
            {
                var dimension = new ImageDimension();
                using (Image imgInput = Image.FromStream(buffer))
                {
                    if (maxSideSize > imgInput.Width && maxSideSize > imgInput.Height) //not resize if image is small
                    {
                        dimension.Width = imgInput.Width;
                        dimension.Height = imgInput.Height;
                    }
                    else
                    {
                        float ratio = ((float)imgInput.Width) / imgInput.Height;
                        if (ratio > 1)
                        {
                            dimension.Width = maxSideSize;
                            dimension.Height = (int)(maxSideSize / ratio);
                        }
                        else
                        {
                            dimension.Width = (int)(maxSideSize * ratio);
                            dimension.Height = maxSideSize;
                        }
                    }

                    var i = new ImageJob(imgInput, imageSavePath, new Instructions(
                                                                                 "maxwidth=" + maxSideSize + ";maxheight=" + maxSideSize + ";mode=max"));
                    i.CreateParentDirectory = true; //Auto-create the uploads directory.
                    i.Build();
                }
                return dimension;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static void ResizeFromFile(string imageSavePath, int maxSideSize, string filePath)
        {
            using (Image original = Image.FromFile(filePath))
            {
                ImageFormat rawFormat = original.RawFormat;
                int width = original.Width;
                int height = original.Height;
                int maxsize;
                if (width >= height)
                    maxsize = width;
                else
                    maxsize = height;
                int newHeight;
                int newWidth;
                if (maxsize > maxSideSize)
                {
                    double ratio = ((double)maxSideSize) / ((double)maxsize);
                    newWidth = Convert.ToInt32((double)(ratio * width));
                    newHeight = Convert.ToInt32((double)(ratio * height));
                }
                else
                {
                    newWidth = width;
                    newHeight = height;
                }
                using (var bitmap = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb))
                {
                    try
                    {
                        using (Graphics gr = Graphics.FromImage(bitmap))
                        {
                            gr.Clear(Color.Transparent);
                            // This is said to give best quality when resizing images
                            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gr.DrawImage(original,
                                         new Rectangle(0, 0, newWidth, newHeight),
                                         new Rectangle(0, 0, original.Width, original.Height),
                                         GraphicsUnit.Pixel);
                        }
                        bitmap.Save(imageSavePath, rawFormat);
                        bitmap.Save(imageSavePath, rawFormat);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public class ImageDimension
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public static ImageDimension ResizeFromStreamFixWidth(string imageSavePath, int width, Stream buffer)
        {
            try
            {
                var dimension = new ImageDimension();
                using (Image imgInput = Image.FromStream(buffer))
                {
                    if (width > imgInput.Width) //not resize if image is small
                    {
                        dimension.Width = imgInput.Width;
                        dimension.Height = imgInput.Height;
                    }
                    else
                    {
                        dimension.Width = width;
                        dimension.Height = (int)(imgInput.Height * ((double)dimension.Width) / imgInput.Width);
                    }
                    var i = new ImageResizer.ImageJob(imgInput, imageSavePath, new ImageResizer.Instructions(
                                                                                 "maxwidth=" + width + ";mode=max"));
                    i.CreateParentDirectory = true; //Auto-create the uploads directory.
                    i.Build();
                }
                return dimension;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static void ResizeFromFileFixWidth(string imageSavePath, int maxWidth, string filePath)
        {
            using (Image original = Image.FromFile(filePath))
            {
                ImageFormat rawFormat = original.RawFormat;
                int width = original.Width;
                int height = original.Height;
                int maxsize;
                maxsize = width;
                int _height;
                int _width;
                if (maxsize > maxWidth)
                {
                    double ratio = ((double)maxWidth) / ((double)maxsize);
                    _width = Convert.ToInt32((double)(ratio * width));
                    _height = Convert.ToInt32((double)(ratio * height));
                }
                else
                {
                    _width = width;
                    _height = height;
                }
                using (var bitmap = new Bitmap(_width, _height, PixelFormat.Format32bppArgb))
                {
                    try
                    {
                        using (Graphics gr = Graphics.FromImage(bitmap))
                        {
                            gr.Clear(Color.Transparent);
                            // This is said to give best quality when resizing images
                            gr.SmoothingMode = SmoothingMode.HighQuality;
                            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            gr.CompositingQuality = CompositingQuality.HighQuality;
                            gr.DrawImage(original,
                                         new Rectangle(0, 0, _width, _height),
                                         new Rectangle(0, 0, original.Width, original.Height),
                                         GraphicsUnit.Pixel);
                        }
                        bitmap.Save(imageSavePath, rawFormat);
                        bitmap.Save(imageSavePath, rawFormat);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public static int ResizeFromUrl(string dirPath, string filename, int maxsize, string url)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.8) Gecko/20100722 Firefox/3.6.8";
            request.KeepAlive = true;
            request.Timeout = 0x7530;
            string fileExtension = url.ToLower().Substring(url.LastIndexOf("."));
            //if (fileExtension.IndexOf("?", System.StringComparison.Ordinal) > 0)
            //    fileExtension = fileExtension.Remove(fileExtension.IndexOf("?", System.StringComparison.Ordinal));
            //if ((fileExtension != ".jpg") && (fileExtension != ".jpeg") && (fileExtension != ".gif") && (fileExtension != ".png"))
            //{
            //    return -1;//Extension not valid
            //}
            try
            {
                String fullsizepath = System.IO.Path.Combine(dirPath, filename).Replace("\\", "//");
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.ContentLength > 0)
                    {

                        ResizeFromStream(fullsizepath, maxsize, response.GetResponseStream());
                        request.Abort();
                        return 0;
                    }
                    request.Abort();
                }
            }
            catch (Exception e)
            {

            }
            return -1;
        }

        public static Color CalculateAverageColor(Bitmap bm)
        {
            int width = bm.Width;
            int height = bm.Height;
            int red = 0;
            int green = 0;
            int blue = 0;
            int minDiversion = 15; // drop pixels that do not differ by at least minDiversion between color values (white, gray or black)
            int dropped = 0; // keep track of dropped pixels
            var totals = new long[] { 0, 0, 0 };
            int bppModifier = bm.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ? 3 : 4; // cutting corners, will fail on anything else but 32 and 24 bit images

            BitmapData srcData = bm.LockBits(new System.Drawing.Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, bm.PixelFormat);
            int stride = srcData.Stride;
            IntPtr Scan0 = srcData.Scan0;

            //unsafe
            //{
            //    var p = (byte*)(void*)Scan0;

            //    for (int y = 0; y < height; y++)
            //    {
            //        for (int x = 0; x < width; x++)
            //        {
            //            int idx = (y * stride) + x * bppModifier;
            //            red = p[idx + 2];
            //            green = p[idx + 1];
            //            blue = p[idx];
            //            if (Math.Abs(red - green) > minDiversion || Math.Abs(red - blue) > minDiversion || Math.Abs(green - blue) > minDiversion)
            //            {
            //                totals[2] += red;
            //                totals[1] += green;
            //                totals[0] += blue;
            //            }
            //            else
            //            {
            //                dropped++;
            //            }
            //        }
            //    }
            //}

            int count = width * height - dropped;
            int avgR = (int)(totals[2] / count);
            int avgG = (int)(totals[1] / count);
            int avgB = (int)(totals[0] / count);

            return System.Drawing.Color.FromArgb(avgR, avgG, avgB);
        }

        public static ImageDimension ResizeFromStreamFixWidth(string imageSavePath, int width, Stream buffer, out Color color)
        {
            color = new Color();
            try
            {
                var dimension = new ImageDimension();
                using (Image imgInput = Image.FromStream(buffer))
                {
                    if (width > imgInput.Width) //not resize if image is small
                    {
                        dimension.Width = imgInput.Width;
                        dimension.Height = imgInput.Height;
                    }
                    else
                    {
                        dimension.Width = width;
                        dimension.Height = (int)(imgInput.Height * ((double)dimension.Width) / imgInput.Width);
                    }
                    var i = new ImageResizer.ImageJob(imgInput, imageSavePath, new ImageResizer.Instructions(
                                                                                 "maxwidth=" + width + ";mode=max;format=jpg"));
                    i.CreateParentDirectory = true; //Auto-create the uploads directory.
                    i.Build();
                    var bmp = new Bitmap(i.Dest.ToString());
                    color = CalculateAverageColor(bmp);
                }
                return dimension;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ResizeFromStreamMinWidth(string imageSavePath, int minWidth, int maxWidth, Stream buffer)
        {
            try
            {
                using (Image imgInput = Image.FromStream(buffer))
                {
                    if (minWidth > imgInput.Width) //not resize if image is small
                    {
                        return -1;
                    }
                    if (imgInput.Width > maxWidth)
                    {
                        minWidth = maxWidth;
                    }
                    else
                    {
                        minWidth = imgInput.Width;
                    }

                    var i = new ImageResizer.ImageJob(imgInput, imageSavePath, new ImageResizer.Instructions("maxwidth=" + minWidth + ";mode=max;format=jpg"));
                    i.CreateParentDirectory = true; //Auto-create the uploads directory.
                    i.Build();
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static bool IsImage(this HttpPostedFileBase postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }

                //if (postedFile.ContentLength < ImageMinimumBytes)
                //{
                //    return false;
                //}

                byte[] buffer = new byte[512];
                postedFile.InputStream.Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.InputStream.Position = 0;
            }

            return true;
        }
        public static Size ResizeKeepAspect(this Size src, int maxWidth, int maxHeight, bool enlarge = false)
        {
            maxWidth = enlarge ? maxWidth : Math.Min(maxWidth, src.Width);
            maxHeight = enlarge ? maxHeight : Math.Min(maxHeight, src.Height);

            decimal rnd = Math.Min(maxWidth / (decimal)src.Width, maxHeight / (decimal)src.Height);
            return new Size((int)Math.Round(src.Width * rnd), (int)Math.Round(src.Height * rnd));
        }
    }
}