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
    public partial class AddClient : Window
    {
        BeautySaloonEntities2 dbBeautySaloon = BeautySaloonEntities2.GetContext();
        Client _client = new Client();
        OpenFileDialog open = new OpenFileDialog();

        public AddClient()
        {
            InitializeComponent();
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            open.Filter = "Все файлы |*.*| Файлы *.jpg|*.jpg";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
            {
                BitmapImage photoImage = new BitmapImage(new Uri(open.FileName));
                imgPhoto.Source = photoImage;
            }
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (txtbxSurname.Text.Length == 0) errors.AppendLine("Введите фамилию");
            if (dpBirth.Text.Length == 0) errors.AppendLine("Введите дату рождения");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _client.Surname = txtbxSurname.Text;
            _client.DateOfBirth = Convert.ToDateTime(dpBirth.Text);
            _client.Address = txtbxAddress.Text;
            _client.PhoneNumber = txtbxPhone.Text;
            

            if (open.SafeFileName.Length != 0)
            {
                string newPhotoName = Directory.GetCurrentDirectory()
                    + "\\image\\" + open.SafeFileName;
                File.Copy(open.FileName, newPhotoName, true);
                _client.Photo = open.SafeFileName;
            }

            try
            {
                dbBeautySaloon.Client.Add(_client);
                dbBeautySaloon.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                dbBeautySaloon.Client.Remove(_client);
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
