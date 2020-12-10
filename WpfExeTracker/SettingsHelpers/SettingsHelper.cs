using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfExeTracker.SettingsHelpers
{
    static class SettingsHelper
    {
        public static void LoadSettings(ref string folderPath, ref string clientName)
        {
            try
            {
                var pathToSaveTo = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                StreamReader sr = new StreamReader(pathToSaveTo + @"\settings.txt");
                folderPath = sr.ReadLine().Replace("FolderPath: ","");
                clientName = sr.ReadLine().Replace("ClientName: ", ""); ;
                Console.WriteLine("a");
            }
            catch { }

        }
        public static void SaveSetting(string folderPath, string clientName)
        {
            try
            {
                var pathToSaveTo = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                StreamWriter sw = new StreamWriter(pathToSaveTo + @"\settings.txt");
                sw.WriteLine($"FolderPath: {folderPath}");
                sw.WriteLine($"ClientName: {clientName}");
                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
