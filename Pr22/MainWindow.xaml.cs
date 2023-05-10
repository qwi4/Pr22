using Pr22.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pr22
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
       // public string Photo { get; set; }

        public MainWindow()
        {
            InitializeComponent();

        }

       // Window_Closing(object sender, System.Compo)
       /* public string PhotoFull
        {
            get
            {
                if (this.Photo == null)
                {
                    return null;
                }
                else
                {
                    string namePhoto = Directory.GetCurrentDirectory() + "\\image\\" + Photo;
                    return namePhoto;
                }
            }
        }*/

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SignIn signIn = new SignIn();
            signIn.Owner = this;
            signIn.ShowDialog();
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            Clients clients = new Clients();
            clients.ShowDialog();
        }

        private void btnServices_Click(object sender, RoutedEventArgs e)
        {
            Services services = new Services();
            services.ShowDialog();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();
        }

        private void btnSignOff_Click(object sender, RoutedEventArgs e)
        {
            SignIn signIn = new SignIn();
            signIn.ShowDialog();
        }
    }
}
