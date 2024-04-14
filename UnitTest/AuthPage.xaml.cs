using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO.Packaging;
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

namespace UnitTest
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            Auth(TextBoxLogin.Text, TextBoxPassword.Text);

        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("RegPage.xaml", UriKind.Relative));
        }

        private int failedAttempts = 0;
        public bool Auth(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return false;
            }

            using (var db = new Entities2())
            {
                var user = db.User
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден");
                    failedAttempts++;

                    if (failedAttempts >= 3)
                    {
                        // Открытие окна капчи
                        CaptchaWindow captchaWindow = new CaptchaWindow();
                        captchaWindow.ShowDialog();
                        failedAttempts = 0;
                    }

                    return false;
                }

                if (user.Role == "Удален")
                {
                    MessageBox.Show("Пользователь с такими данными удалён");
                    return false;
                }
                MessageBox.Show($"Здравствуйте, {user.Role} {user.FIO}!");
                TextBoxLogin.Clear();
                TextBoxPassword.Clear();

                return true;

            }
        }
    }
}
