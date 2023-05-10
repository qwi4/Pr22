using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pr22
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        //Таймер
        DispatcherTimer _timer;
        //Счетчик попыток входа
        int _countLogin = 1;
        // TenisEntities _dbTenis = new TenisEntities();


        //получаем доступ к контексту данных
        BeautySaloonEntities2 dbBeautySaloon = BeautySaloonEntities2.GetContext();

        private DataTable User;
        //Строка подключения к БД
        private readonly string connectionString = "Data Source=(local);Initial Catalog=BeautySaloon;Integrated Security=True";

        void GetCaptcha()
        {
            string masChar = "QWERTYUIOPLKJHGFDSAZXCVBNM" +
                "mnbvcxzlkjhgfdsapoiuytrewq1234567890";
            string captcha = "";
            Random rnd = new Random();

            for (int i = 1; i < 6; i++)
            {
                captcha = captcha + masChar[rnd.Next(0, masChar.Length)];
            }

            txtxblcCaptcha.Visibility = Visibility.Visible;
            txtbxCaptcha.Visibility = Visibility.Visible;
            line.Visibility = Visibility.Visible;
            txtxblcCaptcha.Text = captcha;
            txtbxCaptcha.Text = null;
            txtxblcCaptcha.LayoutTransform = new RotateTransform(rnd.Next(-15, 15));

            line.X1 = 10;
            line.Y1 = rnd.Next(10, 40);
            line.X2 = 280;
            line.Y2 = rnd.Next(10, 40);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            txtbxLogin.Focus();
            Data.Login = false;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += new EventHandler(timer_Tick);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            grdReg.IsEnabled = true;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            //Ищем запись с заданными логином и паролем через LINQ
            var user = from p in dbBeautySaloon.User
                       where p.UserLogin == txtbxLogin.Text &&
                       p.UserPassword == txtbxPassword.Text
                       select p;
            //Если запись найдена
            if (user.Count() == 1 && txtbxCaptcha.Text == txtbxCaptcha.Text)
            {
                //запоминаем данные
                Data.Login = true;
                Data.Surname = user.First().UserSurname;
                Data.Firstname = user.First().UserName;
                Data.Patronymic = user.First().UserPatroymic;
                Data.Rights = user.First().Role.RoleName;
                Close();
            }
            else
            {
                if (user.Count() == 1)
                {
                    MessageBox.Show("Капча введена неверно, повторите ввод",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Ошибка в логине или пароле, повторите попытку",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                GetCaptcha();

                if (_countLogin >= 2)
                {
                    grdReg.IsEnabled = false;
                    _timer.Start();
                }
                _countLogin++;
                txtbxLogin.Focus();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEnterG_Click(object sender, RoutedEventArgs e)
        {
            Data.Login = true;
            Data.Surname = "Гость";
            Data.Firstname = "";
            Data.Patronymic = "";
            Data.Rights = "Клиент";
            Close();
        }

       
        }
}
