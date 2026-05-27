using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class LicenseClassesAccess
    {
        public static bool GetClassByID(int ID, ref string ClassName, ref string ClassDescription, ref byte MinAllowedAge, ref byte DefaultValidityLength
            , ref decimal ClassFees)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * from LicenseClasses where LicenseClassID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    ClassName = (string)reader["ClassName"];
                    ClassFees = (decimal)reader["ClassFees"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];



                    reader.Close();
                }
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

        public static bool GetClassByName(string ClassName, ref int ID, ref string ClassDescription, ref byte MinAllowedAge, ref byte DefaultValidityLength
          , ref decimal ClassFees)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * FROM LicenseClasses where ClassName = @Name";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Name", ClassName);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    ID = (int)reader["LicenseClassID"];
                    ClassFees = (decimal)reader["ClassFees"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];



                    reader.Close();
                }

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

        public static DataTable GetClassLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = "select * from LicenseClasses";
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
    }
}
