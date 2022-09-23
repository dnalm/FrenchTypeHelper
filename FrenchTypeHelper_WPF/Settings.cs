using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Diagnostics;
using IniParser;

namespace FrenchTypeHelper_WPF
{
    public class Settings
    {
        private void Init()
        {
            _startWhenSystemStartUp = true;
            _enabledWhenStartUp = true;
                
            _grave = ']';
            _acute = '[';
            _circonflexe = '-';
            _cedilla = '/';
            _trema = '+';
            _openQuote = '<';
            _closeQuote = '>';
            
            _graveBaseLetters.Clear();
            _graveBaseLetters.Add('a');
            _graveBaseLetters.Add('e');
            _graveBaseLetters.Add('u');
            _graveBaseLetters.Add('A');
            _graveBaseLetters.Add('E');
            _graveBaseLetters.Add('U');
            
            _acuteBaseLetters.Clear();
            _acuteBaseLetters.Add('e');
            _acuteBaseLetters.Add('E');
            
            _cironflexeBaseLetters.Clear();
            _cironflexeBaseLetters.Add('a');
            _cironflexeBaseLetters.Add('e');
            _cironflexeBaseLetters.Add('u');
            _cironflexeBaseLetters.Add('o');
            _cironflexeBaseLetters.Add('i');
            _cironflexeBaseLetters.Add('A');
            _cironflexeBaseLetters.Add('E');
            _cironflexeBaseLetters.Add('O');
            _cironflexeBaseLetters.Add('U');
            _cironflexeBaseLetters.Add('I');
            
            _cedillaBaseLetters.Clear();
            _cedillaBaseLetters.Add('c');
            _cedillaBaseLetters.Add('C');
            
            _tremaBaseLetters.Clear();
            _tremaBaseLetters.Add('e');
            _tremaBaseLetters.Add('u');
            _tremaBaseLetters.Add('i');
            _tremaBaseLetters.Add('E');
            _tremaBaseLetters.Add('U');
            _tremaBaseLetters.Add('I');
            
            _openQuoteBaseLetters.Clear();
            _openQuoteBaseLetters.Add('<');
            
            _closeQuoteBaseLetters.Clear();
            _closeQuoteBaseLetters.Add('>');
        }

        private String settingPath = "setting.ini";
        
        public Settings()
        {
            Init();
            
            if (File.Exists(settingPath))
            {
                Load();
            }
            else
            {
                Save();
            }
        }

        public void Save()
        {
            
        }

        public void Load()
        {
            var parser = new FileIniDataParser();
            var IniData = parser.ReadFile(settingPath);

            StartWhenSystemStartUp = Convert.ToBoolean(IniData["System"]["StartWhenSystemStartUp"]);
        }
        
        
        
        private bool _startWhenSystemStartUp;
        private bool _enabledWhenStartUp;

        private char _grave = ']';
        private char _acute = '[';
        private char _circonflexe = '-';
        private char _cedilla = '/';
        private char _trema = '+';
        private char _openQuote = '<';
        private char _closeQuote = '>';

        private HashSet<Char> _graveBaseLetters = new HashSet<Char>();
        private HashSet<Char> _acuteBaseLetters = new HashSet<Char>();
        private HashSet<Char> _cironflexeBaseLetters = new HashSet<Char>();
        private HashSet<Char> _cedillaBaseLetters = new HashSet<Char>();
        private HashSet<Char> _tremaBaseLetters = new HashSet<Char>();
        private HashSet<Char> _openQuoteBaseLetters = new HashSet<Char>();
        private HashSet<Char> _closeQuoteBaseLetters = new HashSet<Char>();

        public bool StartWhenSystemStartUp
        {
            get => _startWhenSystemStartUp;
            set => _startWhenSystemStartUp = value;
        }

        public bool EnabledWhenStartUp
        {
            get => _enabledWhenStartUp;
            set => _enabledWhenStartUp = value;
        }

        public char Grave
        {
            get => _grave;
            set => _grave = value;
        }

        public char Acute
        {
            get => _acute;
            set => _acute = value;
        }

        public char Circonflexe
        {
            get => _circonflexe;
            set => _circonflexe = value;
        }

        public char Cedilla
        {
            get => _cedilla;
            set => _cedilla = value;
        }

        public char Trema
        {
            get => _trema;
            set => _trema = value;
        }

        public char OpenQuote
        {
            get => _openQuote;
            set => _openQuote = value;
        }

        public char CloseQuote
        {
            get => _closeQuote;
            set => _closeQuote = value;
        }

        public HashSet<char> GraveBaseLetters
        {
            get => _graveBaseLetters;
            set => _graveBaseLetters = value;
        }

        public HashSet<char> AcuteBaseLetters
        {
            get => _acuteBaseLetters;
            set => _acuteBaseLetters = value;
        }

        public HashSet<char> CironflexeBaseLetters
        {
            get => _cironflexeBaseLetters;
            set => _cironflexeBaseLetters = value;
        }

        public HashSet<char> CedillaBaseLetters
        {
            get => _cedillaBaseLetters;
            set => _cedillaBaseLetters = value;
        }

        public HashSet<char> TremaBaseLetters
        {
            get => _tremaBaseLetters;
            set => _tremaBaseLetters = value;
        }

        public HashSet<char> OpenQuoteBaseLetters
        {
            get => _openQuoteBaseLetters;
            set => _openQuoteBaseLetters = value;
        }

        public HashSet<char> CloseQuoteBaseLetters
        {
            get => _closeQuoteBaseLetters;
            set => _closeQuoteBaseLetters = value;
        }



    }
}