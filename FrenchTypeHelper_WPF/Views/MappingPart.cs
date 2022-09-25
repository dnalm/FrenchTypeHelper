using System;
using System.Collections.Generic;
using System.Linq;
using HandyControl.Controls;
using ComboBox = System.Windows.Controls.ComboBox;

namespace FrenchTypeHelper_WPF.Views.SettingPage
{
    public partial class SettingWindow 
    {
        private List<Char> _graveAccentBaseChars = new List<char>("AaEeUu".ToCharArray());
        private List<Char> _acuteAccentBaseChars = new List<char>("eE".ToCharArray());
        private List<Char> _circumflexAccentBaseChars = new List<char>("AaEeUuOoIi".ToCharArray());
        private List<Char> _cedillaAccentBaseChars = new List<char>("cC".ToCharArray());
        private List<Char> _diaeresisAccentBaseChars = new List<char>("eEuUuIi".ToCharArray());
        
        
//         public List<AccentEnum> AccentKinds = new List<AccentEnum>(new[]
// {
//     AccentEnum.GRAVE, AccentEnum.ACUTE, AccentEnum.CIRCUMFLEX, AccentEnum.CEDILLA, AccentEnum.DIAERESIS,
//     AccentEnum.OPEN_QUOTE, AccentEnum.CLOSE_QUOTE
// });

        private static char _openQuoteChar = '<';
        private static char _closeQuoteChar = '>';
        
        private Dictionary<Char, bool> QuickCharAvailableDic = GetInitedQuickCharAvailableDictionary();
        private static Dictionary<Char, bool> GetInitedQuickCharAvailableDictionary()
                {
                    var tmpDic = new Dictionary<char, bool>();
                    tmpDic.Add('-', true);
                    tmpDic.Add('_', true);
                    tmpDic.Add('=', true);
                    tmpDic.Add('+', true);
                    tmpDic.Add('[', true);
                    tmpDic.Add('{', true);
                    tmpDic.Add(']', true);
                    tmpDic.Add('}', true);
                    tmpDic.Add('\\', true);
                    tmpDic.Add('|', true);
                    tmpDic.Add(';', true);
                    tmpDic.Add(':', true);
                    tmpDic.Add('\'', true);
                    tmpDic.Add('"', true);
                    tmpDic.Add(',', true);
                    tmpDic.Add('<', true);
                    tmpDic.Add('.', true);
                    tmpDic.Add('>', true);
                    tmpDic.Add('/', true);
                    tmpDic.Add('?', true);
                    tmpDic.Add('`', true);
                    tmpDic.Add('~', true);
                    tmpDic.Add('!', true);
                    tmpDic.Add('@', true);
                    tmpDic.Add('#', true);
                    tmpDic.Add('$', true);
                    tmpDic.Add('%', true);
                    tmpDic.Add('^', true);
                    tmpDic.Add('&', true);
                    tmpDic.Add('*', true);
                    tmpDic.Add('(', true);
                    tmpDic.Add(')', true);
                    return tmpDic;
                }

        public Dictionary<AccentEnum, HashSet<char>> MappingBaseCharSetDict = new Dictionary<AccentEnum, HashSet<char>>();
        public Dictionary<char, AccentEnum> MappingQuickDict = new Dictionary<char, AccentEnum>();
        private Dictionary<AccentEnum, char> _quickDict = new Dictionary<AccentEnum, char>();

        private void RefreshMapping()
        {
            Dictionary<AccentEnum, HashSet<char>> tmpMappingBaseCharSetDict = new Dictionary<AccentEnum, HashSet<char>>();
            Dictionary<AccentEnum, char> quickerDict = new Dictionary<AccentEnum, char>();
            
            if (GraveAccentCheckBox.IsChecked == true)
            {
                GraveAccentCheckComboBox.IsEnabled = true;
                GraveQuickComboBox.IsEnabled = true;
                tmpMappingBaseCharSetDict.Add(AccentEnum.GRAVE, getBaseCharSetFromCheckComboBox(GraveAccentCheckComboBox));
                quickerDict.Add(AccentEnum.GRAVE, getQuickCharFromQuickComboBox(GraveQuickComboBox));
            }
            else
            {
                GraveAccentCheckComboBox.IsEnabled = false;
                GraveQuickComboBox.IsEnabled = false;
            }
            if (AcuteAccentCheckBox.IsChecked == true)
            {
                AcuteAccentCheckComboBox.IsEnabled = true;
                AcuteQuickComboBox.IsEnabled = true;
                tmpMappingBaseCharSetDict.Add(AccentEnum.ACUTE, getBaseCharSetFromCheckComboBox(AcuteAccentCheckComboBox));
                quickerDict.Add(AccentEnum.ACUTE, getQuickCharFromQuickComboBox(AcuteQuickComboBox));
            }else
            {
                AcuteAccentCheckComboBox.IsEnabled = false;
                AcuteQuickComboBox.IsEnabled = false;
            }
            if (CircumflexAccentCheckBox.IsChecked == true)
            {
                CircumflexAccentCheckComboBox.IsEnabled = true;
                CircumflexQuickComboBox.IsEnabled = true;
                tmpMappingBaseCharSetDict.Add(AccentEnum.CIRCUMFLEX, getBaseCharSetFromCheckComboBox(CircumflexAccentCheckComboBox));
                quickerDict.Add(AccentEnum.CIRCUMFLEX, getQuickCharFromQuickComboBox(CircumflexQuickComboBox));
            }else
            {
                CircumflexAccentCheckComboBox.IsEnabled = false;
                CircumflexQuickComboBox.IsEnabled = false;
            }
            if (CedillaAccentCheckBox.IsChecked == true)
            {
                CedillaAccentCheckComboBox.IsEnabled = true;
                CedillaQuickComboBox.IsEnabled = true;
                tmpMappingBaseCharSetDict.Add(AccentEnum.CEDILLA, getBaseCharSetFromCheckComboBox(CedillaAccentCheckComboBox));
                quickerDict.Add(AccentEnum.CEDILLA, getQuickCharFromQuickComboBox(CedillaQuickComboBox));
            }else
            {
                CedillaAccentCheckComboBox.IsEnabled = false;
                CedillaQuickComboBox.IsEnabled = false;
            }
            if (DiaeresisAccentCheckBox.IsChecked == true)
            {
                DiaeresisAccentCheckComboBox.IsEnabled = true;
                DiaeresisQuickComboBox.IsEnabled = true;
                tmpMappingBaseCharSetDict.Add(AccentEnum.DIAERESIS, getBaseCharSetFromCheckComboBox(DiaeresisAccentCheckComboBox));
                quickerDict.Add(AccentEnum.DIAERESIS, getQuickCharFromQuickComboBox(DiaeresisQuickComboBox));
            }else
            {
                DiaeresisAccentCheckComboBox.IsEnabled = false;
                DiaeresisQuickComboBox.IsEnabled = false;
            }
            if (OpenQuoteCheckBox.IsChecked == true)
            {
                OpenQuoteQuickComboBox.IsEnabled = true;
                tmpMappingBaseCharSetDict.Add(AccentEnum.OPEN_QUOTE, new HashSet<char>(new []{'<'}));
                quickerDict.Add(AccentEnum.OPEN_QUOTE, getQuickCharFromQuickComboBox(OpenQuoteQuickComboBox));
            }else
            {
                OpenQuoteQuickComboBox.IsEnabled = false;
            }
            if (CloseQuoteCheckBox.IsChecked == true)
            {
                CloseQuoteQuickComboBox.IsEnabled = true;
                tmpMappingBaseCharSetDict.Add(AccentEnum.CLOSE_QUOTE, new HashSet<char>(new []{'>'}));
                quickerDict.Add(AccentEnum.CLOSE_QUOTE, getQuickCharFromQuickComboBox(CloseQuoteQuickComboBox));
            }else
            {
                CloseQuoteQuickComboBox.IsEnabled = false;
            }

            MappingQuickDict = revertDict(quickerDict);
            MappingBaseCharSetDict = tmpMappingBaseCharSetDict;
            _quickDict = quickerDict;
        }
        
        private void ResetQuickCharAvailable() 
        {
            var keyCollection = QuickCharAvailableDic.Keys.ToArray();
            for (int i = 0; i  < keyCollection.Length; i++)
            {
                QuickCharAvailableDic[keyCollection[i]] = true;
            }
            QuickCharAvailableDic[((KeyValuePair<char, bool>)GraveQuickComboBox.SelectedValue).Key] = false;
            QuickCharAvailableDic[((KeyValuePair<char, bool>)AcuteQuickComboBox.SelectedValue).Key] = false;
            QuickCharAvailableDic[((KeyValuePair<char, bool>)CircumflexQuickComboBox.SelectedValue).Key] = false;
            QuickCharAvailableDic[((KeyValuePair<char, bool>)CedillaQuickComboBox.SelectedValue).Key] = false;
            QuickCharAvailableDic[((KeyValuePair<char, bool>)DiaeresisQuickComboBox.SelectedValue).Key] = false;
            QuickCharAvailableDic[((KeyValuePair<char, bool>)OpenQuoteQuickComboBox.SelectedValue).Key] = false;
            QuickCharAvailableDic[((KeyValuePair<char, bool>)CloseQuoteQuickComboBox.SelectedValue).Key] = false;
        }
        // ---------------  utils -------------------------        

        private HashSet<char> getBaseCharSetFromCheckComboBox(CheckComboBox ccb)
        {
            HashSet<char> baseCharSet = new HashSet<char>();
            foreach (var charObj in ccb.SelectedItems) 
            {
                baseCharSet.Add((char)charObj);
            }

            return baseCharSet;
        }

        private char getQuickCharFromQuickComboBox(ComboBox cb)
        {
            return ((KeyValuePair<char, bool>)cb.SelectedItem).Key;
        }

        private Dictionary<char, AccentEnum> revertDict(Dictionary<AccentEnum, char> originDict)
        {
            var retDict = new Dictionary<char, AccentEnum>();
            foreach (KeyValuePair<AccentEnum,char> pair in originDict)
            {
                retDict.Add(pair.Value, pair.Key);
            }
            return retDict;
        }
    }
}