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
using WpfApp1.Classes;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddPatient.xaml
    /// </summary>
    public partial class PageAddPatient : Page
    {
        private Patient _currentPatient = new Patient();
        public PageAddPatient(Patient selectedpubly)
        {
            InitializeComponent();
            if (selectedpubly != null)
            {
                _currentPatient = selectedpubly;
                Titletxt.Text = "Изменение пациента";
                btnAddpubly.Content = "Изменить";
            }
            // Создаём контекст
            DataContext = _currentPatient;
        }

        private void btnAddpubly_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPatient.FIO)) error.AppendLine("Укажите ФИО");
            if (string.IsNullOrWhiteSpace(_currentPatient.PlaceOfResidence)) error.AppendLine("Укажите место проживания");
            if (string.IsNullOrWhiteSpace(_currentPatient.PlaceOfWork)) error.AppendLine("Укажите место работы");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentPatient.YearOfBirth))) error.AppendLine("Укажите год рождения");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentPatient.DateOfTheApplication))) error.AppendLine("Укажите дату посещения");
            if (string.IsNullOrWhiteSpace(_currentPatient.Snils)) error.AppendLine("Укажите СНИЛС");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentPatient.IDPatient == 0)
            {
                PolyclinicEntities.GetContext().Patient.Add(_currentPatient);
                try
                {
                    PolyclinicEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PagePatient());
                    MessageBox.Show("Новый пациент успешно добавлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    PolyclinicEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PagePatient());
                    MessageBox.Show("пациент успешно изменен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PagePatient());
        }
    }
}
