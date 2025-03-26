using BLL.Services.Implements;
using BLL.Services.Interfaces;
using DAL.Entities;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SkincareApp
{
    public partial class CustomerDashboardWindow : Window
    {
        private IUserService userService;
        private IServiceService serviceService;
        private IAppointmentService appointmentService;
        private IAppointmentDetailService appointmentDetailService;
        private List<Service> tmpServices;

        public CustomerDashboardWindow()
        {
            userService = new UserService();
            serviceService = new ServiceService();
            appointmentService = new AppointmentService();
            appointmentDetailService = new AppointmentDetailService();
            tmpServices = new();
            InitializeComponent();
        }

        // <-- Calendar logic start 
        private async void cancelAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;
            if (btn != null)
            {
                if (btn.Tag is Appointment appointment)
                {
                    if ((appointment.StartTime - DateTime.Now).Duration() <= TimeSpan.FromHours(24))
                    {
                        MessageBox.Show("You can't cancel this appoitment!");
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Do you want to cancel this appoitment ?", "Caution", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            await appointmentService.ChangeAppoitmentStatusAsync(appointment, "Canceled");
                            MessageBox.Show("Cancel appoitment successfully!");
                            LoadUserAppointment();
                        }
                    }
                }
            }
        }

        private async void LoadUserAppointment()
        {
            if (LoginWindow.USER != null)
            {
                PendingAppointmentList.ItemsSource = await appointmentService.GetAppointmentsAsync(LoginWindow.USER.UserId, "Pending");
                CompletedAppointmentList.ItemsSource = await appointmentService.GetAppointmentsAsync(LoginWindow.USER.UserId, "Completed");
                CanceledAppointmentList.ItemsSource = await appointmentService.GetAppointmentsAsync(LoginWindow.USER.UserId, "Canceled");
            }
        }
        // Calendar logic end -->

        // <-- List services logic start
        private void selectService_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;
            if (btn?.Tag is Service service)
            {
                if (!tmpServices.Contains(service))
                {
                    tmpServices.Add(service);
                }
                else
                {
                    MessageBox.Show("You have already selected this service!");
                }
            }
        }

        private async void searchBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtNameService.Text != null)
            {
                listServices.ItemsSource = await serviceService.GetServicesAsync(txtNameService.Text, "Active");
            }
        }
        // List services logic end -->

        // <-- Booking logic start
        private void bookingTime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void deleteBookingService_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;
            if (btn?.Tag is Service service)
            {
                if (tmpServices.Contains(service))
                {
                    tmpServices.Remove(service);
                }
            }
            listBookingServices.Items.Refresh();
            tmpTotal.Text = $"Total: \t{CaculateTotal()} $";
        }

        private decimal CaculateTotal()
        {
            decimal total = 0;
            foreach (var s in tmpServices)
            {
                total += s.Price;
            }
            return total;
        }

        private async void Booking_Click(object sender, RoutedEventArgs e)
        {
            if (LoginWindow.USER != null)
            {
                if (tmpServices.Count == 0)
                {
                    MessageBox.Show("Please select service!");
                    return;
                }
                var day = bookingDay.SelectedDate;
                var time = bookingTime.SelectedTime;

                if (time == null || day == null)
                {
                    MessageBox.Show("Please choose a valid time!");
                    return;
                }
                DateTime bookingDate = day.Value.Date.Add(time.Value.TimeOfDay);

                if (bookingDate <= DateTime.Now)
                {
                    MessageBox.Show("Please choose a valid time!");
                    return;
                }

                if (await appointmentService.IsValidBookingAsync(LoginWindow.USER.UserId, bookingDate))
                {
                    var endTime = CaculateEndtime(bookingDate, tmpServices);
                    if (endTime.TimeOfDay >= TimeSpan.FromHours(21) || endTime.Date > bookingDate.Date)
                    {
                        MessageBox.Show("Please remove selected services or schedule another time as we do not serve past 9pm!");
                        return;
                    }
                    var tmpAppoitment = new Appointment()
                    {
                        UserId = LoginWindow.USER.UserId,
                        Total = CaculateTotal(),
                        StartTime = bookingDate,
                        EndTime = endTime,
                        CreateDate = DateTime.Now,
                        Status = "Pending"
                    };
                    tmpAppoitment = await appointmentService.AddAppointmentAsync(tmpAppoitment);

                    foreach (var s in tmpServices)
                    {
                        var tmpAppointmentDetail = new AppointmentDetail()
                        {
                            AppointmentId = tmpAppoitment.AppointmentId,
                            ServiceId = s.ServiceId
                        };
                        await appointmentDetailService.AddAppointmentDetailAsync(tmpAppointmentDetail);
                    }
                    tmpServices.Clear();
                    MessageBox.Show("Booking successfully!");
                    listBookingServices.Items.Refresh();
                    tmpTotal.Text = $"Total: \t{CaculateTotal()} $";
                }
            }
        }

        private DateTime CaculateEndtime(DateTime startTime, List<Service> services)
        {
            foreach (var s in services)
            {
                startTime = startTime.AddMinutes(s.Duration + 10);
            }

            return startTime;
        }
        // Booking logic end -->

        // <-- Profile table logic start
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
                LoginWindow.USER = await userService.ChangePasswordAsync(LoginWindow.USER.Email, txtNewProfilePass.Password);
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
        private void ViewHomePage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homeTable.Visibility = Visibility.Visible;
            servicesTable.Visibility = Visibility.Collapsed;
            bookingTable.Visibility = Visibility.Collapsed;
            appointmentTable.Visibility = Visibility.Collapsed;
            profileTable.Visibility = Visibility.Collapsed;
        }

        private async void viewListServices_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homeTable.Visibility = Visibility.Collapsed;
            servicesTable.Visibility = Visibility.Visible;
            bookingTable.Visibility = Visibility.Collapsed;
            appointmentTable.Visibility = Visibility.Collapsed;
            profileTable.Visibility = Visibility.Collapsed;

            listServices.ItemsSource = await serviceService.GetServicesAsync("", "Active");
        }

        private void viewBooking_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homeTable.Visibility = Visibility.Collapsed;
            servicesTable.Visibility = Visibility.Collapsed;
            bookingTable.Visibility = Visibility.Visible;
            appointmentTable.Visibility = Visibility.Collapsed;
            profileTable.Visibility = Visibility.Collapsed;

            listBookingServices.ItemsSource = tmpServices;
            listBookingServices.Items.Refresh();
            tmpTotal.Text = $"Total: \t{CaculateTotal()} $";
        }

        private void ViewCalendar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homeTable.Visibility = Visibility.Collapsed;
            servicesTable.Visibility = Visibility.Collapsed;
            bookingTable.Visibility = Visibility.Collapsed;
            appointmentTable.Visibility = Visibility.Visible;
            profileTable.Visibility = Visibility.Collapsed;

            LoadUserAppointment();
        }

        private void ViewProfile_MouseUp(object sender, MouseButtonEventArgs e)
        {
            homeTable.Visibility = Visibility.Collapsed;
            servicesTable.Visibility = Visibility.Collapsed;
            bookingTable.Visibility = Visibility.Collapsed;
            appointmentTable.Visibility = Visibility.Collapsed;
            profileTable.Visibility = Visibility.Visible;

            LoadProfile();
            UnactiveTextBox();
        }

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

        private void textNameService_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNameService.Focus();
        }

        private void txtNameService_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNameService.Text) && txtNameService.Text.Length > 0)
            {
                textNameService.Visibility = Visibility.Collapsed;
            }
            else
            {
                textNameService.Visibility = Visibility.Visible;
            }
        }
        // Style end -->
    }
}
