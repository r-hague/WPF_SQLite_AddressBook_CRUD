using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBassignment10
{
    public class ViewModel:INotifyPropertyChanged
    {
      Database db = Database.Getdata();

        #region properties

        //the list to show on the main page
        BindingList<Emp> employees;
        public BindingList<Emp> Employees
        {
            get { return employees; }
            set {
                employees = value;
                NotifyPropertyChanged();
            }
        }


        //the selected item
        Emp selectedEmployee;
        public Emp SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value; NotifyPropertyChanged(); }
        }

        //a copy of the selected item or a new item to be edited
        Emp employeeToEdit;
        public Emp EmployeeToEdit
        {
            get { return employeeToEdit; }
            set { employeeToEdit = value; NotifyPropertyChanged(); }
        }
        
        #endregion

        public ViewModel()
        {
            //get a list of persons from the database
            employees = new BindingList<Emp>(db.Get());

        }

        public void Save()
        {
            //if we were editing an existing person
            if (employeeToEdit.EmployeeID != 0)
            {
                int index = Employees.IndexOf(selectedEmployee);
                //remove selectedPerson (the one we're editing) from the list
                Employees.Remove(selectedEmployee);
                //add the new version to the list into the same spot
                Employees.Insert(index, employeeToEdit);
            }
            //we're creating a new one, so add it to the end
            else
                Employees.Add(employeeToEdit);

            //have the database figure out what needs to be updated in the database
            db.Save(Employees.ToList());
        }

        public void Delete()
        {
            db.Delete(selectedEmployee);
            Employees.Remove(selectedEmployee);
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }
}
