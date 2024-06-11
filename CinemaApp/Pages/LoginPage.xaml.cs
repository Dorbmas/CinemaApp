using CinemaApp.Classes;
using CinemaApp.Models;
using System;
using System.Collections.Generic;
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

namespace CinemaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = CinemaEntities.GetContext().Users.FirstOrDefault(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Пользователь не обнаружен!", "Ошибка при авторизации!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    CinemaWindow cinemaWindow = new CinemaWindow(userObj.ID, userObj.RoleID);
                    switch (userObj.RoleID)
                    {
                        case 1: MessageBox.Show($"Добро пожаловать, {userObj.Name}!", "Уведомление!",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                            cinemaWindow.Show();
                            Window.GetWindow(this).Close();
                            break;
                        case 2: MessageBox.Show($"Добро пожаловать, {userObj.Name}!", "Уведомление!",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                            cinemaWindow.Show();
                            Window.GetWindow(this).Close();
                            break;
                        case 3: MessageBox.Show($"Добро пожаловать, {userObj.Name}!", "Уведомление!",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                            cinemaWindow.Show();
                            Window.GetWindow(this).Close();
                            break;
                        default: MessageBox.Show("Данные не обнаружены!", "Уведомление",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message} Критическая работа приложения!", "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new RegistrationPage());
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы входите без авторизации. Накопление баллов будет невозможно. Вы хотите продолжить?", "Уведомление",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CinemaWindow cinemaWindow = new CinemaWindow(0, 0);
                cinemaWindow.Show();
                Window.GetWindow(this).Close();
            }
        }
    }
}
