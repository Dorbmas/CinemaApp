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
    /// Логика взаимодействия для AdminCountryPage.xaml
    /// </summary>
    public partial class AdminCountryPage : Page
    {
        public AdminCountryPage()
        {
            InitializeComponent();

            CountryUpdate();
        }

        public void CountryUpdate()
        {
            var countriesList = CinemaEntities.GetContext().Countries.ToList();
            
            lvCountries.ItemsSource = countriesList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CountryEditPage(null));
        }

        private void btdDelete_Click(object sender, RoutedEventArgs e)
        {
            var countriesForRemoving = lvCountries.SelectedItems.Cast<Countries>().ToList();

            if (MessageBox.Show($"Вы уверены, что хотите удалить следующие страны: {countriesForRemoving.Count}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CinemaEntities.GetContext().Countries.RemoveRange(countriesForRemoving);
                    CinemaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    CountryUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CountryEditPage((sender as Button).DataContext as Countries));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CinemaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                CountryUpdate();
            }
        }
    }
}
