using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalLib
{
    public class ActualDal : IDal
    {
        private readonly string cnnstr;
        SqlConnection cnn;
        SqlCommand cmd;
        public ActualDal(string cnnStr)
        {
            this.cnnstr = cnnStr;
            this.cnn = new SqlConnection(cnnStr);
            this.cmd = new SqlCommand();
            cmd.Connection = this.cnn;
            
        }
        
        public bool AddEmployee(Employee emp)
        {
            cmd.CommandText=$"insert into Emp values({emp.Id},'{emp.Name}',{emp.Dept})";
            cnn.Open();
            int rowsAffected =cmd.ExecuteNonQuery();
            cnn.Close();
            return rowsAffected > 0;
        }

        public List<Employee> GetAllEmployee()
        {
          List<Employee> list = new List<Employee>();
            cmd.CommandText = "select * from Emp";
            cnn.Open();
            SqlDataReader reader = this.cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Employee { Id=(int)reader[0],Name=(string)reader[1], Dept=(int)reader[2] });
            }
            cnn.Close();
            return list;
            
                
         
        }

        public Employee GetEmployeeById(int id)
        {
            Employee emp = null;
            cmd.CommandText = $"select * from Emp where EmpId ={id}";
            cnn.Open();
            SqlDataReader reader =this.cmd.ExecuteReader();
            if(reader.Read())
            {
                emp =new Employee { Id = (int)reader[0], Name = (string)reader[1], Dept = (int)reader[2] };
            }
            cnn.Close();
            return emp;
        }

        public bool ModifyEmployee(Employee emp)
        {
            cmd.CommandText = $"Update Emp set EmpName='{emp.Name}',Dept= {emp.Dept} where EmpId ={emp.Id}";
            cnn.Open();
            int rowsAffected=cmd.ExecuteNonQuery();
            cnn.Close();
            return rowsAffected > 0;    
        }

        public bool RemoveEmployee(int id)
        {
            cmd.CommandText = $"delete Emp where EmpId ={id}";
            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();
            return rowsAffected > 0;

        }
    }
}
