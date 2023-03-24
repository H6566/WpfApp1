using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;
using WpfApp1.Classes;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMedicalCard.xaml
    /// </summary>
    public partial class PageMedicalCard : Page
    {
        public PageMedicalCard()
        {
            InitializeComponent();
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().MedicalCard.ToList();
        }

        private void Doctor_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageDiagnosis());
        }

        private void Medical_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageDoctor());
        }

        private void Glavmenu_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageGlavMenu());
        }

        private void Btnadd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddMedicalCard(null));
        }

        private void Delette_Click(object sender, RoutedEventArgs e)
        {
            var AftoForRemoving = DtgSQL.SelectedItems.Cast<MedicalCard>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {AftoForRemoving.Count()} записи?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PolyclinicEntities.GetContext().MedicalCard.RemoveRange(AftoForRemoving);
                    PolyclinicEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DtgSQL.ItemsSource = PolyclinicEntities.GetContext().MedicalCard.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BTNedit_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddMedicalCard((MedicalCard)DtgSQL.SelectedItem));
        }

        private void Finddiag_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().MedicalCard.Where(x => x.Diagnosis.DiseaseName.ToLower().Contains(Finddiag.Text.ToLower())).ToList();
        }

        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().MedicalCard.OrderBy(x => x.IDDiagnosis).ToList();
        }

        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().MedicalCard.OrderByDescending(x => x.IDDiagnosis).ToList();
        }

      
    }
}
