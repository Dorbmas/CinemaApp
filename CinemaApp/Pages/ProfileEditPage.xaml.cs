using CinemaApp.Classes;
using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для ProfileEditPage.xaml
    /// </summary>
    public partial class ProfileEditPage : Page
    {
        Users _currentUsers = new Users();
        Regex regex = new Regex(@"^[а-яА-Я\s-]+$");
        int UserID;
        int RoleID;    
        public ProfileEditPage(Users users, int userID, int roleID)
        {
            InitializeComponent();

            UserID = userID;
            RoleID = roleID;

            if (users != null )
                _currentUsers = users;

            DataContext = _currentUsers;

            if (RoleID != 1)
            {
                spRole.Visibility = Visibility.Hidden;
                tbLogin.IsEnabled = false;
            }

            cbRole.ItemsSource = CinemaEntities.GetContext().Roles.ToList();
        }

        private void tbPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pbPassword.Password != tbPassword.Text)
            {
                btnSave.IsEnabled = false;
                pbPassword.Background = Brushes.LightCoral;
                pbPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                btnSave.IsEnabled = true;
                pbPassword.Background = Brushes.LightGreen;
                pbPassword.BorderBrush = Brushes.Green;
            }
        }

        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbPassword.Password != tbPassword.Text)
            {
                btnSave.IsEnabled = false;
                pbPassword.Background = Brushes.LightCoral;
                pbPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                btnSave.IsEnabled = true;
                pbPassword.Background = Brushes.LightGreen;
                pbPassword.BorderBrush = Brushes.Green;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            bool isValidLastname = regex.IsMatch(tbLastname.Text);
            if (string.IsNullOrEmpty(_currentUsers.Lastname) || isValidLastname == false)
                errors.AppendLine("Введите фамилию!");
            bool isValidName = regex.IsMatch(tbName.Text);
            if (string.IsNullOrEmpty(_currentUsers.Name) || isValidName == false)
                errors.AppendLine("Введите имя!");
            bool isValidSurname = regex.IsMatch(tbSurname.Text);
            if (tbSurname.Text != "" && isValidSurname == false)
                errors.AppendLine("Введите отчество!");
            if (string.IsNullOrEmpty(_currentUsers.Login))
                errors.AppendLine("Введите логин!");
            if (string.IsNullOrEmpty(_currentUsers.Password))
                errors.AppendLine("Введите пароль!");
            if (_currentUsers.Roles == null)
                errors.AppendLine("Укажите роль");
            if (tbLogin.Text.Length < 6)
                errors.AppendLine("Логин должен состоят минимум из 6 символов!");
            if (tbPassword.Text.Length < 6)
                errors.AppendLine("Пароль должен состоят минимум из 6 символов!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUsers.ID == 0)
                CinemaEntities.GetContext().Users.Add(_currentUsers);

            try
            {
                CinemaEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
