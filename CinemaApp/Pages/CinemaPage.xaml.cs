using CinemaApp.Classes;
using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CinemaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для CinemaPage.xaml
    /// </summary>
    public partial class CinemaPage : Page
    {
        private DispatcherTimer timer;
        int UserID;
        int RoleID;
        public CinemaPage(int userID, int roleID)
        {
            InitializeComponent();
            InitializeClock();
           
            UserID = userID;
            RoleID = roleID;

            CinemaUpdate();
        }

        public void CinemaUpdate()
        {
            var moviesList = CinemaEntities.GetContext().Movies.ToList();

            lvMovies.ItemsSource = moviesList;
        }

        private void InitializeClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tbClock.Text = DateTime.Now.ToString("HH:mm");
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {          
            Manager.MainFrame.Navigate(new SessionsPage((sender as Grid).DataContext as Movies, UserID, RoleID));
        }
    }
}
