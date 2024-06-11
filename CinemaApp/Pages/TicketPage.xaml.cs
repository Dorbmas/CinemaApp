using CinemaApp.Classes;
using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        RowsAndSeats _currentSeat = new RowsAndSeats();
        Tickets _currentTickets = new Tickets();
        string MovieName;
        DateTime SessionDate;
        TimeSpan SessionTime;
        double Price;
        int MovieID;
        int UserID;
        int RoleID;
        public TicketPage(RowsAndSeats selectedSeat, string movieName, DateTime sessionDate, TimeSpan sessionTime, double price, int movieID, int userID, int roleID)
        {
            InitializeComponent();

            UserID = userID;
            RoleID = roleID;

            if (selectedSeat != null)
                _currentSeat = selectedSeat;

            DataContext = _currentSeat;

            if (RoleID != 3)
            {
                btnPayPoints.Visibility = Visibility.Hidden;
            }

            MovieName = movieName;
            SessionDate = sessionDate;
            SessionTime = sessionTime;
            Price = price;
            MovieID = movieID;

            tbTitle.Text = MovieName;
            tbDate.Text = SessionDate.ToString("dd MMMM");
            tbTime.Text = SessionTime.ToString();
            tbPrice.Text = Price.ToString();
            tbHall.Text = _currentSeat.HallID.ToString();
            tbRow.Text = _currentSeat.Row.ToString();
            tbSeat.Text = _currentSeat.Seat.ToString();           
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();           

            _currentTickets.Date = SessionDate;
            _currentTickets.Time = DateTime.Now.TimeOfDay;
            _currentTickets.HallID = _currentSeat.HallID;
            _currentTickets.Row = _currentSeat.Row;
            _currentTickets.Seat = _currentSeat.Seat;
            _currentTickets.Price = Price;
            _currentTickets.MovieID = MovieID;
            _currentTickets.MovieTime = SessionTime;

            _currentSeat.Status = false;

            var user = CinemaEntities.GetContext().Users.Find(UserID);

            if (RoleID == 3)
            {
                if (user != null)
                {
                    user.Points = (int?)Math.Floor(Price * 0.01);
                    CinemaEntities.GetContext().Users.AddOrUpdate(user);
                    CinemaEntities.GetContext().SaveChanges();
                }
            }
            
            if (_currentTickets.ID == 0)
                CinemaEntities.GetContext().Tickets.Add(_currentTickets);          

            try
            {
                CinemaEntities.GetContext().SaveChanges();
                if (RoleID == 3)
                {
                    MessageBox.Show("Оплата прошла успешно! Вам был начислен кешбэк в размере 1% от стоимости билета");
                }
                else
                {
                    MessageBox.Show("Оплата прошла успешно!");
                }
                if (RoleID == 2)
                {
                    CinemaWindow cinemaWindow = new CinemaWindow(UserID, RoleID);
                    cinemaWindow.Show();
                    Window.GetWindow(this).Close();
                }
                else
                {
                    mainWindow.Show();
                    Window.GetWindow(this).Close();
                }             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPayPoints_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("При оплате бонусами, ваши бонусы будут списаны и оплата за билет составит 1 рубль. Вы хотите продолжить?", "Уведомление!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var user = CinemaEntities.GetContext().Users.Find(UserID);

                if (user.Points < Price)
                {
                    MessageBox.Show("Недостаточно баллов для списания!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MainWindow mainWindow = new MainWindow();

                    _currentTickets.Date = SessionDate;
                    _currentTickets.Time = DateTime.Now.TimeOfDay;
                    _currentTickets.HallID = _currentSeat.HallID;
                    _currentTickets.Row = _currentSeat.Row;
                    _currentTickets.Seat = _currentSeat.Seat;
                    _currentTickets.Price = 1;
                    _currentTickets.MovieID = MovieID;
                    _currentTickets.MovieTime = SessionTime;

                    _currentSeat.Status = false;

                    if (RoleID == 3)
                    {
                        if (user != null)
                        {
                            user.Points = (int?)(user.Points - Price);
                            CinemaEntities.GetContext().Users.AddOrUpdate(user);
                            CinemaEntities.GetContext().SaveChanges();
                        }
                    }

                    if (_currentTickets.ID == 0)
                        CinemaEntities.GetContext().Tickets.Add(_currentTickets);

                    try
                    {
                        CinemaEntities.GetContext().SaveChanges();
                        MessageBox.Show("Оплата прошла успешно!");
                        mainWindow.Show();
                        Window.GetWindow(this).Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
