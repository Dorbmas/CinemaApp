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
    /// Логика взаимодействия для HallEditPage.xaml
    /// </summary>
    public partial class HallEditPage : Page
    {
        Halls _currentHalls = new Halls();
        public HallEditPage(Halls selectedHalls)
        {
            InitializeComponent();

            if (selectedHalls != null)
                _currentHalls = selectedHalls;

            DataContext = _currentHalls;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentHalls.Title))
                errors.AppendLine("Введите зал!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentHalls.ID == 0)
                CinemaEntities.GetContext().Halls.Add(_currentHalls);

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
