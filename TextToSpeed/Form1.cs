using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Globalization;
using System.Speech.AudioFormat;
using System.IO;

namespace TextToSpeed
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTextToSpeed_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in rtxtText.Lines)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string text = item.Trim();

                        using (SpeechSynthesizer synth = new SpeechSynthesizer())
                        {
                            synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.NotSet, 0, CultureInfo.GetCultureInfo(txtLangCode.Text));
                            synth.Rate = int.Parse(txtSpeed.Text);
                            // Configure the audio output.
                            synth.SetOutputToWaveFile(Application.StartupPath + @"\mp3\"+ text + ".wav",
                              new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));

                            // Create a SoundPlayer instance to play output audio file.
                            System.Media.SoundPlayer m_SoundPlayer =
                              new System.Media.SoundPlayer(Application.StartupPath + @"\mp3\" + text + ".wav");

                            // Build a prompt.
                            PromptBuilder builder = new PromptBuilder();
                            builder.AppendText(text);

                            // Speak the prompt.
                            synth.Speak(builder);
                            m_SoundPlayer.Play();
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }
}
