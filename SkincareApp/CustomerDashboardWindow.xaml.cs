using System.Windows;
using System.Windows.Input;

namespace SkincareApp
{
    public partial class CustomerDashboardWindow : Window
    {
        public CustomerDashboardWindow()
        {
            InitializeComponent();
        }

        // <-- Style start
        private void closeBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void minimizeBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void logoutBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LoginWindow.USER = null;
            LoginWindow loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        // Style end -->
    }
}
