using System;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Pr22.Pages
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        BeautySaloonEntities2 dbBeautySaloon = BeautySaloonEntities2.GetContext();

        public Clients()
        {
            InitializeComponent();

            dbBeautySaloon.Client.Load();
            lvClient.ItemsSource = dbBeautySaloon.Client.Local.ToBindingList();

            if (Data.Rights == "Admin") ;
            else
            {
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
            }

            BitmapImage tet = new BitmapImage(new Uri(dbBeautySaloon.Client.ToList()[0].Photo));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.ShowDialog();
            lvClient.Focus();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int indexRow = lvClient.SelectedIndex;
            if (indexRow != -1)
            {
                Client row = (Client)lvClient.Items[indexRow];
                Data.Id = row.CardNumber;

                EditClient editClient = new EditClient();
                editClient.ShowDialog();
                lvClient.Items.Refresh();
                lvClient.Focus();
            }
            else MessageBox.Show("Выберите элемент",
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?",
                "Удаление записи",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Client row = (Client)lvClient.SelectedValue;
                    dbBeautySaloon.Client.Remove(row);
                    dbBeautySaloon.SaveChanges();
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выберите запись",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand);
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtbxSearch.Text.Length != 0)
            {
                try
                {
                    foreach (var item in lvClient.Items)
                    {
                        if (((Client)item).Surname.Contains(txtbxSearch.Text))
                        {
                            lvClient.SelectedItem = item;
                            lvClient.ScrollIntoView(item);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Введите атрибут поиска");
        }

        private void btnFiltr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtbxSearch.Text.Length > 0)
                {
                    var client = dbBeautySaloon.Client.ToList();
                    var filtered = client.Where(p => p.Surname == txtbxSearch.Text);

                    lvClient.ItemsSource = filtered;
                }
                else lvClient.ItemsSource = dbBeautySaloon.Client.Local.ToBindingList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            txtbxSearch.Clear();
        }
    }
}
