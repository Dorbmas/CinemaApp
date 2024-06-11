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
    /// Логика взаимодействия для GenreEditPage.xaml
    /// </summary>
    public partial class GenreEditPage : Page
    {
        Genres _currentGenres = new Genres();
        public GenreEditPage(Genres selectedGenres)
        {
            InitializeComponent();

            if (selectedGenres != null)
                _currentGenres = selectedGenres;

            DataContext = _currentGenres;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentGenres.Genre))
                errors.AppendLine("Введите жанр!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            
            if (_currentGenres.ID == 0)
                CinemaEntities.GetContext().Genres.Add(_currentGenres);

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
