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
    /// Логика взаимодействия для AdminGenresPage.xaml
    /// </summary>
    public partial class AdminGenresPage : Page
    {
        public AdminGenresPage()
        {
            InitializeComponent();
        }

        public void GenresUpdate()
        {
            var genresList = CinemaEntities.GetContext().Genres.ToList();

            lvGenres.ItemsSource = genresList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new GenreEditPage(null));
        }

        private void btdDelete_Click(object sender, RoutedEventArgs e)
        {
            var genresForRemoving = lvGenres.SelectedItems.Cast<Genres>().ToList();

            if (MessageBox.Show($"Вы уверены, что хотите удалить следующие жанры: {genresForRemoving.Count}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CinemaEntities.GetContext().Genres.RemoveRange(genresForRemoving);
                    CinemaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    GenresUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new GenreEditPage((sender as Button).DataContext as Genres));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CinemaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                GenresUpdate();
            }
        }
    }
}
