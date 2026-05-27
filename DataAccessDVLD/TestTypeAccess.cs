using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class TestTypeAccess
    {
        public static DataTable GetTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = "select * from TestTypes";
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }


            return dt;
        }


        public static bool GetTestTypeByID(int ID, ref string Name, ref string Description, ref decimal Fees)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * from TestTypes where TestTypeID = @ID ";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;


                    Name = (string)reader["TestTypeTitle"];
                    Description = (string)reader["TestTypeDescription"];
                    Fees = (decimal)reader["TestTypeFees"];


                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                // Console.WriteLine(ex.ToString());
            }
            finally
            {
                Connection.Close();
            }

            return isFound;

        }
        public static bool GetTestTypeByName(string Name, ref int ID, ref string Description, ref decimal Fees)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * from TestTypes where TestTypeTitle = @TestTypeTitle ";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeTitle", Name);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;


                    ID = (int)reader["TestTypeID"];
                    Description = (string)reader["TestTypeDescription"];
                    Fees = (decimal)reader["TestTypeFees"];


                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                // Console.WriteLine(ex.ToString());
            }
            finally
            {
                Connection.Close();
            }

            return isFound;

        }

        public static bool UpdateTestType(int ID, string Name, string Des, decimal Fees)
        {

            int RowEffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"Update  TestTypes  
                            set TestTypeTitle = @TestTypeTitle,
                               TestTypeDescription = @TestTypeDescription, 
                                TestTypeFees = @TestTypeFees
                                where TestTypeID = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@TestTypeDescription", Des);
            cmd.Parameters.AddWithValue("@TestTypeFees", Fees);
            cmd.Parameters.AddWithValue("@TestTypeTitle", Name);



            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                RowEffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (RowEffected > 0);

        }
    }
}
