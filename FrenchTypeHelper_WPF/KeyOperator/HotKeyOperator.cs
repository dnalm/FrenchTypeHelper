using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FrenchTypeHelper_WPF.Views.SettingPage;
using Gma.System.MouseKeyHook;

namespace FrenchTypeHelper_WPF.KeyOperator
{
    public class HotKeyOperator: IKeyOperator
    {
        private List<IHotkeyObserver> _hotkeyObservers;

        private SettingWindow _settingWindow;
        
        // public HotKeyCombination SwitchStatusHotKey { get; set; }



        public HotKeyOperator(List<IHotkeyObserver> hotkeyObserverses, SettingWindow settingWindow)
        {
            _hotkeyObservers = hotkeyObserverses;
            _settingWindow = settingWindow;
            // SwitchStatusHotKey
        }
        
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            if (_settingWindow.IsHotkeyEnable && _settingWindow.Hotkey != null && _settingWindow.Hotkey.EqualToKeyEventArgs(e))
            {
                _hotkeyObservers.ForEach(o => o.NotifySwitchStatus());
            }

            if (e.Control || e.Alt)
            {
                _hotkeyObservers.ForEach(o => o.NotifyInterrupt());
            }

            ;Console.WriteLine(e);
        }

        protected override void init()
        {
        }

        protected override void Subscribe()
        {
            GlobalHook.KeyDown += GlobalHookKeyDown;
        }

        protected override void Unsubscribe()
        {
            GlobalHook.KeyDown -= GlobalHookKeyDown;
        }
    }
    
    public interface IHotkeyObserver
    {
        void NotifySwitchStatus();
        void NotifyInterrupt();
    }

    public class HotKeyCombination
    {
        public bool Control { set; get; } = false;
        public bool Alt { set; get; } = false;
        public bool Shift { set; get; } = false;
        private string keyStr;

        public string KeyStr
        {
            get => keyStr;
            set
            {
                keyStr = value;
                Keys.TryParse(value, out KeyCode);
            }
        }
        public Keys KeyCode;


        public HotKeyCombination(bool control, bool alt, bool shift, string keyStr)
        {
            Control = control;
            Alt = alt;
            Shift = shift;
            KeyStr = keyStr;
        }

        public bool EqualToKeyEventArgs(KeyEventArgs other)
        {
            return Control == other.Control && Alt == other.Alt && KeyCode == other.KeyCode;
        }

        public override string ToString()
        {
            List<string> keys = new List<string>();
            if(Control) keys.Add("Ctrl");
            if(Alt) keys.Add("Alt");
            if(Shift) keys.Add("Shift");
            keys.Add(KeyStr);
            return string.Join("+", keys);
        }
    } 
}