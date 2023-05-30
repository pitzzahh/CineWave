using System.Windows;
using System.Windows.Input;

namespace CineWave.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnMoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}