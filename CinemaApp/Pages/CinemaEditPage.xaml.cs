using CinemaApp.Classes;
using CinemaApp.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для CinemaEditPage.xaml
    /// </summary>
    public partial class CinemaEditPage : Page
    {
        Movies _currentMovie = new Movies();
        byte[] imageData;
        public CinemaEditPage(Movies selectedMovie)
        {
            InitializeComponent();

            if (selectedMovie != null)
                _currentMovie = selectedMovie;

            DataContext = _currentMovie;

            cbCountry.ItemsSource = CinemaEntities.GetContext().Countries.ToList();
            cbGenre.ItemsSource = CinemaEntities.GetContext().Genres.ToList();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображение (*.png, *.jpg, *.jpeg) |*.png; *.jpg; *.jpeg";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                imageData = File.ReadAllBytes(openFileDialog.FileName);
                imgImage.Source = new ImageSourceConverter()
                    .ConvertFrom(imageData) as ImageSource;

                _currentMovie.Image = imageData;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentMovie.Title))
                errors.AppendLine("Введите название фильма!");
            if (_currentMovie.YearOfIssue < 1900 || _currentMovie.YearOfIssue > 2024)
                errors.AppendLine("Введите корректную дату выпуска фильма!");
            if (_currentMovie.Countries == null)
                errors.AppendLine("Укажите страну-производитель фильма!");
            if (_currentMovie.Genres == null)
                errors.AppendLine("Укажите жанр фильма!");
            if (string.IsNullOrEmpty(_currentMovie.Duration))
                errors.AppendLine("Введите длительность фильма!");
            if (string.IsNullOrEmpty(_currentMovie.AgeRating))
                errors.AppendLine("Введите возрастное ограничение фильма!");
            if (string.IsNullOrEmpty(_currentMovie.Description))
                errors.AppendLine("Введите описание фильма!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentMovie.ID == 0)
                CinemaEntities.GetContext().Movies.Add(_currentMovie);

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
