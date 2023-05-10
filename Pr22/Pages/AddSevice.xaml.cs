using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Pr22.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddService : Window
    {
        BeautySaloonEntities2 dbBeautySaloon = BeautySaloonEntities2.GetContext();
        Service _service = new Service();
       

        public AddService()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (txtbxName.Text.Length == 0) errors.AppendLine("Введите название");
            if (txtbxPrice.Text.Length == 0) errors.AppendLine("Введите цену");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _service.Name = txtbxName.Text;
            _service.Price = Convert.ToInt32(txtbxPrice.Text);

            try
            {
                dbBeautySaloon.Service.Add(_service);
                dbBeautySaloon.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                dbBeautySaloon.Service.Remove(_service);
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
