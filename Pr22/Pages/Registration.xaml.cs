using System;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;

namespace Pr22.Pages
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Registration : Window
    {
        BeautySaloonEntities2 dbBeautySaloon = BeautySaloonEntities2.GetContext();

        public Registration()
        {
            InitializeComponent();

            dbBeautySaloon.Reg.Load();
            lvReg.ItemsSource = dbBeautySaloon.Reg.Local.ToBindingList();

            if (Data.Rights == "Admin") ;
            else
            {
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddRegistration addRegistration = new AddRegistration();
            addRegistration.ShowDialog();
            lvReg.Focus();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int indexRow = lvReg.SelectedIndex;
            if (indexRow != -1)
            {
                Reg row = (Reg)lvReg.Items[indexRow];
                Data.Id = row.Id;

                EditRegistration editRegistration = new EditRegistration();
                editRegistration.ShowDialog();
                lvReg.Items.Refresh();
                lvReg.Focus();
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
                    Reg row = (Reg)lvReg.SelectedValue;
                    dbBeautySaloon.Reg.Remove(row);
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
                    foreach (var item in lvReg.Items)
                    {
                        if (((Reg)item).Id.ToString().Contains(txtbxSearch.Text))
                        {
                            lvReg.SelectedItem = item;
                            lvReg.ScrollIntoView(item);
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
                    var reg = dbBeautySaloon.Reg.ToList();
                    var filtered = reg.Where(p => p.Id.ToString() == txtbxSearch.Text);

                    lvReg.ItemsSource = filtered;
                }
                else lvReg.ItemsSource = dbBeautySaloon.Reg.Local.ToBindingList();
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
