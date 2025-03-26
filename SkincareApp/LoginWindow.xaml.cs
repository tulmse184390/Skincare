using BLL.Services.Implements;
using BLL.Services.Interfaces;
using DAL.Entities;
using System.Windows;
using System.Windows.Input;

namespace SkincareApp
{
    public partial class LoginWindow : Window
    {
        public static User? USER;
        private IUserService userService;

        public LoginWindow()
        {
            userService = new UserService();
            InitializeComponent();
        }

        private async void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Password;
            var user = await userService.LoginAsync(email, password);
            if (user == null)
            {
                MessageBox.Show("Email or password is invalid!");
            }
            else
            {
                USER = user;
                switch (user.Role.RoleName)
                {
                    case "Customer":
                        CustomerDashboardWindow customerDashboard = new();
                        customerDashboard.Show();
                        break;
                    case "Admin":
                        AdminDashboardWindow adminDashboard = new();
                        adminDashboard.Show();
                        break;
                    case "Employee":
                        EmployeeDashboardWindow employeeDashboard = new();
                        employeeDashboard.Show();
                        break;
                    default:
                        MessageBox.Show("Your information is invalid!");
                        return;
                }
                this.Close();
            }
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

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void changeToRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new();
            registerWindow.Show();
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        // Style end -->
    }
}
