using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL.Services.Implements;
using BLL.Services.Interfaces;
using DAL.Entities;

namespace SkincareApp
{
    /// <summary>
    /// Interaction logic for AdminDashboardWindow.xaml
    /// </summary>
    public partial class AdminDashboardWindow : Window
    {

        private readonly IServiceService _serviceService;

        public AdminDashboardWindow()
        {
            InitializeComponent();
            _serviceService = new ServiceService();
            LoadServices();
        }

        public void LoadServices() {
            var services = _serviceService.GetAllServices();
            dataServices.ItemsSource = services;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            try {
                var service = new Service {
                    ServiceName = serviceNameTxt.Text,
                    Duration = int.Parse(serviceDurationTxt.Text),
                    Price = decimal.Parse(servicePriceTxt.Text),
                    Status = serviceStatusTxt.Text,
                    Description = serviceDescTxt.Text
                };

                Debug.WriteLine(service.ServiceName);

                var result = _serviceService.AddService(service);
                if (result) {
                    MessageBox.Show("Add service successfully!");
                    LoadServices();
                } else {
                    MessageBox.Show("Service name is already exist!");
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {
            try {
                var service = new Service {
                    ServiceId = int.Parse(serviceIdTxt.Text),
                    ServiceName = serviceNameTxt.Text,
                    Duration = int.Parse(serviceDurationTxt.Text),
                    Price = decimal.Parse(servicePriceTxt.Text),
                    Status = serviceStatusTxt.Text,
                    Description = serviceDescTxt.Text
                };

                _serviceService.UpdateService(service);
                MessageBox.Show("Update service successfully!");
                LoadServices();

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            var serviceId = int.Parse(serviceIdTxt.Text);
            _serviceService.DeleteService(serviceId);
            MessageBox.Show("Delete service successfully!");
            LoadServices();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e) {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void dataServices_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var service = dataServices.SelectedItem as Service;
            if (service != null) {
                serviceIdTxt.Text = service.ServiceId + "";
                serviceNameTxt.Text = service.ServiceName;
                serviceDurationTxt.Text = service.Duration + "";
                servicePriceTxt.Text = service.Price + "";
                serviceStatusTxt.Text = service.Status;
                serviceDescTxt.Text = service.Description;
            } else {
                serviceIdTxt.Text = "";
                serviceNameTxt.Text = "";
                serviceDurationTxt.Text = "";
                servicePriceTxt.Text = "";
                serviceStatusTxt.Text = ""; ;
                serviceDescTxt.Text = "";
            }
        }
    }
}
