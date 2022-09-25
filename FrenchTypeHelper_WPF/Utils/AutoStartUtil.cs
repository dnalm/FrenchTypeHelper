using HandyControl.Tools.Extension;
using Microsoft.Win32;

namespace FrenchTypeHelper_WPF.Utils
{
    public class AutoStartUtil
    {
        
        public static bool Enabled
        {
            set
            {
                if (value)
                {
                    EnableAutoStart();
                }
                else
                {
                    DisableAutoStart();
                }

            }
        }

        private static void EnableAutoStart()
        {
            var rLocal = Registry.CurrentUser;
            var rRun = rLocal.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            var currPathObj = rRun.GetValue("FrenchTypeHelper");
            if (currPathObj == null || currPathObj.ToString() != System.Windows.Forms.Application.ExecutablePath)
            {
                rRun.SetValue("", System.Windows.Forms.Application.ExecutablePath);
            }
            rRun.Close();
            rLocal.Close();
        }

        private static void DisableAutoStart()
        {
            var rLocal = Registry.CurrentUser;
            var rRun = rLocal.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            
            rRun.DeleteValue("FrenchTypeHelper", false);
            
            rRun.Close();
            rLocal.Close();
        }
    }
}