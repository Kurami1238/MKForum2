using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MKForum.Helpers
{
    public class Logger
    {
        //private static string _savePath = Path.GetFullPath(@"\Logs\Log.log");
        private const string _savePath = "D:\\CSharpTeamTopic\\Logs\\Log.log";


        //記錄錯誤
        public static void WriteLog(string moduleName, Exception ex)
        {
            string content =
$@"------------------
{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}
                {moduleName}
                {ex.ToString()}
 ----------------------
";

            File.AppendAllText(Logger._savePath, content);
        }

    }
}