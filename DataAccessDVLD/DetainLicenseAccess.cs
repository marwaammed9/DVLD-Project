using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class DetainLicenseAccess
    {
        public static bool GetDetainLicenseByID(int ID, ref int LicenseID, ref int CreatedByUserID, ref decimal FineFees, ref DateTime DetainDate,
       ref int? ReleasedByUserID, ref DateTime? ReleaseDate, ref int? ReleaseApplicationID, ref bool IsReleased)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "select * from DetainedLicenses where DetainID = @ID";
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
                    FineFees = (decimal)reader["FineFees"];
                    LicenseID = (int)reader["LicenseID"];
                    IsReleased = (bool)reader["IsReleased"];
                    DetainDate = (DateTime)reader["DetainDate"];

                    if (reader["ReleaseDate"] != DBNull.Value)
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }
                    else
                    {
                        ReleaseDate = null;
                    }

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    else
                    {
                        ReleaseDate = null;
                    }

                    if (reader["ReleasedByUserID"] != DBNull.Value)
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }
                    else
                    {
                        ReleasedByUserID = null;
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

        public static bool UpdateDetainLicense(int ID, int LicenseID, int CreatedByUserID, decimal FineFees, DateTime DetainDate,
        int? ReleasedByUserID, DateTime? ReleaseDate, int? ReleaseApplicationID, bool IsReleased)
        {

            int RowEffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"Update  DetainedLicenses  
                            set LicenseID = @LicenseID,                             
                                CreatedByUserID = @CreatedByUserID, 
                                FineFees = @FineFees, 
                                DetainDate = @DetainDate, 
                                ReleasedByUserID = @ReleasedByUserID, 
                                ReleaseDate = @ReleaseDate, 
                                ReleaseApplicationID = @ReleaseApplicationID ,
                                IsReleased = @IsReleased
                                where DetainID = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsReleased", IsReleased);
            cmd.Parameters.AddWithValue("@FineFees", FineFees);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);




            if (ReleaseApplicationID != null)
            {
                cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ReleaseApplicationID", System.DBNull.Value);
            }
            if (ReleaseDate != null)
            {
                cmd.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ReleaseDate", System.DBNull.Value);
            }
            if (ReleasedByUserID != null)
            {
                cmd.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ReleasedByUserID", System.DBNull.Value);
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

        public static bool ReleaseDetainLicense(int DetainID,
       int ReleasedByUserID, DateTime ReleaseDate, int ReleaseApplicationID)
        {

            int RowEffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"Update  DetainedLicenses  
                            set ReleasedByUserID = @ReleasedByUserID, 
                                ReleaseDate = @ReleaseDate, 
                                ReleaseApplicationID = @ReleaseApplicationID ,
                                IsReleased = 1
                                where DetainID = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);







            cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            cmd.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            cmd.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            cmd.Parameters.AddWithValue("@ID", DetainID);

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

        public static bool GetDetainLicenseByLicenseID(int LicenseID, ref int ID, ref int CreatedByUserID, ref decimal FineFees, ref DateTime DetainDate,
       ref int? ReleasedByUserID, ref DateTime? ReleaseDate, ref int? ReleaseApplicationID, ref bool IsReleased)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string Query = "select * FROM DetainedLicenses where LicenseID = @ID and IsReleased = 0";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", LicenseID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    FineFees = (decimal)reader["FineFees"];
                    ID = (int)reader["DetainID"];
                    IsReleased = (bool)reader["IsReleased"];
                    DetainDate = (DateTime)reader["DetainDate"];

                    if (reader["ReleaseDate"] != DBNull.Value)
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }
                    else
                    {
                        ReleaseDate = null;
                    }

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    else
                    {
                        ReleaseDate = null;
                    }

                    if (reader["ReleasedByUserID"] != DBNull.Value)
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }
                    else
                    {
                        ReleasedByUserID = null;
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



        public static int AddDetainLicnese(int LicenseID, int CreatedByUserID, decimal FineFees, DateTime DetainDate,
        int? ReleasedByUserID, DateTime? ReleaseDate, int? ReleaseApplicationID, bool IsReleased)
        {


            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses (LicenseID, FineFees, CreatedByUserID ,DetainDate,ReleasedByUserID,ReleaseDate ,ReleaseApplicationID ,IsReleased )
                             VALUES (@LicenseID,@FineFees,@CreatedByUserID, @DetainDate, @ReleasedByUserID ,@ReleaseDate,@ReleaseApplicationID,@IsReleased );
                             SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);



            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsReleased", IsReleased);
            cmd.Parameters.AddWithValue("@FineFees", FineFees);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);




            if (ReleaseApplicationID != null)
            {
                cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ReleaseApplicationID", System.DBNull.Value);
            }
            if (ReleaseDate != null)
            {
                cmd.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ReleaseDate", System.DBNull.Value);
            }
            if (ReleasedByUserID != null)
            {
                cmd.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ReleasedByUserID", System.DBNull.Value);
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



        public static DataTable GetDetainLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = "select * from DetainedLicenses";
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
        public static DataTable GetDetainLicensesForMain()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = "select * from DetainedLicenses_View";
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


        public static bool IsDetain(int ID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from DetainedLicenses
                                 Where DetainID = @ID ";

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

        public static bool IsDetainByLicenseID(int LcienseID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from DetainedLicenses
                                 Where LicenseID = @ID ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ID", LcienseID);
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



