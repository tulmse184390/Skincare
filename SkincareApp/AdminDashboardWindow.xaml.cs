using System;
using System.Collections.Generic;
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

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e) {

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
