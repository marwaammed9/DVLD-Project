using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class LicenseAccess
    {
        public static bool GetLicenseByID(int ID, ref int AppID, ref int DriverID, ref int CreatedByUserID, ref decimal PaidFees, ref DateTime IssuseDate,
        ref int LicenseClassID, ref DateTime ExpDate, ref string Notes, ref bool IsActive, ref byte IssuseReason)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "select * from Licenses where LicenseID = @ID";
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
                    AppID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IsActive = (bool)reader["IsActive"];
                    IssuseDate = (DateTime)reader["IssueDate"];
                    ExpDate = (DateTime)reader["ExpirationDate"];
                    IssuseReason = (byte)reader["IssueReason"];
                    LicenseClassID = (int)reader["LicenseClass"];

                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
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

        public static bool UpdateLicense(int ID, int AppID, int DriverID, int CreatedByUserID, decimal PaidFees, DateTime IssuseDate,
         int LicenseClassID, DateTime ExpDate, string Notes, bool IsActive, byte IssuseReason)
        {

            int RowEffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"Update  Licenses  
                            set ApplicationID = @ApplicationID, 
                                PaidFees = @PaidFees, 
                                CreatedByUserID = @CreatedByUserID, 
                                DriverID = @DriverID, 
                                IssueDate = @IssueDate, 
                                ExpirationDate = @ExpirationDate, 
                                IssueReason = @IssueReason, 
                                LicenseClass = @LicenseClass ,
                                IsActive = @IsActive,
                                Notes = @Notes
                                where LicenseID = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ApplicationID", AppID);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@IssueDate", IssuseDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpDate);
            cmd.Parameters.AddWithValue("@IssueReason", IssuseReason);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);




            if (Notes != "")
            {
                cmd.Parameters.AddWithValue("@Notes", Notes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Notes", System.DBNull.Value);
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
        public static bool GetLicenseByAppID(int AppID, ref int ID, ref int DriverID, ref int CreatedByUserID, ref decimal PaidFees, ref DateTime IssuseDate,
   ref int LicenseClassID, ref DateTime ExpDate, ref string Notes, ref bool IsActive, ref byte IssuseReason)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "select * FROM Licenses where ApplicationID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", AppID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;



                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    PaidFees = (decimal)reader["PaidFees"];
                    ID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    IsActive = (bool)reader["IsActive"];
                    IssuseDate = (DateTime)reader["IssueDate"];
                    ExpDate = (DateTime)reader["ExpirationDate"];
                    IssuseReason = (byte)reader["IssueReason"];
                    LicenseClassID = (int)reader["LicenseClass"];

                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
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


        public static int IsActiveLicenseExistByPersonIDAndClassID(int ClassID, int PersonID)
        {
            int LicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@LicenseClass", ClassID);



            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return LicenseID;


        }



        public static int AddLicnese(int AppID, int DriverID, int CreatedByUserID, decimal PaidFees, DateTime IssuseDate,
         int LicenseClassID, DateTime ExpDate, string Notes, bool IsActive, byte IssuseReason)
        {


            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO Licenses (CreatedByUserID, PaidFees, ApplicationID ,DriverID,IsActive,IssueDate ,ExpirationDate ,IssueReason ,LicenseClass ,Notes)
                             VALUES (@CreatedByUserID,@PaidFees,@ApplicationID, @DriverID,@IsActive, @IssueDate ,@ExpirationDate,@IssueReason,@LicenseClass ,@Notes);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@ApplicationID", AppID);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpDate);
            cmd.Parameters.AddWithValue("@IssueDate", IssuseDate);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            if (Notes == "")
            {
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Notes", Notes);

            }

            cmd.Parameters.AddWithValue("@IssueReason", IssuseReason);





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
        public static DataTable GetLicensesByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = "select * from Licenses where DriverID = @ID";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ID", DriverID);


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



        public static DataTable GetLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = "select * from Licenses";
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

        //public static bool DeletePeople(int ID)
        //{
        //    SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
        //    string Query = @"Delete People where PersonID = @ID";

        //    SqlCommand cmd = new SqlCommand(Query, connection);
        //    cmd.Parameters.AddWithValue("@ID", ID);
        //    int RowEffected = 0;

        //    try
        //    {
        //        connection.Open();
        //        RowEffected = cmd.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }




        //    return (RowEffected > 0);

        //}

        public static bool IsExsit(int ID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from Licenses
                                 Where LicenseID = @ID ";

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
        public static bool IsDetain(int ID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"select Found =1 from DetainedLicenses inner join Licenses 
                             on DetainedLicenses.LicenseID = Licenses.LicenseID
                        where Licenses.LicenseID = @ID and IsReleased = 0";

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

        public static bool DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


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


        public static bool IsExsitByAppID(int AppID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from Licenses
                                 Where ApplicationID = @ID ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ID", AppID);
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
