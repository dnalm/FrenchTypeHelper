using FrenchTypeHelper_WPF.Utils;
using HandyControl.Controls;

namespace FrenchTypeHelper_WPF.Views.SettingPage
{
    public partial class SettingWindow
    {
        private bool _isAutoRun;
        public bool IsAutoRun
        {
            get => _isAutoRun;
            set
            {
                AutoStartUtil.Enabled = value;
                _isAutoRun = value;
            }
        }

        public bool IsEnableWhenStartup;
        
    }
}