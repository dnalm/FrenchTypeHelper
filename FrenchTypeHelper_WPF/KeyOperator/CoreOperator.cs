using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace FrenchTypeHelper_WPF.KeyOperator
{
    public class CoreOperator: IKeyOperator
    {
        private char lastChar = ' ';
        private bool killChars = false;
        private char nextChar = ' ';
        
        private Dictionary<char, Dictionary<char, char>> mapping = new Dictionary<char, Dictionary<char, char>>();

        public Dictionary<char, Dictionary<char, char>> Mapping
        {
            set
            {
                mapping = value;
            }
            private get
            {
                return mapping;
            }
        }
        

        public CoreOperator(Dictionary<char, Dictionary<char, char>> mappingConfig)
        {
            Mapping = mappingConfig;
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
            if (mapping.ContainsKey(e.KeyChar))
            {
                if (mapping[e.KeyChar].ContainsKey(lastChar))
                {
                    
                    nextChar = mapping[e.KeyChar][lastChar];
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
    }


}