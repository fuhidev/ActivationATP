using Microsoft.Win32;
using System;

namespace ActivationATP
{
    class Program
    {
        static void Main(string[] args)
        {
            SetStartup();
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ATP",true);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ATP");
            }
            var guid = generateGuid();
            key.SetValue("ATP-ZALO", "ATP-ZALO-" + guid);
            key.SetValue("ATP-NINJA_PRO", "ATP-NINJA_PRO-" + guid); 
            key.SetValue("ATP-FACEBOOK_PRO", "ATP-FACEBOOK_PRO-" + guid);
            key.Close();
        }

        static string generateGuid()
        {
            string value = "";
            for(var i = 0; i <= 7; i++)
            {
                var guid = Guid.NewGuid();
                var tmp =  guid.ToString().Substring(0, 4).ToUpper();
                value += tmp;
                if(i < 7)
                {
                    value += "-";
                }
            }
            return value;
            
        }

        static void SetStartup()
        {
            string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

            RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(runKey);
            var appName = "ActivationATP";
            if (startupKey.GetValue(appName) == null)
            {
                startupKey.Close();
                startupKey = Registry.CurrentUser.OpenSubKey(runKey, true);
                startupKey.SetValue(appName, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("dll", "exe") + "\"");
                startupKey.Close();
            }

        }
    }
}
