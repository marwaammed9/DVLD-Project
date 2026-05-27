using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class AppointmentTestAccess
    {
        public static bool GetAppointmentByID(int ID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref decimal PaidFees, ref DateTime AppointmentDate,
  ref int CreatedByUserID, ref int ReTakeTestApplicationID, ref bool isLocked)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "select * from TestAppointments where TestAppointmentID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;



                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    PaidFees = (decimal)reader["PaidFees"];
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    isLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                    {
                        ReTakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    }
                    else
                    {
                        ReTakeTestApplicationID = -1;
                    }


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

        public static DataTable GetAllAppointmentByLocalIDAndTestID(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;"; SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
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
                Connection.Close();
            }


            return dt;

        }


        public static int AddAppointmentTest(int TestTypeID, int LocalDrivingLicenseApplicationID, decimal PaidFees, DateTime AppointmentDate,
   int CreatedByUserID, int? RetakeTestApplicationID, bool IsLocked)
        {


            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO TestAppointments (CreatedByUserID, TestTypeID, LocalDrivingLicenseApplicationID ,PaidFees,AppointmentDate,RetakeTestApplicationID,IsLocked)
                             VALUES (@CreatedByUserID,@TestTypeID,@LocalDrivingLicenseApplicationID, @PaidFees,@AppointmentDate,@RetakeTestApplicationID, @IsLocked);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@IsLocked", IsLocked);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            if (RetakeTestApplicationID == -1)
            {
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value); // ✅
            }
            else
            {
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            }




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



        public static bool UpdateAppointment(int ID, int TestTypeID, int LocalDrivingLicenseApplicationID, decimal PaidFees, DateTime AppointmentDate,
   int CreatedByUserID, int? RetakeTestApplicationID, bool IsLocked)
        {

            int RowEffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);


            string query = @"Update  TestAppointments  
                            set CreatedByUserID = @CreatedByUserID, 
                                TestTypeID = @TestTypeID, 
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, 
                                PaidFees = @PaidFees, 
                                AppointmentDate = @AppointmentDate, 
                                IsLocked = @IsLocked,
                               RetakeTestApplicationID = @RetakeTestApplicationID
                               
                                where TestAppointmentID = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@IsLocked", IsLocked);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);

            if (RetakeTestApplicationID == -1)
            {
                cmd.Parameters.AddWithValue("@ReTakeTestApplicationID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ReTakeTestApplicationID", RetakeTestApplicationID);
            }

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

        public static DataTable GetAppointments()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = "select * from TestAppointments_View";
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

        public static bool DeleteAppointment(int AppointemntID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = @"Delete TestAppointments where TestAppointmentID = @ID";

            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@ID", AppointemntID);
            int RowEffected = 0;

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

        public static bool IsExsit(int ID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from TestAppointments
                                 Where TestAppointmentID = @ID ";

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

        public static bool IsExsitByLocalID(int LocalID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from TestAppointments
                                 Where LocalDrivingLicenseApplicationID = @ID ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ID", LocalID);
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

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
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


            return TestID;

        }

    }
}
