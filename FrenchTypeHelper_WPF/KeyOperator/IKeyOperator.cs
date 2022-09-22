using System;
using Gma.System.MouseKeyHook;

namespace FrenchTypeHelper_WPF.KeyOperator
{
    public abstract class IKeyOperator: IDisposable
    {
        protected IKeyboardMouseEvents GlobalHook;

        private bool _enabled = false;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                if (_enabled)
                {
                    init();
                    Subscribe();
                }
                else
                {
                    Unsubscribe();
                }
            }
        }

        protected IKeyOperator()
        {
            GlobalHook = Hook.GlobalEvents();
            Enabled = false;
        }
        
        public void Dispose()
        {
            Unsubscribe();
        }

        protected abstract void init();

        protected abstract void Subscribe();

        protected abstract void Unsubscribe();
        
        
    }
}