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
    /// Логика взаимодействия для PageAddDiagnos.xaml
    /// </summary>
    public partial class PageAddDiagnos : Page
    {
        private Diagnosis _currentDiagnosis = new Diagnosis();
        public PageAddDiagnos(Diagnosis selecteddiag)
        {
            InitializeComponent();
            CMBlen.ItemsSource = PolyclinicEntities.GetContext().Treatment.ToList();
            CMBlen.SelectedValuePath = "IDTreatment";
            CMBlen.DisplayMemberPath = "TreatmentСharacteristic";
            if (selecteddiag != null)
            {
                _currentDiagnosis = selecteddiag;
                Titletxt.Text = "Изменение диагноз";
                btnAddpubly.Content = "Изменить";
            }
            // Создаём контекст
            DataContext = _currentDiagnosis;
        }

        private void btnAddpubly_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentDiagnosis.DiseaseName)) error.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(_currentDiagnosis.Severity)) error.AppendLine("Укажите степень тяжести");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentDiagnosis.IDDiagnosis == 0)
            {
                PolyclinicEntities.GetContext().Diagnosis.Add(_currentDiagnosis);
                try
                {
                    PolyclinicEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageDiagnosis());
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
                    Classes.ClassFrame.frmObj.Navigate(new PageDiagnosis());
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
            Classes.ClassFrame.frmObj.Navigate(new PageDiagnosis());
        }
    }
}
