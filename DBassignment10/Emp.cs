using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBassignment10
{
    
    public class EmployeeSortByName : IComparer<Emp>
    {
        public int Compare(Emp x, Emp y)
        {
            //used for sorting the data
            //you can pick any field you want to use as sort order
            var sort = x.Name.CompareTo(y.Name);
            if (sort == 0)
                sort = x.Name.CompareTo(y.Name);

            return sort;
        }
    }

    public class Emp
    {
        //defining the properties for the database fields
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public decimal Hourly_Payrate { get; set; }

        //making a clone
        public Emp Surrogate()
        {
            var u = new Emp
            {
                EmployeeID = EmployeeID,
                Name = Name,
                Position = Position,
                Department = Department,
                Hourly_Payrate = Hourly_Payrate
               
            };
            return u;
        }
 
    }

}
