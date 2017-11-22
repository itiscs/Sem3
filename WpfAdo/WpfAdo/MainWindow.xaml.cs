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

namespace WpfAdo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            WpfAdo.DataSet1 dataSet1 = ((WpfAdo.DataSet1)(this.FindResource("dataSet1")));
            // Загрузить данные в таблицу Customers. Можно изменить этот код как требуется.
            WpfAdo.DataSet1TableAdapters.CustomersTableAdapter dataSet1CustomersTableAdapter = new WpfAdo.DataSet1TableAdapters.CustomersTableAdapter();
            dataSet1CustomersTableAdapter.Fill(dataSet1.Customers);
            System.Windows.Data.CollectionViewSource customersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customersViewSource")));
            customersViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Zadacha1. Можно изменить этот код как требуется.
            WpfAdo.DataSet1TableAdapters.Zadacha1TableAdapter dataSet1Zadacha1TableAdapter = new WpfAdo.DataSet1TableAdapters.Zadacha1TableAdapter();
            dataSet1Zadacha1TableAdapter.Fill(dataSet1.Zadacha1,100m);
            System.Windows.Data.CollectionViewSource zadacha1ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("zadacha1ViewSource")));
            zadacha1ViewSource.View.MoveCurrentToFirst();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            WpfAdo.DataSet1 dataSet1 = ((WpfAdo.DataSet1)(this.FindResource("dataSet1")));
            // Загрузить данные в таблицу Customers. Можно изменить этот код как требуется.
            WpfAdo.DataSet1TableAdapters.CustomersTableAdapter dataSet1CustomersTableAdapter = new WpfAdo.DataSet1TableAdapters.CustomersTableAdapter();
            dataSet1CustomersTableAdapter.Update(dataSet1.Customers);
            MessageBox.Show("OK!");

        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            WpfAdo.DataSet1 dataSet1 = ((WpfAdo.DataSet1)(this.FindResource("dataSet1")));
            // Загрузить данные в таблицу Zadacha1. Можно изменить этот код как требуется.
            WpfAdo.DataSet1TableAdapters.Zadacha1TableAdapter dataSet1Zadacha1TableAdapter = new WpfAdo.DataSet1TableAdapters.Zadacha1TableAdapter();
            dataSet1Zadacha1TableAdapter.Fill(dataSet1.Zadacha1, Convert.ToDecimal(txtSum.Text));
            System.Windows.Data.CollectionViewSource zadacha1ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("zadacha1ViewSource")));
            zadacha1ViewSource.View.MoveCurrentToFirst();


        }
    }
}
