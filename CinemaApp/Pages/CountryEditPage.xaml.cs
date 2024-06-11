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
    /// Логика взаимодействия для CountryEditPage.xaml
    /// </summary>
    public partial class CountryEditPage : Page
    {
        Countries _currentCountries = new Countries();
        public CountryEditPage(Countries selectedCountries)
        {
            InitializeComponent();

            if (selectedCountries != null)
                _currentCountries = selectedCountries;

            DataContext = _currentCountries;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentCountries.Country))
                errors.AppendLine("Введите страну!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentCountries.ID == 0)
                CinemaEntities.GetContext().Countries.Add(_currentCountries);

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
