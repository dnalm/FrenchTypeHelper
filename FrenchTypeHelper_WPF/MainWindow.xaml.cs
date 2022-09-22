

using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace FrenchTypeHelper_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private IKeyboardMouseEvents m_GlobalHook;
        
        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();
            
            m_GlobalHook.KeyUp += GlobalHookKeyUp;
            m_GlobalHook.KeyPress += GlobalHookKeyDown;
        }
        
        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            
        }
        
        private void GlobalHookKeyDown(object sender, KeyPressEventArgs e)
        {
            
        }
        
        public void Unsubscribe()
        {
           
            m_GlobalHook.KeyUp -= GlobalHookKeyUp;
            m_GlobalHook.KeyPress -= GlobalHookKeyDown;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}