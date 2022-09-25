using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using FrenchTypeHelper_WPF.KeyOperator;
using HandyControl.Controls;
using IniParser;
using IniParser.Model;
using MessageBox = System.Windows.MessageBox;

namespace FrenchTypeHelper_WPF.Views.SettingPage
{
    public partial class SettingWindow
    {
        private string _config_path = "config.ini";
        
        private void CreateDefaultConfigFile()
        {
            if (!File.Exists(_config_path))
            {
                var fs = new FileStream(_config_path, FileMode.Create);
                var streamWriter = new StreamWriter(fs);
                streamWriter.WriteLine(Resource.conifg);
                streamWriter.Flush();
                streamWriter.Close();
                fs.Close();

                // // MessageBox.Show(Resource.config);
                // var binaryWriter = new BinaryWriter(fs, Encoding.UTF8);
                // // binaryWriter.Write("\n");
                // binaryWriter.Write(Resource.conifg);
                // binaryWriter.Flush();
                // binaryWriter.Close();
                // fs.Close();
            }
        }

        private void Load()
        {
            CreateDefaultConfigFile();
            var iniParser = new FileIniDataParser();
            IniData iniData = iniParser.ReadFile(_config_path);
            
            // system
            var systemSection = iniData["system"];
            
            IsAutoRun = Convert.ToBoolean(systemSection["AutoRun"]);
            AutoRunComBox.IsChecked = IsAutoRun;

            IsEnableWhenStartup = Convert.ToBoolean(systemSection["EnableWhenStart"]);
            EnableWhenStartup.IsChecked = IsEnableWhenStartup;
            
            // hotkey
            var switchHotkeySection = iniData["hotkey_switch"];
            
            IsHotkeyEnable = Convert.ToBoolean(switchHotkeySection["Enable"]);
            HotkeyCheckBox.IsChecked = IsHotkeyEnable;
            string[] switchHotKeys = switchHotkeySection["Hotkey"].Split('+');
            var keyStr = switchHotKeys[switchHotKeys.Length - 1];
            // Keys k;
            // Keys.TryParse(switchHotKeys[switchHotKeys.Length - 1], true, out k);
            Hotkey = new HotKeyCombination(switchHotKeys.Contains("Ctrl"), switchHotKeys.Contains("Alt"),
                switchHotKeys.Contains("Shift"), keyStr);
            HotKeyKeyCode.SelectedIndex = _keysList.IndexOf(keyStr);
            HotkeyCtrl.IsChecked = Hotkey.Control;
            HotkeyAlt.IsChecked = Hotkey.Alt;
            HotkeyShift.IsChecked = Hotkey.Shift;
            

            // mapping
            var graveSection = iniData["mapping_grave"];
            GraveAccentCheckBox.IsChecked = Convert.ToBoolean(graveSection["Enable"]);
            foreach (var baseChar in graveSection["BaseChar"].ToCharArray())
            {
                GraveAccentCheckComboBox.SelectedItems.Add(baseChar);
            }
            GraveQuickComboBox.SelectedValue =
                QuickCharAvailableDic.Where(i => i.Key == Convert.ToChar(graveSection["QuickChar"])).First();

            var acuteSection = iniData["mapping_acute"];
            AcuteAccentCheckBox.IsChecked = Convert.ToBoolean(acuteSection["Enable"]);
            foreach (var baseChar in acuteSection["BaseChar"].ToCharArray())
            {
                AcuteAccentCheckComboBox.SelectedItems.Add(baseChar);
            }
            AcuteQuickComboBox.SelectedValue =
                QuickCharAvailableDic.Where(i => i.Key == Convert.ToChar(acuteSection["QuickChar"])).First();
            
            var circumflexSection = iniData["mapping_circumflex"];
            CircumflexAccentCheckBox.IsChecked = Convert.ToBoolean(circumflexSection["Enable"]);
            foreach (var baseChar in circumflexSection["BaseChar"].ToCharArray())
            {
                CircumflexAccentCheckComboBox.SelectedItems.Add(baseChar);
            }
            CircumflexQuickComboBox.SelectedValue =
                QuickCharAvailableDic.Where(i => i.Key == Convert.ToChar(circumflexSection["QuickChar"])).First();
            
            var cedillaSection = iniData["mapping_cedilla"];
            CedillaAccentCheckBox.IsChecked = Convert.ToBoolean(cedillaSection["Enable"]);
            foreach (var baseChar in cedillaSection["BaseChar"].ToCharArray())
            {
                CedillaAccentCheckComboBox.SelectedItems.Add(baseChar);
            }
            CedillaQuickComboBox.SelectedValue =
                QuickCharAvailableDic.Where(i => i.Key == Convert.ToChar(cedillaSection["QuickChar"])).First();
            
            var diaeresisSection = iniData["mapping_diaeresis"];
            DiaeresisAccentCheckBox.IsChecked = Convert.ToBoolean(diaeresisSection["Enable"]);
            foreach (var baseChar in diaeresisSection["BaseChar"].ToCharArray())
            {
                DiaeresisAccentCheckComboBox.SelectedItems.Add(baseChar);
            }
            DiaeresisQuickComboBox.SelectedValue =
                QuickCharAvailableDic.Where(i => i.Key == Convert.ToChar(diaeresisSection["QuickChar"])).First();
            
            var openQuoteSection = iniData["mapping_open_quote"];
            OpenQuoteCheckBox.IsChecked = Convert.ToBoolean(openQuoteSection["Enable"]);
            OpenQuoteQuickComboBox.SelectedValue =
                QuickCharAvailableDic.Where(i => i.Key == Convert.ToChar(openQuoteSection["QuickChar"])).First();
            
            var closeQuoteSection = iniData["mapping_close_quote"];
            CloseQuoteCheckBox.IsChecked = Convert.ToBoolean(closeQuoteSection["Enable"]);
            CloseQuoteQuickComboBox.SelectedValue =
                QuickCharAvailableDic.Where(i => i.Key == Convert.ToChar(closeQuoteSection["QuickChar"])).First();
            
            
            // interrupt
        }

        private void SaveConfigs()
        {
            var iniParser = new FileIniDataParser();
            IniData iniData = iniParser.ReadFile(_config_path);
            
            // system
            var systemSection = iniData["system"];

            systemSection["AutoRun"] = Convert.ToString(IsAutoRun);
            systemSection["EnableWhenStart"] = Convert.ToString(IsEnableWhenStartup);

            // hotkey
            var switchHotkeySection = iniData["hotkey_switch"];

            switchHotkeySection["Enable"] = Convert.ToString(IsHotkeyEnable);
            switchHotkeySection["Hotkey"] = Hotkey.ToString();

            // mapping
            var graveSection = iniData["mapping_grave"];
            graveSection["Enable"] = Convert.ToString(GraveAccentCheckBox.IsChecked);
            graveSection["BaseChar"] = getBaseCharFromCheckComboBox(GraveAccentCheckComboBox);
            graveSection["QuickChar"] =
                Convert.ToString(((KeyValuePair<char, bool>)GraveQuickComboBox.SelectedValue).Key);
            
            var acuteSection = iniData["mapping_acute"];
            acuteSection["Enable"] = Convert.ToString(AcuteAccentCheckBox.IsChecked);
            acuteSection["BaseChar"] = getBaseCharFromCheckComboBox(AcuteAccentCheckComboBox);
            acuteSection["QuickChar"] =
                Convert.ToString(((KeyValuePair<char, bool>)AcuteQuickComboBox.SelectedValue).Key);
            
            var circumflexSection = iniData["mapping_circumflex"];
            circumflexSection["Enable"] = Convert.ToString(CircumflexAccentCheckBox.IsChecked);
            circumflexSection["BaseChar"] = getBaseCharFromCheckComboBox(CircumflexAccentCheckComboBox);
            circumflexSection["QuickChar"] =
                Convert.ToString(((KeyValuePair<char, bool>)CircumflexQuickComboBox.SelectedValue).Key);
            
            var cedillaSection = iniData["mapping_cedilla"];
            cedillaSection["Enable"] = Convert.ToString(CedillaAccentCheckBox.IsChecked);
            cedillaSection["BaseChar"] = getBaseCharFromCheckComboBox(CedillaAccentCheckComboBox);
            cedillaSection["QuickChar"] =
                Convert.ToString(((KeyValuePair<char, bool>)CedillaQuickComboBox.SelectedValue).Key);
            
            var diaeresisSection = iniData["mapping_diaeresis"];
            diaeresisSection["Enable"] = Convert.ToString(DiaeresisAccentCheckBox.IsChecked);
            diaeresisSection["BaseChar"] = getBaseCharFromCheckComboBox(DiaeresisAccentCheckComboBox);
            diaeresisSection["QuickChar"] =
                Convert.ToString(((KeyValuePair<char, bool>)DiaeresisQuickComboBox.SelectedValue).Key);
            
            var openQuoteSection = iniData["mapping_open_quote"];
            openQuoteSection["Enable"] = Convert.ToString(OpenQuoteCheckBox.IsChecked);
            openQuoteSection["QuickChar"] =
                Convert.ToString(((KeyValuePair<char, bool>)OpenQuoteQuickComboBox.SelectedValue).Key);
            
            var closeQuoteSection = iniData["mapping_close_quote"];
            closeQuoteSection["Enable"] = Convert.ToString(CloseQuoteCheckBox.IsChecked);
            closeQuoteSection["QuickChar"] =
                Convert.ToString(((KeyValuePair<char, bool>)CloseQuoteQuickComboBox.SelectedValue).Key);
            
            // interrupt
            
            iniParser.WriteFile(_config_path, iniData);
        }
        
        // ---- utils ----
        private String getBaseCharFromCheckComboBox(CheckComboBox ccb)
        {
            string str = "";
            foreach (object charObj in ccb.SelectedItems)
            {
                str += ((char)charObj);
            }
            return str;
        }



    }
}