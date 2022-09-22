

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using FrenchTypeHelper_WPF.KeyOperator;
using Application = System.Windows.Application;

namespace FrenchTypeHelper_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: IHotkeyObserver
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private HotKeyOperator _hotKeyOperator;
        private CoreOperator _coreOperator;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            
            Dictionary<char, Dictionary<char, char>> mapping = new Dictionary<char, Dictionary<char, char>>();

            Dictionary<char, char> dict1 = new Dictionary<char, char>();
            mapping.Add('/', dict1);
            dict1.Add('c', 'ç');
            dict1.Add('C', 'Ç');
            
            Dictionary<char, char> dict2 = new Dictionary<char, char>();
            mapping.Add('[', dict2);
            dict2.Add('a', 'à');
            dict2.Add('A', 'À');
            dict2.Add('e', 'è');
            dict2.Add('E', 'È');
            dict2.Add('u', 'ù');
            dict2.Add('U', 'Ù');
            
            
            Dictionary<char, char> dict3 = new Dictionary<char, char>();
            mapping.Add(']', dict3);
            dict3.Add('a', 'â');
            dict3.Add('A', 'Â');
            dict3.Add('e', 'ê');
            dict3.Add('E', 'Ê');
            dict3.Add('u', 'û');
            dict3.Add('U', 'Û');
            dict3.Add('o', 'ô');
            dict3.Add('O', 'Ô');
            dict3.Add('i', 'î');
            dict3.Add('I', 'Î');
            
            Dictionary<char, char> dict4 = new Dictionary<char, char>();
            mapping.Add('=', dict4);
            dict4.Add('e', 'ë');
            dict4.Add('E', 'Ë');
            dict4.Add('u', 'ü');
            dict4.Add('U', 'Ü');
            dict4.Add('i', 'ï');
            dict4.Add('I', 'Ï');
            
            Dictionary<char, char> dict5 = new Dictionary<char, char>();
            mapping.Add('-', dict5);
            dict5.Add('e', 'é');
            dict5.Add('E', 'É');
            
            Dictionary<char, char> dict6 = new Dictionary<char, char>();
            mapping.Add('<', dict6);
            dict6.Add('<', '«');
            
            
            Dictionary<char, char> dict7 = new Dictionary<char, char>();
            mapping.Add('>', dict7);
            dict7.Add('>', '»');
//c/c/c;c/çe\èêéëa\e\u\«»
            _coreOperator = new CoreOperator(mapping);
            
            _hotKeyOperator = new HotKeyOperator(this);
            _hotKeyOperator.SwitchStatusHotKey = new HotKeyCombination(true, false, false, Keys.OemQuotes);
            
            
            SwitchStatus(true);
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
            }
            else
            {
                StatusMenuItem.Header = "Disabled";
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
            this.Visibility = Visibility.Visible;
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
    }
}