using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace FrenchTypeHelper_WPF.KeyOperator
{
    public class HotKeyOperator: IKeyOperator
    {
        private IHotkeyObserver _hotkeyObserver;
        
        public HotKeyCombination SwitchStatusHotKey { get; set; }

        public HotKeyOperator(IHotkeyObserver hotkeyObserver)
        {
            _hotkeyObserver = hotkeyObserver;
        }
        
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            if (SwitchStatusHotKey != null && SwitchStatusHotKey.EqualToKeyEventArgs(e))
            {
                _hotkeyObserver.NotifySwitchStatus();
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
    }

    public class HotKeyCombination
    {
        public bool Control { set; get; } = false;
        public bool Alt { set; get; } = false;
        public bool Shift { set; get; } = false;
        public Keys? KeyCode { set; get; }

        public HotKeyCombination(bool control, bool alt, bool shift, Keys? keyCode)
        {
            Control = control;
            Alt = alt;
            Shift = shift;
            KeyCode = keyCode;
        }

        public bool EqualToKeyEventArgs(KeyEventArgs other)
        {
            return Control == other.Control && Alt == other.Alt && KeyCode == other.KeyCode;
        }
        
    } 
}