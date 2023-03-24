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
    /// Логика взаимодействия для PageDiagnosis.xaml
    /// </summary>
    public partial class PageDiagnosis : Page
    {
        public PageDiagnosis()
        {
            InitializeComponent();
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Diagnosis.ToList();
            var listform = PolyclinicEntities.GetContext().Diagnosis.Select(x => x.Severity).Distinct().ToList();
            CMBFilterdiagnos.Items.Add("Все форматы");
            foreach (string formm in listform)
            {
                CMBFilterdiagnos.Items.Add(formm);
            }
        }
        private void CMBFilterdiagnos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string form = CMBFilterdiagnos.SelectedValue.ToString();
            if (form != "Все форматы")
            {
                DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Diagnosis.Where(x => x.Severity == form).ToList();
            }
            else
                DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Diagnosis.ToList();
        }

        private void Finddiagnos_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Diagnosis.Where(x => x.DiseaseName.ToLower().Contains(Finddiagnos.Text.ToLower())).ToList();
        }

        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Diagnosis.OrderBy(x => x.DiseaseName).ToList();
        }

        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Diagnosis.OrderByDescending(x => x.DiseaseName).ToList();
        }

        private void Delette_Click(object sender, RoutedEventArgs e)
        {
            var AftoForRemoving = DtgSQL.SelectedItems.Cast<Diagnosis>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {AftoForRemoving.Count()} записи?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PolyclinicEntities.GetContext().Diagnosis.RemoveRange((IEnumerable<Classes.Diagnosis>)AftoForRemoving);
                    PolyclinicEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Diagnosis.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Btnadd_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddDiagnos(null));
        }

        private void BTNedit_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddDiagnos((Diagnosis)DtgSQL.SelectedItem));
        }

        private void Med_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageMedicalCard());
        }

        private void Glavmenu_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageGlavMenu());
        }

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageDoctor());
        }
    }
}
