using BLL.Services.Implements;
using BLL.Services.Interfaces;
using DAL.Entities;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SkincareApp
{
    public partial class CustomerDashboardWindow : Window
    {
        private IUserService userService;

        public CustomerDashboardWindow()
        {
            userService = new UserService();
            InitializeComponent();
        }

        // <-- Profile table logic start
        private void ViewProfile_MouseUp(object sender, MouseButtonEventArgs e)
        {
            homeTable.Visibility = Visibility.Collapsed;
            profileTable.Visibility = Visibility.Visible;
            LoadProfile();
            UnactiveTextBox();
        }

        private void editProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ActiveTextBox();
        }

        private async void saveProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LoginWindow.USER != null)
            {
                var id = LoginWindow.USER.UserId;
                var firstName = txtProfileFirstName.Text;
                var lastName = txtProfileLastName.Text;
                var phoneNumber = txtProfilePhoneNumber.Text;
                var gender = Male.IsChecked == true ? "Male" : "Female";
                var password = LoginWindow.USER.Password;

                var phoneNumberPattern = @"^0\d{9}$";
                if (phoneNumber != null && !Regex.IsMatch(phoneNumber, phoneNumberPattern))
                {
                    MessageBox.Show("The phone number is invalid!");
                    return;
                }

                if (NewPasswordContainer.Visibility == Visibility.Visible)
                {
                    if (ConfirmPassword())
                    {
                        password = txtNewProfilePass.Password;
                    }
                    return;
                }

                User tmpUser = new()
                {
                    UserId = id,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Gender = gender,
                    Password = password
                };

                LoginWindow.USER = await userService.UpdateProfileAsync(tmpUser);
                LoadProfile();
                UnactiveTextBox();
                FinishManagePassword();
            }
        }

        private void ChangePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            NewPasswordContainer.Visibility = Visibility.Visible;
            SavePasswordBtn.Visibility = Visibility.Visible;
            CancelPasswordBtn.Visibility = Visibility.Visible;
            txtOldProfilePass.Password = "";
            txtOldProfilePass.IsEnabled = true;
        }

        private async void SavePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmPassword() && LoginWindow.USER != null)
            {
                await userService.ChangePasswordAsync(LoginWindow.USER.Email, txtNewProfilePass.Password);
                FinishManagePassword();
            }
        }

        private void CancelPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            FinishManagePassword();
        }

        private void LoadProfile()
        {
            if (LoginWindow.USER != null)
            {
                txtProfileFirstName.Text = LoginWindow.USER.FirstName;
                txtProfileLastName.Text = LoginWindow.USER.LastName;
                txtProfilePhoneNumber.Text = LoginWindow.USER.PhoneNumber;
                txtProfileEmail.Text = LoginWindow.USER.Email;
                if (LoginWindow.USER.Gender == "Male")
                {
                    Male.IsChecked = true;
                }
                else
                {
                    Female.IsChecked = true;
                }
            }
        }

        private void ActiveTextBox()
        {
            saveProfileBtn.IsEnabled = true;
            txtProfileFirstName.IsEnabled = true;
            txtProfileLastName.IsEnabled = true;
            txtProfilePhoneNumber.IsEnabled = true;
            txtProfileGender.IsEnabled = true;
        }

        private void UnactiveTextBox()
        {
            saveProfileBtn.IsEnabled = false;
            txtProfileFirstName.IsEnabled = false;
            txtProfileLastName.IsEnabled = false;
            txtProfilePhoneNumber.IsEnabled = false;
            txtProfileGender.IsEnabled = false;
        }

        private bool ConfirmPassword()
        {
            if (LoginWindow.USER != null)
            {
                if (txtOldProfilePass.Password == LoginWindow.USER.Password)
                {
                    var newPassword = txtNewProfilePass.Password;
                    var confirmPassword = txtConfirmProfilePass.Password;

                    if (string.IsNullOrEmpty(newPassword))
                    {
                        MessageBox.Show("Password can't be empty!");
                        return false;
                    }

                    if (newPassword == confirmPassword)
                    {
                        if (newPassword == txtOldProfilePass.Password)
                        {
                            MessageBox.Show("New password is same as old password!");
                            return false;
                        }
                        MessageBox.Show("Change password successfully!");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("New password doesn't match with confirm password!");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Old password is not correct!");
                    return false;
                }
            }
            return false;
        }

        private void FinishManagePassword()
        {
            if (LoginWindow.USER != null)
            {
                txtOldProfilePass.Password = LoginWindow.USER.Password;
                txtOldProfilePass.IsEnabled = false;
                txtNewProfilePass.Password = "";
                txtConfirmProfilePass.Password = "";
                NewPasswordContainer.Visibility = Visibility.Collapsed;
                CancelPasswordBtn.Visibility = Visibility.Collapsed;
                SavePasswordBtn.Visibility = Visibility.Collapsed;
            }
        }
        // Profile table logic end -->

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
