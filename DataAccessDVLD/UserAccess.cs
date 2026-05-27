using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class UserAccess
    {
        public static bool GetUserByID(int UserID, ref string username, ref string password, ref int PersonID, ref bool isActive)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * from Users where UserID = @ID";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", UserID);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    username = (string)reader["UserName"];
                    password = (string)reader["Password"];
                    PersonID = (int)reader["PersonID"];
                    isActive = Convert.ToBoolean(reader["IsActive"]);

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
        public static bool GetUserByUserName(string username, ref int UserID, ref string password, ref int PersonID, ref bool isActive)
        {
            bool isFound = false;
            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = "select * from Users where UserName = @username";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@username", username);


            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    UserID = (int)reader["UserID"];
                    password = (string)reader["Password"];
                    PersonID = (int)reader["PersonID"];
                    isActive = Convert.ToBoolean(reader["IsActive"]);

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
        public static bool IsPersonHasUser(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = "SELECT Found = 1 FROM Users WHERE PersonID = @PersonID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            bool isFound = false;

            try
            {
                connection.Open();
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                //isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int AddUser(string username, string password, int PersonID, bool isActive)
        {


            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO Users (PersonID, UserName , Password , IsActive)
                             VALUES (@PersonID,@UserName,@Password, @IsActive);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@UserName", username);
            cmd.Parameters.AddWithValue("@Password", password);

            cmd.Parameters.AddWithValue("@IsActive", isActive);


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

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool isActive)
        {

            int RowEffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"Update  Users  
                            set UserName = @UserName, 
                                PersonID = @PersonID,
                                Password = @Password, 
                                IsActive = @isActive
                                where UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@IsActive", isActive);
            cmd.Parameters.AddWithValue("@UserID", UserID);



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

        public static DataTable GetUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"SELECT UserID, Users.PersonID ,
                      FirstName + ' ' + SecondName + ' ' + ThirdName + ' ' + LastName AS FullName  
                , UserName ,  IsActive FROM Users  INNER JOIN People  ON People.PersonID = Users.PersonID ";

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

        public static bool DeleteUser(int ID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string Query = @"Delete Users where UserID = @ID";

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

        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password,
      ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE Username = @Username and Password=@Password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsUserExist(int ID)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from Users
                                 Where UserID = @ID ";

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

        public static bool IsUserExist(string UserName)
        {
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);
            string query = @"Select Found = 1 from Users
                                 Where UserName = @UserName ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserName", UserName);
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

        public static bool ChangePassword(int UserID, string NewPassword)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"Update  Users  
                            set Password = @Password
                            where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", NewPassword);


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

