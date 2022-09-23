using System.Windows;

namespace FrenchTypeHelper_WPF.Views
{
    public partial class SettingWindow : Window
    {
        private MainWindow _mainWindow;
        public SettingWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}