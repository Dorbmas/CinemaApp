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
    /// Логика взаимодействия для AdminTicketsPage.xaml
    /// </summary>
    public partial class AdminTicketsPage : Page
    {
        public AdminTicketsPage()
        {
            InitializeComponent();
        }

        public void TicketsUpdate()
        {
            var ticketsList = CinemaEntities.GetContext().Tickets.ToList();

            lvTickets.ItemsSource = ticketsList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TicketEditPage(null));
        }

        private void btdDelete_Click(object sender, RoutedEventArgs e)
        {
            var ticketsForRemoving = lvTickets.SelectedItems.Cast<Tickets>().ToList();

            if (MessageBox.Show($"Вы уверены, что хотите удалить следующие билеты: {ticketsForRemoving.Count}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CinemaEntities.GetContext().Tickets.RemoveRange(ticketsForRemoving);
                    CinemaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    TicketsUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TicketEditPage((sender as Button).DataContext as Tickets));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CinemaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                TicketsUpdate();
            }
        }
    }
}
