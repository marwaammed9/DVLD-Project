using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class ApplicationAccess
    {
        public static bool GetApplicationByID(int ID, ref int AppPersonID, ref int CreatedByUserID, ref decimal PaidFees, ref DateTime LastStatusUpdate,
       ref byte AppStatus, ref int AppTypeID, ref DateTime AppDate)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "select * FROM Applications where ApplicationID = @ID";
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
                    AppStatus = (byte)reader["ApplicationStatus"];
                    AppTypeID = (int)reader["ApplicationTypeID"];
                    AppPersonID = (int)reader["ApplicantPersonID"];
                    LastStatusUpdate = (DateTime)reader["LastStatusDate"];
                    AppDate = (DateTime)reader["ApplicationDate"];


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

 
        public static int AddApplication( int AppPersonID,  int CreatedByUserID,  decimal PaidFees,  DateTime LastStatusUpdate,
        byte AppStatus,  int AppTypeID,  DateTime AppDate)
        {


            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO Applications (CreatedByUserID, ApplicationDate, LastStatusDate ,ApplicantPersonID,ApplicationTypeID,ApplicationStatus ,PaidFees)
                             VALUES (@CreatedByUserID,@ApplicatonDate,@LastStatusDate, @ApplicantPersonID,@ApplicationTypeID, @ApplicationStatus ,@PaidFees);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@ApplicatonDate", AppDate);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusUpdate);
            cmd.Parameters.AddWithValue("@ApplicantPersonID", AppPersonID);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", AppTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", AppStatus);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
    

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


        public static bool UpdateApplication(int ID, int AppPersonID, int CreatedByUserID, decimal PaidFees, DateTime LastStatusUpdate,
        byte AppStatus, int AppTypeID, DateTime AppDate)
        {

            int RowEffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

        
            string query = @"Update  Applications  
                            set CreatedByUserID = @CreatedByUserID, 
                                ApplicationDate = @ApplicationDate, 
                                LastStatusDate = @LastStatusDate, 
                                ApplicantPersonID = @ApplicantPersonID, 
                                ApplicationTypeID = @ApplicationTypeID, 
                                ApplicationStatus = @ApplicationStatus, 
                                PaidFees = @PaidFees
                                where ApplicationID = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@ApplicationDate", AppDate);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusUpdate);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", AppTypeID);
            cmd.Parameters.AddWithValue("@ApplicantPersonID", AppPersonID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", AppStatus);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
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

        public static DataTable GetApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = "select * from Applications";
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

        public static bool DeleteApplication(int ID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = @"Delete Applications where ApplicationID = @ID";

            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@ID", ID);
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
            string query = @"Select Found = 1 from Applications
                                 Where ApplicationID = @ID ";

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


        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {

            //incase the ActiveApplication ID !=-1 return true.
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT ActiveApplicationID=ApplicationID FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"Update  Applications  
                            set 
                                ApplicationStatus = @NewStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID=@ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@NewStatus", NewStatus);
            command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

    }


}

