using CinemaApp.Classes;
using CinemaApp.Models;
using CinemaApp.Pages;
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
using System.Windows.Shapes;

namespace CinemaApp
{
    /// <summary>
    /// Логика взаимодействия для CinemaWindow.xaml
    /// </summary>
    public partial class CinemaWindow : Window
    {
        int UserID;
        int RoleID;
        public CinemaWindow(int userID, int roleID)
        {
            InitializeComponent();
            
            UserID = userID;
            RoleID = roleID;

            Manager.MainFrame = MainFrame;
         
            
            switch (RoleID)
            {
                case 0:
                    MainFrame.Navigate(new CinemaPage(0, 0));
                    break;
                case 1:
                    MainFrame.Navigate(new AdminPage(UserID, RoleID));
                    break;
                case 2:
                    MainFrame.Navigate(new CinemaPage(UserID, RoleID));
                    break;
                case 3:
                    MainFrame.Navigate(new CinemaPage(UserID, RoleID));
                    break;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this).Close();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (Manager.MainFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Hidden;
                btnProfile.Visibility = Visibility.Hidden;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
                btnExit.Visibility = Visibility.Visible;
                if (RoleID == 0)
                {
                    btnProfile.Visibility = Visibility.Hidden;
                }
                else
                {
                    btnProfile.Visibility = Visibility.Visible;
                }              
            }
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            var user = CinemaEntities.GetContext().Users.FirstOrDefault(x => x.ID == UserID);

            MainFrame.Navigate(new ProfileEditPage(user, UserID, RoleID));
        }
    }
}
