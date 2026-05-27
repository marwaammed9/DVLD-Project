using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DataAccessDVLD
{
    public class PeopleAccess
    {
        public static bool GetPersonByID(int ID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref string NationlNo, ref string Email, ref string Phone,
            ref string Address, ref int CountryID, ref int Gendor, ref string ImagePath, ref DateTime DateOfBirth)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * from People where PersonID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    CountryID = (int)reader["NationalityCountryID"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    NationlNo = (string)reader["NationalNo"];


                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }


                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
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

        public static bool GetPersonByNationlNo(string NationlNo, ref int ID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref string Email, ref string Phone,
          ref string Address, ref int CountryID, ref int Gendor, ref string ImagePath, ref DateTime DateOfBirth)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * from People where NationalNo = @NationalNo";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationlNo);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    CountryID = (int)reader["NationalityCountryID"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    ID = (int)reader["PersonID"];


                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }


                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
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
        public static int AddPerson(string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo, string Email, string Phone,
             string Address, int CountryID, int Gendor, string ImagePath, DateTime DateOfBirth)
        {


            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO People (FirstName, SecondName, ThirdName ,LastName,NationalNo,Gendor ,Email, Phone, Address,DateOfBirth, NationalityCountryID,ImagePath)
                             VALUES (@FirstName,@SecondName,@ThirdName, @LastName,@NationalNo, @Gendor ,@Email, @Phone, @Address,@DateOfBirth, @CountryID,@ImagePath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);
            cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Gendor", Gendor);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != "")
            {
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            if (Email != "")
            {
                cmd.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Email", System.DBNull.Value);
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



        public static bool UpdatePeople(int ID, string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo, string Email, string Phone,
             string Address, int CountryID, int Gendor, string ImagePath, DateTime DateOfBirth)
        {

            int RowEffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"Update  People  
                            set FirstName = @FirstName, 
                                SecondName = @SecondName, 
                                ThirdName = @ThirdName, 
                                LastName = @LastName, 
                                Email = @Email, 
                                Phone = @Phone, 
                                Address = @Address, 
                                Gendor = @Gendor ,
                                NationalNo=@NationalNo,
                                DateOfBirth = @DateOfBirth,
                                NationalityCountryID = @CountryID,
                                ImagePath =@ImagePath
                                where PersonID = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);
            cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@Gendor", Gendor);
            cmd.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != "")
            {
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            if (Email != "")
            {
                cmd.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Email", System.DBNull.Value);
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

        public static DataTable GetPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"select People.PersonID ,
                People.NationalNo , People.FirstName , People.SecondName , People.ThirdName , People.LastName ,
People.DateOfBirth , People.Address ,people.Phone ,People.Email ,Countries.CountryName ,case when People.Gendor = 0 then 'Male' else 'Female'
End As GendorCaption ,
People.ImagePath from People inner join Countries On
People.NationalityCountryID = Countries.CountryID order by PersonID asc";
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

        public static bool DeletePeople(int ID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = @"Delete People where PersonID = @ID";

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
            string query = @"Select Found = 1 from People
                                 Where PersonID = @ID ";

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

        public static bool IsExsit(string NationaNo)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from People
                                 Where NationalNo = @No ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@No", NationaNo);
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


        public static bool IsDriver(int PresonID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"select found = 1 from People inner join Drivers on Drivers.PersonID = People.PersonID where People.PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PersonID", PresonID);
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



