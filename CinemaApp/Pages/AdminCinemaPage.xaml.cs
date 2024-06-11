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
    /// Логика взаимодействия для AdminCinemaPage.xaml
    /// </summary>
    public partial class AdminCinemaPage : Page
    {
        public AdminCinemaPage()
        {
            InitializeComponent();
            CinemaUpdate();
        }

        public void CinemaUpdate()
        {
            var cinemaList = CinemaEntities.GetContext().Movies.ToList();

            lvMovies.ItemsSource = cinemaList;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CinemaEditPage((sender as Button).DataContext as Movies));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CinemaEditPage(null));
        }

        private void btdDelete_Click(object sender, RoutedEventArgs e)
        {
            var moviesForRemoving = lvMovies.SelectedItems.Cast<Movies>().ToList();

            if (MessageBox.Show($"Вы уверены, что хотите удалить следующие фильмы: {moviesForRemoving.Count}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CinemaEntities.GetContext().Movies.RemoveRange(moviesForRemoving);
                    CinemaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    CinemaUpdate();
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
                CinemaUpdate();
            }
        }
    }
}
