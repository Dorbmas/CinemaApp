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
    /// Логика взаимодействия для AdminHallsPage.xaml
    /// </summary>
    public partial class AdminHallsPage : Page
    {
        public AdminHallsPage()
        {
            InitializeComponent();

            HallUpdate();
        }

        public void HallUpdate()
        {
            var hallsList = CinemaEntities.GetContext().Halls.ToList();

            lvHalls.ItemsSource = hallsList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new HallEditPage(null));
        }

        private void btdDelete_Click(object sender, RoutedEventArgs e)
        {
            var hallsForRemoving = lvHalls.SelectedItems.Cast<Halls>().ToList();

            if (MessageBox.Show($"Вы уверены, что хотите удалить следующие залы: {hallsForRemoving.Count}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CinemaEntities.GetContext().Halls.RemoveRange(hallsForRemoving);
                    CinemaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    HallUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new HallEditPage((sender as Button).DataContext as Halls));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CinemaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                HallUpdate();
            }
        }
    }
}
