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
    /// Логика взаимодействия для TicketEditPage.xaml
    /// </summary>
    public partial class TicketEditPage : Page
    {
        Tickets _currentTickets = new Tickets();
        public TicketEditPage(Tickets selectedTickets)
        {
            InitializeComponent();

            if (selectedTickets != null )
                _currentTickets = selectedTickets;

            DataContext = _currentTickets;

            cbMovie.ItemsSource = CinemaEntities.GetContext().Movies.ToList();
            cbHall.ItemsSource = CinemaEntities.GetContext().Halls.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentTickets.Date == null || _currentTickets.Date < DateTime.Today)
                errors.AppendLine("Введите дату билета!");
             if (_currentTickets.Time == null)
                errors.AppendLine("Введите время билета!");
            if (_currentTickets.Movies == null)
                errors.AppendLine("Укажите фильм!");           
            if (_currentTickets.Halls == null)
                errors.AppendLine("Укажите зал!");
            if (_currentTickets.Row < 1 || _currentTickets.Row > 2)
                errors.AppendLine("Введите ряд!");
            if (_currentTickets.Seat < 1 || _currentTickets.Seat > 10)
                errors.AppendLine("Введите место!");
            if (_currentTickets.Price < 1)
                errors.AppendLine("Введите цену билета!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentTickets.ID == 0)
                CinemaEntities.GetContext().Tickets.Add(_currentTickets);

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
