using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class DriverAccess
    {
        public static bool GetDriverByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * from Drivers where DriverID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", DriverID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];


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
        public static bool GetDriverPersonIDByID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * from Drivers where PersonID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", PersonID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];


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

        public static int AddDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {


            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID , CreatedDate )
                             VALUES (@PersonID,@CreatedByUserID,@CreatedDate);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {
                connection.Open();
                object obj = cmd.ExecuteScalar();
                if (obj != null && (int.TryParse(obj.ToString(), out int insertedID)))
                {
                    return insertedID;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close(); // مهم تسكري الكونيكشن دايما
            }
            return -1;


        }



        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            int RowEffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"Update  Drivers  
                            set PersonID = @PersonID, 
                                CreatedByUserID = @CreatedByUserID,
                                CreatedDate = @CreatedDate                    
                                where DriverID = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            cmd.Parameters.AddWithValue("@ID", DriverID);



            try
            {
                connection.Open();
                RowEffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (RowEffected > 0);

        }

        public static DataTable GetDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"SELECT * from Drivers";

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

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"  
                            SELECT Top 1 InternationalLicenseID
                            FROM InternationalLicenses 
                            where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                            order by ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return InternationalLicenseID;
        }

        public static DataTable GetMyDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"select * from Drivers_View";

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



        public static bool IsDriverExist(int ID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from Drivers
                                 Where DriverID = @ID ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            bool isFound = false;

            try
            {
                connection.Open();
                Object obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    isFound = true;

                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }




    }
}
