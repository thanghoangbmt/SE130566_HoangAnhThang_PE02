using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAssemblies
{
    public class StudentData
    {
        string strConnection;

        public StudentData()
        {
            strConnection = getConnectionString();
        }

        public string getConnectionString()
        {
            String strConnection = "server=SE130566\\SQLEXPRESS;database=Test;uid=sa;pwd=gooner";
            return strConnection;
        }

        public bool AddNewProduct(Student s)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "INSERT INTO Students(StudentName, Dob, Email) " +
                "VALUES(@StudentName, @Dob, @Email)";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@StudentName", s.Name);
            cmd.Parameters.AddWithValue("@Dob", s.Dob);
            cmd.Parameters.AddWithValue("@Email", s.Email);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool UpdateProduct(Student s)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "UPDATE Students SET StudentName = @StudentName, Dob = @Dob, " +
                "Email = @Email WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", s.ID);
            cmd.Parameters.AddWithValue("@StudentName", s.Name);
            cmd.Parameters.AddWithValue("@Dob", s.Dob);
            cmd.Parameters.AddWithValue("@Email", s.Email);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool DeleteStudent(Student s)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "DELETE Students WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", s.ID);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public List<Student> FindStudent(string searchValue)
        {
            List<Student> list = null;
            string SQL = "SELECT ID, StudentName, Dob, Email" +
                " FROM Students WHERE StudentName LIKE '%" + searchValue + "%'";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataReader rd = null;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (list == null)
                        {
                            list = new List<Student>();
                        }

                        Student student = new Student()
                        {
                            ID = rd.GetInt32(0),
                            Name = rd.GetString(1),
                            Dob = rd.GetDateTime(2),
                            Email = rd.GetString(3)
                        };
                        list.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                cnn.Close();
            }

            return list;
        }

        public List<Student> GetListStudent()
        {
            List<Student> list = null;
            string SQL = "SELECT ID, StudentName, Dob, Email" +
                " FROM Students";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataReader rd = null;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (list == null)
                            list = new List<Student>();
                        Student student = new Student()
                        {
                            ID = rd.GetInt32(0),
                            Name = rd.GetString(1),
                            Dob = rd.GetDateTime(2),
                            Email = rd.GetString(3)
                        };
                        list.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return list;
        }
    }
}
