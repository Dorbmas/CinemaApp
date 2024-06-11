using CinemaApp.Classes;
using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CinemaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        Regex regex = new Regex(@"^[а-яА-Я\s-]+$");
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CinemaEntities.GetContext().Users.Count(x => x.Login == tbLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                StringBuilder errors = new StringBuilder();

                bool isValidLastname = regex.IsMatch(tbLastname.Text);
                if (string.IsNullOrEmpty(tbLastname.Text) || isValidLastname == false)
                    errors.AppendLine("Введите фамилию!");
                bool isValidName = regex.IsMatch(tbName.Text);
                if (string.IsNullOrEmpty(tbName.Text) || isValidName == false)
                    errors.AppendLine("Введите имя!");
                bool isValidSurname = regex.IsMatch(tbSurname.Text);
                if (tbSurname.Text != "" && isValidSurname == false)
                    errors.AppendLine("Введите отчество!");
                if (tbLogin.Text.Length < 6)
                    errors.AppendLine("Логин должен состоят минимум из 6 символов");
                if (tbPassword.Text.Length < 6)
                    errors.AppendLine("Пароль должен состоят минимум из 6 символов");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                Users userObj = new Users()
                {
                    Lastname = tbLastname.Text,
                    Name = tbName.Text,
                    Surname = tbSurname.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Text,
                    RoleID = 3
                };
                CinemaEntities.GetContext().Users.Add(userObj);
                CinemaEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbPassword.Password != tbPassword.Text)
            {
                btnCreate.IsEnabled = false;
                pbPassword.Background = Brushes.LightCoral;
                pbPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                btnCreate.IsEnabled = true;
                pbPassword.Background = Brushes.LightGreen;
                pbPassword.BorderBrush = Brushes.Green;
            }
        }

        private void tbPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPassword.Text != pbPassword.Password)
            {
                btnCreate.IsEnabled = false;
                pbPassword.Background = Brushes.LightCoral;
                pbPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                btnCreate.IsEnabled = true;
                pbPassword.Background = Brushes.LightGreen;
                pbPassword.BorderBrush = Brushes.Green;
            }
        }
    }
}
