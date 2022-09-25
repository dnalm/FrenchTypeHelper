

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FrenchTypeHelper_WPF.KeyOperator;
using FrenchTypeHelper_WPF.Utils;
using FrenchTypeHelper_WPF.Views;
using FrenchTypeHelper_WPF.Views.SettingPage;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace FrenchTypeHelper_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: IHotkeyObserver
    {
        public MainWindow()
        {
            // Forbid multi instance
            var processes = Process.GetProcesses();
            if (processes.Count(p => p.ProcessName.Equals("FrenchTypeHelper_WPF")) > 1)
            {
                MessageBox.Show("FrenchTypeHelper has been running.", "FrenchTypeHelper", MessageBoxButton.OK, MessageBoxImage.Information);
                System.Environment.Exit(1);
            }
            
            InitializeComponent();
        }

        private HotKeyOperator _hotKeyOperator;
        private CoreOperator _coreOperator;

        private SettingWindow _settingWindow;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            
            _settingWindow = new SettingWindow(this);
            _settingWindow.Visibility = Visibility.Hidden;

            _coreOperator = new CoreOperator(_settingWindow);
            
            _hotKeyOperator = new HotKeyOperator(new List<IHotkeyObserver>(new IHotkeyObserver[]{this, _coreOperator}), _settingWindow);
            // _hotKeyOperator.SwitchStatusHotKey = new HotKeyCombination(true, false, false, Keys.OemQuotes);
            
          
            SwitchStatus(_settingWindow.IsEnableWhenStartup);
            _hotKeyOperator.Enabled = true;
        }

        private void SwitchStatus()
        {
            SwitchStatus(!_coreOperator.Enabled);
        }
        
        private void SwitchStatus(bool enable)
        {
            _coreOperator.Enabled = enable;
            if(_coreOperator.Enabled)
            {
                StatusMenuItem.Header = "Enabled";
                // todo
                // StatusMenuItem.Icon = Resource.system_icon_enabled;
                NotifyIcon.Icon = ImageUtil.BitmapToBitmapSource(Resource.system_icon_enabled);
            }
            else
            {
                StatusMenuItem.Header = "Disabled";
                // todo
                // StatusMenuItem.Icon = Resource.system_icon_disabled;
                NotifyIcon.Icon = ImageUtil.BitmapToBitmapSource(Resource.system_icon_disabled);
            }
        }
        

        protected override void OnClosing(CancelEventArgs e)
        {
            // base.OnClosing(e);
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Setting_OnClick(object sender, RoutedEventArgs e)
        {
            _settingWindow.Visibility = Visibility.Visible;
        }

        private void NotifyIcon_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void Status_OnClick(object sender, RoutedEventArgs e)
        {
            SwitchStatus();
        }

        public void NotifySwitchStatus()
        {
            SwitchStatus();
        }

        public void NotifyInterrupt()
        { }
    }
}