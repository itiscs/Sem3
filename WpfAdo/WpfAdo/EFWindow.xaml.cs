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
using System.Windows.Shapes;

namespace WpfAdo
{
    /// <summary>
    /// Логика взаимодействия для EFWindow.xaml
    /// </summary>
    public partial class EFWindow : Window
    {
        Model.ModelDB _context = new Model.ModelDB();

        public EFWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource customersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customersViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            customersViewSource.Source = _context.Customers.ToList();
        }
    }
}
