using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DBassignment10
{
    public class Database
    {
        const string CONNECT_STRING = "DataSource=Personnel.db";
        SQLiteConnection conn;
        static Database _db;

        private Database()
        {
            conn = new SQLiteConnection(CONNECT_STRING);
            conn.Open();
        }

        public static Database Getdata()
        {
            if (_db == null)
                _db = new Database();
            return _db;
        }

        public int GetID()
        {
            var id = 0;
            string Query = "SELECT MAX(EmployeeID) FROM Employee"; //query correction to select max employeeid and add another unique id to it
            var CreateCommand = new SQLiteCommand(Query, conn);
            var o = CreateCommand.ExecuteScalar();
            id = int.Parse(o.ToString()); //fixed the casting


            return id + 1 ;
        }

        public List<Emp> Get()
        {
            var ps = new List<Emp>();
            var cmdString = "SELECT EmployeeID,Name,Position, Department, Hourly_Payrate" +
                               " FROM Employee";
            var cmd = new SQLiteCommand(cmdString, conn);
            SQLiteDataReader rd = cmd.ExecuteReader();

            //getordinal function is used to sort and perform action on a column by using case sensitive search
            //useless in loop because of searching same thing again and again over,so instead of using it in a loop assign it to an int
            //and call it throught that variable in a loop
            int employeeid = rd.GetOrdinal("EmployeeID");
            int hourlyrate = rd.GetOrdinal("Hourly_Payrate");
            int empname = rd.GetOrdinal("Name");
            int empposition = rd.GetOrdinal("Position");
            int empdept = rd.GetOrdinal("Department");
            while (rd.Read())
                ps.Add(new Emp()
                {
                    EmployeeID = rd.GetInt32(employeeid),
                    Name = rd.GetString(empname),
                    Position = rd.GetString(empposition),
                    Department = rd.GetString(empdept),
                    Hourly_Payrate = rd.GetDecimal(hourlyrate)
                });
            rd.Close();

           // ps.Sort(new EmployeeSortByName()); Not sorting through name since we need to display it in primary key order

            return ps;
        }

        public void Save(List<Emp> ps)
        {
            var employeesToDelete = new List<Emp>();

            ViewModel vm = new ViewModel();
            foreach (Emp p in ps)
            {
                if (p.EmployeeID == 0)
                {
                    Add(p);
                }
                else
                {
                    Update(p);
                }
            }
        }

        public void Add(Emp p)
        {
            if (p.EmployeeID == 0)
                p.EmployeeID = GetID();

            var cmdString = "INSERT INTO Employee " +
                                    "(EmployeeID, Name, Position, Department, Hourly_Payrate)" +
                                    "VALUES" +
                                    "(@EMPLOYEEID, @NAME, @POSITION, @DEPARTMENT, @HOURLY_PAYRATE)";

            var cmd = new SQLiteCommand(cmdString, conn);
            cmd.Parameters.AddWithValue("@EMPLOYEEID", p.EmployeeID);
            cmd.Parameters.AddWithValue("@NAME", p.Name);
            cmd.Parameters.AddWithValue("@POSITION", p.Position);
            cmd.Parameters.AddWithValue("@DEPARTMENT", p.Department);
            cmd.Parameters.AddWithValue("@HOURLY_PAYRATE", p.Hourly_Payrate);

            //to not letting the program break by empty fields or 0 payrate since it will give error on cast implementation in getfunction next time you run the query,
            //thus having to reload the db backup.
            if (p.Hourly_Payrate == 0 ||p.Name==null||p.Position==null)
            {
                cmd.Cancel();
                MessageBox.Show("Invalid inputs, record not updated");
            }
            else
                cmd.ExecuteNonQuery();
        }

        public void Delete(Emp p)
        {
            var cmdString = "DELETE from Employee WHERE EmployeeID = @EMPLOYEEID";

            var cmd = new SQLiteCommand(cmdString, conn);
            cmd.Parameters.AddWithValue("@EMPLOYEEID", p.EmployeeID);

            cmd.ExecuteNonQuery();
        }

        public void Update(Emp p)
        {
                var cmdString = "UPDATE Employee SET Name = @NAME, " +
                                                     "Position = @POSITION, " +
                                                     "Department = @DEPARTMENT, " +
                                                     "Hourly_Payrate = @HOURLY_PAYRATE " +
                                   "WHERE EmployeeID = @EMPLOYEEID";

                var cmd = new SQLiteCommand(cmdString, conn);
                cmd.Parameters.AddWithValue("@EMPLOYEEID", p.EmployeeID);
                cmd.Parameters.AddWithValue("@NAME", p.Name);
                cmd.Parameters.AddWithValue("@POSITION", p.Position);
                cmd.Parameters.AddWithValue("@DEPARTMENT", p.Department);
                cmd.Parameters.AddWithValue("@HOURLY_PAYRATE", p.Hourly_Payrate);

            if (p.Hourly_Payrate == 0 || p.Name == null || p.Position == null)
            {
                cmd.Cancel();
                MessageBox.Show("Invalid inputs, record not updated");
            }

            else
                cmd.ExecuteNonQuery();
        }
        }

    }

