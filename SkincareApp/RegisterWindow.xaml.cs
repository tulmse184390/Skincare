using BLL.Services.Implements;
using BLL.Services.Interfaces;
using DAL.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SkincareApp
{
    public partial class RegisterWindow : Window
    {
        private IUserService userService;
        private IRoleService roleService;

        public RegisterWindow()
        {
            userService = new UserService();
            roleService = new RoleService();
            InitializeComponent();
        }

        private async void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var email = txtEmail.Text;
                var password = txtPassword.Password;
                var firstname = txtFirstName.Text;
                var lastname = txtLastName.Text;
                var phone = txtPhonumber.Text;
                var gender = Male.IsChecked == true ? "Male" : "Female";
                var roleId = (await roleService.GetRolesAsync()).FirstOrDefault(x => x.RoleName == "Customer")?.RoleId;

                if (roleId == null)
                {
                    MessageBox.Show("Something went wrong!");
                    return;
                } 
                var newUser = new User()
                {
                    RoleId = roleId.Value,
                    Email = email,
                    Password = password,
                    FirstName = firstname,
                    LastName = lastname,
                    PhoneNumber = phone,
                    Gender = gender,
                    Status = "Active"
                };

                await userService.Register(newUser);
                MessageBox.Show("Register successfully");
                LoginWindow loginWindow = new();
                loginWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // <-- Style start
        private void textFirstName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFirstName.Focus();
        }

        private void textLastName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLastName.Focus();
        }

        private void textPhonumber_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPhonumber.Focus();
        }

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFirstName.Text) && txtFirstName.Text.Length > 0)
            {
                textFirstName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textFirstName.Visibility = Visibility.Visible;
            }
        }

        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLastName.Text) && txtLastName.Text.Length > 0)
            {
                textLastName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textLastName.Visibility = Visibility.Visible;
            }
        }

        private void txtPhonumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhonumber.Text) && txtPhonumber.Text.Length > 0)
            {
                textPhonumber.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPhonumber.Visibility = Visibility.Visible;
            }
        }

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

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void changeToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        // Style end -->
    }
}
