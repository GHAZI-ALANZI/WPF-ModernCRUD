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
using Wpf_ModernCRUD.EntityModel;
using Wpf_ModernCRUD.Pages;

namespace Wpf_ModernCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModerbCRUDEntities _db = new ModerbCRUDEntities();
        public static DataGrid datagrid;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            DGridCustomer.ItemsSource = _db.CustomerDBs.ToList();
            datagrid = DGridCustomer;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int ID = (DGridCustomer.SelectedItem as CustomerDB).ID;
            EditDataWin editDataWin = new EditDataWin(ID);
            editDataWin.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int ID = (DGridCustomer.SelectedItem as CustomerDB).ID;
            var deleteCustomer = _db.CustomerDBs.Where(m => m.ID == ID).Single();
            _db.CustomerDBs.Remove(deleteCustomer);
            _db.SaveChanges();
            DGridCustomer.ItemsSource = _db.CustomerDBs.ToList();
            MessageBox.Show("Delete Data Success!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddDataWin addDataWin = new AddDataWin();
            addDataWin.ShowDialog();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = _db.CustomerDBs.Where(x => x.Name.Contains(TBoxSearch.Text) || x.Address.Contains(TBoxSearch.Text) || x.Phone.Contains(TBoxSearch.Text)).ToList();
            DGridCustomer.ItemsSource = result;
        }
    }
}

