using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using FrenchTypeHelper_WPF.KeyOperator;
using FrenchTypeHelper_WPF.Utils;
using FrenchTypeHelper_WPF.Views.SettingPage;
using Gma.System.MouseKeyHook.HotKeys;
using HandyControl.Controls;
using HandyControl.Data;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = HandyControl.Controls.MessageBox;
using Window = System.Windows.Window;

namespace FrenchTypeHelper_WPF.Views.SettingPage
{
    public partial class SettingWindow : Window
    {
        private MainWindow _mainWindow;

        private bool _initFinished = false;

        public SettingWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            InitializeComponent();
            
            BindSources();
            Load();

            _initFinished = true;
            
            ResetQuickCharAvailable();
            RefreshMapping();
            HotkeyCheckBox_OnChanged(null, null);

        }

        private void BindSources()
        {

            GraveAccentCheckComboBox.ItemsSource = _graveAccentBaseChars;
            GraveQuickComboBox.ItemsSource =  QuickCharAvailableDic;
            GraveQuickComboBox.SelectedIndex = 0;
            
            AcuteAccentCheckComboBox.ItemsSource = _acuteAccentBaseChars;
            AcuteQuickComboBox.ItemsSource =  QuickCharAvailableDic;
            AcuteQuickComboBox.SelectedIndex = 0;
          
            CircumflexAccentCheckComboBox.ItemsSource = _circumflexAccentBaseChars;
            CircumflexQuickComboBox.ItemsSource =  QuickCharAvailableDic;
            CircumflexQuickComboBox.SelectedIndex = 0;
            
            CedillaAccentCheckComboBox.ItemsSource = _cedillaAccentBaseChars;
            CedillaQuickComboBox.ItemsSource =  QuickCharAvailableDic;
            CedillaQuickComboBox.SelectedIndex = 0;
         
            DiaeresisAccentCheckComboBox.ItemsSource = _diaeresisAccentBaseChars;
            DiaeresisQuickComboBox.ItemsSource =  QuickCharAvailableDic;
            DiaeresisQuickComboBox.SelectedIndex = 0;
          
            OpenQuoteQuickComboBox.ItemsSource =  QuickCharAvailableDic;
            OpenQuoteQuickComboBox.SelectedIndex = 0;
            OpenQuoteQuickComboBox.SelectedValue = QuickCharAvailableDic.Where(i => i.Key == '`').First();
            
            CloseQuoteQuickComboBox.ItemsSource =  QuickCharAvailableDic;
            CloseQuoteQuickComboBox.SelectedIndex = 0;

            HotKeyKeyCode.ItemsSource = _keysList;
            
        }

        
        //system
        private void AutoRun_OnChanged(object sender, RoutedEventArgs e)
        {
            if (_initFinished)
            {
                if (AutoRunComBox.IsChecked == true)
                {
                    IsAutoRun = true;
                }
                else
                {
                    IsAutoRun = false;
                }
                SaveConfigs();
            }

        }
        
        private void EnableWhenStartup_OnChanged(object sender, RoutedEventArgs e)
        {
            if (_initFinished)
            {
                if (EnableWhenStartup.IsChecked == true)
                {
                    IsEnableWhenStartup = true;
                }
                else
                {
                    IsEnableWhenStartup = false;
                }
                SaveConfigs();
            }

        }
        
        //hotkey
        private void HotkeyCtrl_OnChanged(object sender, RoutedEventArgs e)
        {
            if (_initFinished)
            {
                Hotkey.Control = (bool)HotkeyCtrl.IsChecked;
                SaveConfigs();
            }
        }
        
        private void HotkeyAlt_OnChanged(object sender, RoutedEventArgs e)
        {
            if (_initFinished)
            {
                Hotkey.Alt = (bool)HotkeyAlt.IsChecked;
                SaveConfigs();
            }
        }
        
        private void HotkeyShift_OnChanged(object sender, RoutedEventArgs e)
        {
            if (_initFinished)
            {
                Hotkey.Shift = (bool)HotkeyShift.IsChecked;
                SaveConfigs();
            }
        }
        
        private void HotKeyKeyCode_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_initFinished)
            {
                Hotkey.KeyStr = (string)HotKeyKeyCode.SelectedItem;
                // Keys k;
                // Keys.TryParse((string)HotKeyKeyCode.SelectedItem, out k);
                // Hotkey.KeyCode = k;
                SaveConfigs();
            }
        }
        
        private void HotkeyCheckBox_OnChanged(object sender, RoutedEventArgs e)
        {
            IsHotkeyEnable = (bool)HotkeyCheckBox.IsChecked;
            HotkeyCtrl.IsEnabled = IsHotkeyEnable;
            HotkeyAlt.IsEnabled = IsHotkeyEnable;
            HotkeyShift.IsEnabled = IsHotkeyEnable;
            HotKeyKeyCode.IsEnabled = IsHotkeyEnable;
        }
        
        // mapping
        
        private void Mapping_OnChanged(object sender, RoutedEventArgs e)
        {
            if (_initFinished)
            {
                RefreshMapping();
                SaveConfigs();
            }
            
        }
        
        private void MappingQuickComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            if (_initFinished)
            {
                ResetQuickCharAvailable();
                RefreshMapping();
                SaveConfigs();
            }

        }

        // Interrupt
        //todo
        
       // exit button
       private void Exit_OnClick(object sender, RoutedEventArgs e) 
       {
           this.Visibility = Visibility.Hidden;
       }



    }

    public enum AccentEnum
    {
        GRAVE, ACUTE, CIRCUMFLEX, CEDILLA, DIAERESIS, OPEN_QUOTE, CLOSE_QUOTE, NULL
    }
}