using Base_Atestat.Debug;
using MySql.Data.MySqlClient;
namespace Base_Atestat.DB
{
    public class DBWrapper
    {
        #region infos
        private static MySqlConnection _connection;
        private static string server;
        private static string database;
        private static string username;
        private static string password;
        #endregion

        public DBWrapper()
        {
           
        }

        public static void StartDB()
        {
            //DB Login Informations
            server = "localhost";
            database = "atestat";
            username = "root";
            password = "";

            string connString;
            connString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={password}";

            _connection = new MySqlConnection(connString);

            //Check if we have connection with DB
            if(isConnected())
            {
                PrintConsole.printDB("Initialized!");
                _connection.Close();
            }
            else
            {
                PrintConsole.printDB("Connection Failed!");
                _connection.Close();
            }

        }
        private static bool isConnected()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch(MySqlException ex) {
            
                switch(ex.ErrorCode)
                {
                    case 0:
                        PrintConsole.printDB("Connection to server failed!");
                        break;
                    case 1045:
                        PrintConsole.printDB("database login username or password is incorrect!");
                        break;
                }
                return false;

            }
        }


        public object getItem(int playerId, int objectId)
        {
            string query = $"SELECT * FROM users WHERE id='{playerId}";
            try
            {
                if(isConnected())
                {
                    MySqlCommand cmd = new MySqlCommand(query, _connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        object obj = reader.GetValue(objectId);
                        reader.Close();
                        _connection.Close();
                        return obj;
                    }
                    else
                    {
                        reader.Close();
                        _connection.Close();
                        return false;
                    }
                }
                else
                {
                    _connection.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _connection.Close();
                PrintConsole.printDB("getItem error: " + ex.ToString());
                return false;
            }
        }

        public object setItem(int playerId, string objectName, string itemString, int itemNumber)
        {
            string query;
            if(itemString==null)
            {
                query = $"UPDATE 'users' SET '{objectName}' = '{itemNumber}' WHERE 'users'.'id' = {playerId}";
            }    
            else {
                query = $"UPDATE 'users' SET '{objectName}' = '{itemString}' WHERE 'users'.'id' = {playerId}";
            }

            try
            {
                if (isConnected())
                {
                    MySqlCommand cmd = new MySqlCommand(query, _connection);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        _connection.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                else
                {
                    _connection.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                PrintConsole.printDB("getItem error: " + ex.ToString());
                _connection.Close();
                return false;
            }
        }
    }
}
