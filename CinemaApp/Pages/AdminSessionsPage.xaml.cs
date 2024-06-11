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
    /// Логика взаимодействия для AdminSessionsPage.xaml
    /// </summary>
    public partial class AdminSessionsPage : Page
    {
        public AdminSessionsPage()
        {
            InitializeComponent();

            SessionUpdate();
        }

        public void SessionUpdate()
        {
            var sessionList = CinemaEntities.GetContext().Sessions.ToList();

            lvSessions.ItemsSource = sessionList;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SessionEditPage((sender as Button).DataContext as Sessions));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SessionEditPage(null));
        }

        private void btdDelete_Click(object sender, RoutedEventArgs e)
        {
            var sessionsForRemoving = lvSessions.SelectedItems.Cast<Sessions>().ToList();

            if (MessageBox.Show($"Вы уверены, что хотите удалить следующие сеансы, {sessionsForRemoving.Count}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CinemaEntities.GetContext().Sessions.RemoveRange(sessionsForRemoving);
                    CinemaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    SessionUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CinemaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                SessionUpdate();
            }
        }
    }
}
