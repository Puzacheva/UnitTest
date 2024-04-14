using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public bool Reg(string FIO, string Login, string Password, string gender, string Role, string Tel, string Foto)
        {

            if (string.IsNullOrEmpty(FIO) || (string.IsNullOrEmpty(Login)) || (string.IsNullOrEmpty(Password))
            || (W.IsChecked == false && M.IsChecked == false) || (ComboBoxRole.SelectedItem == null) || (string.IsNullOrEmpty(Tel))
            || (string.IsNullOrEmpty(Foto)))
            {
                MessageBox.Show("Все поля обязательны для ввода!");
                return false;
            }

            Entities2 db = new Entities2();
            string enteredLogin = Login;
            bool isLoginExists = db.User.Any(u => u.Login == enteredLogin);

            if (isLoginExists)
            {
                MessageBox.Show("Пользователь с таким логином уже существует. Пожалуйста, выберите другой логин.");
                return false;
            }

            /*string gender = "";

            if (W.IsChecked == true)
            {
                gender = "Женский";
            }
            else if (M.IsChecked == true)
            {
                gender = "Мужской";
            }*/

            User userObject = new User
            {
                FIO = FIO,
                Login = Login,
                Password = Password,
                Gender = gender,
                Role = Role,
                PhoneNumber = Tel,
                Photo = Foto
            };
            db.User.Add(userObject);
            db.SaveChanges();
            MessageBox.Show("Пользователь успешно зарегистрирован.");
            return true;

        }

        public void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            string gender = "";

            if (W.IsChecked == true)
            {
                gender = "Женский";
            }
            else if (M.IsChecked == true)
            {
                gender = "Мужской";
            }

            /*string Role = null;*/
            if (ComboBoxRole.SelectedItem != null)
            {
                string selectedRole = ComboBoxRole.SelectedItem.ToString();
                Reg(FIO.Text, Login.Text, Password.Text, gender, selectedRole, Tel.Text, Foto.Text);
            }
            else
            {
                MessageBox.Show("Все поля обязательны для ввода!");
            }
            return;

            //Reg(FIO.Text, Login.Text, Password.Text, gender, Role, Tel.Text, Foto.Text);//
        }

        private void Tel_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsDigit(e.Text);
        }

        private void Tel_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = Tel.Text;
            if (text.Length == 3)
            {
                Tel.Text = "(" + text + ") ";
                Tel.SelectionStart = Tel.Text.Length;
            }
            else if (text.Length == 9)
            {
                Tel.Text = text + "-";
                Tel.SelectionStart = Tel.Text.Length;
            }
        }
        private bool IsDigit(string text)
        {
            return Regex.IsMatch(text, "[0-9]");
        }

        private void ComboBoxRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LabelRole.Visibility = Visibility.Collapsed;
        }
    }
}
