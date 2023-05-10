using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace Pr22.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddRegistration : Window
    {
        BeautySaloonEntities2 dbBeautySaloon = BeautySaloonEntities2.GetContext();
        Reg _serviceRegistration = new Reg();
       

        public AddRegistration()
        {
            InitializeComponent();
            txtbxClient.ItemsSource = dbBeautySaloon.Client.ToList();
            txtbxClient.DisplayMemberPath = "CardNumber";

            txtbxService.ItemsSource = dbBeautySaloon.Service.ToList();
            txtbxService.DisplayMemberPath = "Code";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (txtbxClient.Text.Length == 0) errors.AppendLine("Выберите номер клиента");
            if (txtbxService.Text.Length == 0) errors.AppendLine("Выберите номер услуги");
            if (dpDate.Text.Length == 0) errors.AppendLine("Выберите дату");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _serviceRegistration.Client = Convert.ToInt32(txtbxClient.Text);
            _serviceRegistration.Service = Convert.ToInt32(txtbxService.Text);
            _serviceRegistration.Date = Convert.ToDateTime(dpDate.Text);

            try
            {
                dbBeautySaloon.Reg.Add(_serviceRegistration);
                dbBeautySaloon.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                dbBeautySaloon.Reg.Remove(_serviceRegistration);
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
