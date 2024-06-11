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
    /// Логика взаимодействия для AdminUsersPage.xaml
    /// </summary>
    public partial class AdminUsersPage : Page
    {
        int UserID;
        int RoleID;
        public AdminUsersPage(int userID, int roleID)
        {
            InitializeComponent();
            UsersUpdate();
            UserID = userID;
            RoleID = roleID;
        }

        public void UsersUpdate()
        {
            var usersList = CinemaEntities.GetContext().Users.ToList();

            lvUsers.ItemsSource = usersList.Where(x => x.RoleID != 1);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UserEditPage(null, UserID, RoleID));
        }

        private void btdDelete_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = lvUsers.SelectedItems.Cast<Users>().ToList();

            if (MessageBox.Show($"Вы уверены, что хотите удалить следующих пользователей: {usersForRemoving.Count}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CinemaEntities.GetContext().Users.RemoveRange(usersForRemoving);
                    CinemaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    UsersUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UserEditPage((sender as Button).DataContext as Users, UserID, RoleID));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CinemaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                UsersUpdate();
            }
        }
    }
}
