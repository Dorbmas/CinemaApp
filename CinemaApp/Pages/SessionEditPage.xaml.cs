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
    /// Логика взаимодействия для SessionEditPage.xaml
    /// </summary>
    public partial class SessionEditPage : Page
    {
        Sessions _currentSession = new Sessions();
        public SessionEditPage(Sessions selectedSession)
        {
            InitializeComponent();

            if (selectedSession != null)
                _currentSession = selectedSession;

            DataContext = _currentSession;

            cbMovie.ItemsSource = CinemaEntities.GetContext().Movies.ToList();
            cbHall.ItemsSource = CinemaEntities.GetContext().Halls.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentSession.Movies == null)
                errors.AppendLine("Укажите фильм!");
            if (_currentSession.Date == null || _currentSession.Date < DateTime.Today)
                errors.AppendLine("Введите дату сеанса!");
            if (_currentSession.Time == null)
                errors.AppendLine("Введите время сеанса!");
            if (_currentSession.Halls == null)
                errors.AppendLine("Укажите зал!");
            if (_currentSession.Price < 1)
                errors.AppendLine("Введите цену сеанса!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentSession.ID == 0)
                CinemaEntities.GetContext().Sessions.Add(_currentSession);

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
