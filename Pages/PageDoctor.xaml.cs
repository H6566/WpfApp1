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
using WpfApp1.Classes;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageDoctor.xaml
    /// </summary>
    public partial class PageDoctor : Page
    {
        public PageDoctor()
        {
            InitializeComponent();
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Doctor.ToList();
        }

        private void Medical_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageMedicalCard());
        }

        private void Patient_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PagePatient());
        }

        private void Glavmenu_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageGlavMenu());
        }

        private void BTNedit_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddDoctor((Doctor)DtgSQL.SelectedItem));
        }

        private void Btnadd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddDoctor(null));
        }

        private void Delette_Click(object sender, RoutedEventArgs e)
        {
            var AftoForRemoving = DtgSQL.SelectedItems.Cast<Doctor>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {AftoForRemoving.Count()} записи?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PolyclinicEntities.GetContext().Doctor.RemoveRange(AftoForRemoving);
                    PolyclinicEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Doctor.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Doctor.OrderBy(x => x.FIO).ToList();
        }

        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Doctor.OrderByDescending(x => x.FIO).ToList();
        }

        private void RbUp1_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Doctor.Where(x => x.WorkExperience >= 0 && x.WorkExperience <= 20).ToList();
        }

        private void RbDown1_Checked(object sender, RoutedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Doctor.Where(x => x.WorkExperience >= 21 && x.WorkExperience <= 50).ToList();
        }

        private void Findfam_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtgSQL.ItemsSource = PolyclinicEntities.GetContext().Doctor.Where(x => x.FIO.ToLower().Contains(Findfam.Text.ToLower())).ToList();
        }
        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook wb = excelApp.Workbooks.Open($"{Directory.GetCurrentDirectory()}\\Shablon.xlsx");
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
            ws.Cells[1, 4] = "Поликлиника";
            ws.Cells[2, 4] = "Ведомость сотрудников";
            ws.Cells[7, 1] = "Дата";
            DateTime thisDay = DateTime.Today;
            ws.Cells[7, 2] = thisDay.ToShortDateString();
            int indexRows = 9;
            //ячейка
            ws.Cells[1][indexRows] = "ФИО";
            ws.Cells[2][indexRows] = "Стаж";
            ws.Cells[3][indexRows] = "Специализация";

            //список пользователей из таблицы после фильтрации и поиска
            var printItems = DtgSQL.Items;
            //цикл по данным из списка для печати
            foreach (Doctor item in printItems)
            {
                ws.Cells[1][indexRows + 1] = item.FIO;
                ws.Cells[2][indexRows + 1].Value = item.WorkExperience.ToString();
                ws.Cells[3][indexRows + 1] = item.Specialization.NameSpecialization;
                indexRows++;
            }
            ws.Cells[indexRows + 3, 1] = "Подпись:";
            ws.Cells[indexRows + 3, 2] = "Серебрянников Н.Д.";
            excelApp.Visible = true;
        }
    }
}
