using System;
using System.Text;
using System.Windows;

namespace Pr22.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class EditService : Window
    {
        BeautySaloonEntities2 dbBeautySaloon = BeautySaloonEntities2.GetContext();
        Service _service = new Service();
       

        public EditService()
        {
            InitializeComponent();
            _service = dbBeautySaloon.Service.Find(Data.Id);

            txtbxName.Text = _service.Name;
            txtbxPrice.Text = Convert.ToString(_service.Price);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEditService_Click(object sender, RoutedEventArgs e)
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
            _service.Price = Convert.ToDecimal(txtbxPrice.Text);

            try
            {
                // dbBeautySaloon.Client.Add(_client);
                dbBeautySaloon.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
