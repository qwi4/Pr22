using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Pr22.Pages
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Services : Window
    {
        BeautySaloonEntities2 dbBeautySaloon = BeautySaloonEntities2.GetContext();
        
        public Services()
        {
            InitializeComponent();

            dbBeautySaloon.Service.Load();
            lvService.ItemsSource = dbBeautySaloon.Service.Local.ToBindingList();

            if (Data.Rights == "Admin") ;
            else
            {
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddService addService = new AddService();
            addService.ShowDialog();
            lvService.Focus();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int indexRow = lvService.SelectedIndex;
            if (indexRow != -1)
            {
                Service row = (Service)lvService.Items[indexRow];
                Data.Id = row.Code;

                EditService editService = new EditService();
                editService.ShowDialog();
                lvService.Items.Refresh();
                lvService.Focus();
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
                    Service row = (Service)lvService.SelectedValue;
                    dbBeautySaloon.Service.Remove(row);
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
                    foreach (var item in lvService.Items)
                    {
                        if (((Service)item).Name.Contains(txtbxSearch.Text))
                        {
                            lvService.SelectedItem = item;
                            lvService.ScrollIntoView(item);
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
                    var service = dbBeautySaloon.Service.ToList();
                    var filtered = service.Where(p => p.Name == txtbxSearch.Text);

                    lvService.ItemsSource = filtered;
                }
                else lvService.ItemsSource = dbBeautySaloon.Service.Local.ToBindingList();
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

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            txtbxSearch.Clear();
        }
    }
}
