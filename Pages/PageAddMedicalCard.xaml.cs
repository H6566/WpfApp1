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
    /// Логика взаимодействия для PageAddMedicalCard.xaml
    /// </summary>
    public partial class PageAddMedicalCard : Page
    {
        private MedicalCard _currentmed = new MedicalCard();
        public PageAddMedicalCard(MedicalCard selectedmedcar)
        {
            InitializeComponent();
            if (selectedmedcar != null)
            {
                _currentmed = selectedmedcar;
                Titletxt.Text = "Изменение мед. карты";
                btnAddpubly.Content = "Изменить";
            }
            // Создаём контекст
            DataContext = _currentmed;
            CMBdiag.ItemsSource = PolyclinicEntities.GetContext().Diagnosis.ToList();
            CMBdiag.SelectedValuePath = "IDDiagnosis";
            CMBdiag.DisplayMemberPath = "DiseaseName";
            CMBnamedoc.ItemsSource = PolyclinicEntities.GetContext().Doctor.ToList();
            CMBnamedoc.SelectedValuePath = "IDDoctor";
            CMBnamedoc.DisplayMemberPath = "FIO";
            CMBnamepat.ItemsSource = PolyclinicEntities.GetContext().Patient.ToList();
            CMBnamepat.SelectedValuePath = "IDPatient";
            CMBnamepat.DisplayMemberPath = "FIO";
        }

        private void btnAddpubly_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentmed.IDMedicalBook == 0)
            {
                PolyclinicEntities.GetContext().MedicalCard.Add(_currentmed);
                try
                {
                    PolyclinicEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageMedicalCard());
                    MessageBox.Show("Новая карта успешно добавлена!");
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
                    Classes.ClassFrame.frmObj.Navigate(new PageMedicalCard());
                    MessageBox.Show("Карта успешно изменена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageMedicalCard());
        }
    }
}
