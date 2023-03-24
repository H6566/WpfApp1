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
    /// Логика взаимодействия для PageGlavMenu.xaml
    /// </summary>
    public partial class PageGlavMenu : Page
    {
        public PageGlavMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PagePatient());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageDoctor());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageMedicalCard());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageDiagnosis());
        }
    }
}
