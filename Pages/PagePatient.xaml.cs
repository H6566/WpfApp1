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
    /// Логика взаимодействия для PagePatient.xaml
    /// </summary>
    public partial class PagePatient : Page
    {
        public PagePatient()
        {
            InitializeComponent();
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.ToList();
            var listlive = PolyclinicEntities.GetContext().Patient.Select(x => x.PlaceOfResidence).Distinct().ToList();
            var listwork = PolyclinicEntities.GetContext().Patient.Select(x => x.PlaceOfWork).Distinct().ToList();
            CMBFilterhouse.Items.Add("Все места проживания");
            foreach (string life in listlive)
            {
                CMBFilterhouse.Items.Add(life);
            }
            CMBFilterjobs.Items.Add("Все места работ");
            foreach (string job in listwork)
            {
                CMBFilterjobs.Items.Add(job);
            }
        }

        private void Glavmenu_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageGlavMenu());
        }

        private void Doctor_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageDoctor());
        }

        private void BTNedit_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddPatient((Patient)DtgSQL.SelectedItem));
        }

        private void Btnadd_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddPatient(null));
        }

        private void Delette_Click(object sender, RoutedEventArgs e)
        {
            var AftoForRemoving = DtgSQL.SelectedItems.Cast<Patient>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {AftoForRemoving.Count()} записи?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PolyclinicEntities.GetContext().Patient.RemoveRange(AftoForRemoving);
                    PolyclinicEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void RbDown1_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.Where(x => x.YearOfBirth >= 2001 && x.YearOfBirth <= 2023).ToList();
        }

        private void RbUp1_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.Where(x => x.YearOfBirth >= 1950 && x.YearOfBirth <= 2000).ToList();
        }

        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.OrderBy(x => x.FIO).ToList();
        }

        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.OrderByDescending(x => x.FIO).ToList();
        }

        private void Findfam_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.Where(x => x.Snils.ToLower().Contains(Findfam.Text.ToLower())).ToList();
        }

        private void CMBFilterhouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string life = CMBFilterhouse.SelectedValue.ToString();
            if (life != "Все места проживания")
                DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.Where(x => x.PlaceOfResidence == life).ToList();
            else
                DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.ToList();
        }

        private void CMBFilterjobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string job = CMBFilterjobs.SelectedValue.ToString();
            if (job != "Все места работ")
                DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.Where(x => x.PlaceOfWork == job).ToList();
            else
                DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Patient.ToList();
        }
    }
}
