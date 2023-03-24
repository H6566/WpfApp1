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
    /// Логика взаимодействия для PageAddDoctor.xaml
    /// </summary>
    public partial class PageAddDoctor : Page
    {
        private Doctor _currentDoctor = new Doctor();
        public PageAddDoctor(Doctor selecteddoc)
        {
            InitializeComponent();
            if (selecteddoc != null)
            {
                _currentDoctor = selecteddoc;
                Titletxt.Text = "Изменение доктора";
                btnAddpubly.Content = "Изменить";
            }
            // Создаём контекст
            DataContext = _currentDoctor;
            CMBspec.ItemsSource = PolyclinicEntities.GetContext().Specialization.ToList();
            CMBspec.SelectedValuePath = "IDSpecialization";
            CMBspec.DisplayMemberPath = "NameSpecialization";
        }

        private void btnAddpubly_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentDoctor.FIO)) error.AppendLine("Укажите ФИО");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentDoctor.WorkExperience))) error.AppendLine("Укажите место проживания");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentDoctor.IDDoctor == 0)
            {
                PolyclinicEntities.GetContext().Doctor.Add(_currentDoctor);
                try
                {
                    PolyclinicEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageDoctor());
                    MessageBox.Show("Новый доктор успешно добавлен!");
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
                    Classes.ClassFrame.frmObj.Navigate(new PageDoctor());
                    MessageBox.Show("доктор успешно изменен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageDoctor());
        }
    }
}
