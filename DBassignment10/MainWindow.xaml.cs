using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SQLite;

namespace DBassignment10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }
       
        //add
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //create new person to edit
            vm.EmployeeToEdit = new Emp();
            //create the edit window
            Employeedata pw = new Employeedata(vm)
            {
                //make us the owner of this dialog
                Owner = this,
                //so that we can put the dialog centered on us on screen
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            pw.ShowDialog();
        }
        //edit
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedEmployee == null)
            {
                MessageBox.Show("Select a record to update");

            }

            else
            {
                //make a copy and allow the editing window access to the copy
                vm.EmployeeToEdit = vm.SelectedEmployee.Surrogate();
                Employeedata pw = new Employeedata(vm);
                pw.ShowDialog();

            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
                if (vm.SelectedEmployee == null)
                {
                    MessageBox.Show("Select a record to delete");

                }

                else
                {
                    vm.Delete();
            }
        }
        //close
    }
}
