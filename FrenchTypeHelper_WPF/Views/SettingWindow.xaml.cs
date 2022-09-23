using System;
using System.Windows;
using FrenchTypeHelper_WPF.Views.SettingPage;
using Gma.System.MouseKeyHook.HotKeys;
using HandyControl.Controls;
using HandyControl.Data;
using Window = System.Windows.Window;

namespace FrenchTypeHelper_WPF.Views
{
    public partial class SettingWindow : Window
    {
        private MainWindow _mainWindow;

        private SystemPage _systemPage;
        private HotKeyPage _hotkeyPage;
        private MappingPage _mappingPage;
        private InterruptPage _interruptPage;
        
        public SettingWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            _systemPage = new SystemPage();
            _hotkeyPage = new HotKeyPage();
            _mappingPage = new MappingPage();
            _interruptPage = new InterruptPage();
            
            InitializeComponent();
            SystemSideMenuItem.IsSelected = true;
            SwitchPage(SystemSideMenuItem);
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void SwitchPage(SideMenuItem item)
        {
            if (item == SystemSideMenuItem) SettingFrame.Content = _systemPage;
            else if(item == HotkeySideMenuItem) SettingFrame.Content = _hotkeyPage;
            else if(item == MappingSideMenuItem) SettingFrame.Content = _mappingPage;
            else if(item == InterruptSideMenuItem) SettingFrame.Content = _interruptPage;
        }

        private void SideMenu_OnSelectionChanged(object sender, FunctionEventArgs<object> e)
        {
            SwitchPage((SideMenuItem)e.Info);
        }
    }
}