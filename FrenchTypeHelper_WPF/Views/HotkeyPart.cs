using System.Collections.Generic;
using System.Windows.Forms;
using FrenchTypeHelper_WPF.KeyOperator;

namespace FrenchTypeHelper_WPF.Views.SettingPage
{
    public partial class SettingWindow
    {
        public bool IsHotkeyEnable;
        
        public HotKeyCombination Hotkey;
     
        
        private List<string> _keysList = _InitKeyList();
        private static List<string> _InitKeyList()
        {
            var keysList = new List<string>();
            keysList.Add("A");
            keysList.Add("B");
            keysList.Add("C");
            keysList.Add("D");
            keysList.Add("E");
            keysList.Add("F");
            keysList.Add("G");
            keysList.Add("H");
            keysList.Add("I");
            keysList.Add("J");
            keysList.Add("K");
            keysList.Add("L");
            keysList.Add("M");
            keysList.Add("N");
            keysList.Add("O");
            keysList.Add("P");
            keysList.Add("Q");
            keysList.Add("R");
            keysList.Add("S");
            keysList.Add("T");
            keysList.Add("U");
            keysList.Add("V");
            keysList.Add("W");
            keysList.Add("X");
            keysList.Add("Y");
            keysList.Add("Z");
            
            keysList.Add("F1");
            keysList.Add("F2");
            keysList.Add("F3");
            keysList.Add("F4");
            keysList.Add("F5");
            keysList.Add("F6");
            keysList.Add("F7");
            keysList.Add("F8");
            keysList.Add("F9");
            keysList.Add("F10");
            keysList.Add("F11");
            keysList.Add("F12");
            
            keysList.Add("Space");
            keysList.Add("Oemcomma");
            keysList.Add("Oemplus");
            keysList.Add("OemQuestion");
            keysList.Add("OemQuotes");
            keysList.Add("OemOpenBrackets");
            keysList.Add("OemCloseBrackets");

            return keysList;
        }
    }
}