using System;

namespace WebSolution.Data
{
    public class CoreDataConfig
    {

        public static String MongoConnectionString = System.Configuration.ConfigurationManager.AppSettings["MongoConnectionString"];
        public static String DbName = System.Configuration.ConfigurationManager.AppSettings["DBName"];
        public static String DbNameLanguageIndex = System.Configuration.ConfigurationManager.AppSettings["DbNameLanguageIndex"];
        public static int Language = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Language"]);
        public static string Current_Url { get; set; } = "http://localhost:44315";
    }
}