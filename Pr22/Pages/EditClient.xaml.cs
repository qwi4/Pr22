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
    public partial class EditClient : Window
    {
        BeautySaloonEntities2 dbBeautySaloon = BeautySaloonEntities2.GetContext();
        Client _client = new Client();
        OpenFileDialog open = new OpenFileDialog();

        public EditClient()
        {
            InitializeComponent();

            _client = dbBeautySaloon.Client.Find(Data.Id);

            txtbxSurname.Text = _client.Surname;
            dpBirth.SelectedDate = _client.DateOfBirth;
            txtbxAddress.Text = _client.Address;
            txtbxPhone.Text = _client.PhoneNumber;

            if (_client.Photo != null)
            {
                BitmapImage photoImage = new BitmapImage(new Uri(_client.Photo));
                imgPhoto.Source = photoImage;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSaveClient_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (txtbxSurname.Text.Length ==0) errors.AppendLine("Введите фамлию");
            if (dpBirth.Text.Length == 0) errors.AppendLine("Введите дату");

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
                if (open.SafeFileName != _client.Photo)
                {
                    string newNamePhoto = Directory.GetCurrentDirectory() +
                        "\\image\\" + open.SafeFileName;
                    File.Copy(open.FileName, newNamePhoto, true);
                    _client.Photo = open.SafeFileName;
                }
            }

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
