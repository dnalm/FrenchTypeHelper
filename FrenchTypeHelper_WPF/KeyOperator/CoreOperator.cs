using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FrenchTypeHelper_WPF.Views;
using Gma.System.MouseKeyHook;
using SettingWindow = FrenchTypeHelper_WPF.Views.SettingPage;

namespace FrenchTypeHelper_WPF.KeyOperator
{
    public class CoreOperator: IKeyOperator, IHotkeyObserver
    {
        private char lastChar = ' ';
        private bool killChars = false;
        private char nextChar = ' ';

        private SettingWindow.SettingWindow _settingWindow;

        private Dictionary<SettingWindow.AccentEnum, Dictionary<char, char>> mapping = _InitMapping();
        private static Dictionary<SettingWindow.AccentEnum, Dictionary<char, char>> _InitMapping()
        {

        Dictionary<SettingWindow.AccentEnum, Dictionary<char, char>> _mapping =
            new Dictionary<SettingWindow.AccentEnum, Dictionary<char, char>>();
        
            Dictionary<char, char> cedillaDict = new Dictionary<char, char>();
            _mapping.Add(SettingWindow.AccentEnum.CEDILLA, cedillaDict);
            cedillaDict.Add('c', 'ç');
            cedillaDict.Add('C', 'Ç');
            
            Dictionary<char, char> graveDict = new Dictionary<char, char>();
            _mapping.Add(SettingWindow.AccentEnum.GRAVE, graveDict);
            graveDict.Add('a', 'à');
            graveDict.Add('A', 'À');
            graveDict.Add('e', 'è');
            graveDict.Add('E', 'È');
            graveDict.Add('u', 'ù');
            graveDict.Add('U', 'Ù');
            
            
            Dictionary<char, char> circumflexDict = new Dictionary<char, char>();
            _mapping.Add(SettingWindow.AccentEnum.CIRCUMFLEX, circumflexDict);
            circumflexDict.Add('a', 'â');
            circumflexDict.Add('A', 'Â');
            circumflexDict.Add('e', 'ê');
            circumflexDict.Add('E', 'Ê');
            circumflexDict.Add('u', 'û');
            circumflexDict.Add('U', 'Û');
            circumflexDict.Add('o', 'ô');
            circumflexDict.Add('O', 'Ô');
            circumflexDict.Add('i', 'î');
            circumflexDict.Add('I', 'Î');
            
            Dictionary<char, char> diaeresisDict = new Dictionary<char, char>();
            _mapping.Add(SettingWindow.AccentEnum.DIAERESIS, diaeresisDict);
            diaeresisDict.Add('e', 'ë');
            diaeresisDict.Add('E', 'Ë');
            diaeresisDict.Add('u', 'ü');
            diaeresisDict.Add('U', 'Ü');
            diaeresisDict.Add('i', 'ï');
            diaeresisDict.Add('I', 'Ï');
            
            Dictionary<char, char> acuteDict = new Dictionary<char, char>();
            _mapping.Add(SettingWindow.AccentEnum.ACUTE, acuteDict);
            acuteDict.Add('e', 'é');
            acuteDict.Add('E', 'É');
            
            Dictionary<char, char> openQuoteDict = new Dictionary<char, char>();
            _mapping.Add(SettingWindow.AccentEnum.OPEN_QUOTE, openQuoteDict);
            openQuoteDict.Add('<', '«');
            
            
            Dictionary<char, char> closeQuoteDict = new Dictionary<char, char>();
            _mapping.Add(SettingWindow.AccentEnum.CLOSE_QUOTE, closeQuoteDict);
            closeQuoteDict.Add('>', '»');

            return _mapping;
        }
        
        public CoreOperator(SettingWindow.SettingWindow settingWindow)
        {
            _settingWindow = settingWindow;
            
           
        }

        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            if (killChars)
            {
                killChars = false;
                SendKeys.SendWait("{BS}{BS}");
                SendKeys.SendWait(nextChar.ToString());
                
            }
        }
        
        
        private void GlobalHookPressDown(object sender, KeyPressEventArgs e)
        {
            if (_settingWindow.MappingQuickDict.ContainsKey(e.KeyChar))
            {
                var accentEnum = _settingWindow.MappingQuickDict[e.KeyChar];
                if (_settingWindow.MappingBaseCharSetDict[accentEnum].Contains(lastChar))
                {
                    nextChar = mapping[accentEnum][lastChar];
                    killChars = true;
                }
            }

            lastChar = e.KeyChar;
        }

        protected override void init()
        {
            lastChar = ' ';
            killChars = false;
            nextChar = ' ';
        }

        protected override void Subscribe()
        {
            GlobalHook.KeyUp += GlobalHookKeyUp;
            GlobalHook.KeyPress += GlobalHookPressDown;
        }

        protected override void Unsubscribe()
        {
            GlobalHook.KeyUp -= GlobalHookKeyUp;
            GlobalHook.KeyPress -= GlobalHookPressDown;
        }

        public void NotifySwitchStatus()
        {}

        private void Interrupt()
        {
            killChars = false;
            nextChar = ' ';
        }

        public void NotifyInterrupt()
        {
            Interrupt();
        }
    }


}