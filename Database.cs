using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace swami2021
{
    public class Database
    {
        public string ConnString { get; set; }
        public MySqlConnection Conn { get; set; }

        public Database()
        {
            string server = Environment.GetEnvironmentVariable("swami_database_server");
            string name = Environment.GetEnvironmentVariable("swami_database_name");
            string port = Environment.GetEnvironmentVariable("swami_database_port");
            string username = Environment.GetEnvironmentVariable("swami_database_username");
            string password = Environment.GetEnvironmentVariable("swami_database_password");
            Console.WriteLine("got the datbase " + server);

            this.ConnString = $@"server = {server};user={username};database={name};port={port};password={password};";
            this.Conn = new MySqlConnection(this.ConnString);
        }

        public void Open()
        {
            this.Conn.Open();
        }

        public void Close()
        {
            this.Conn.Close();
        }

        public List<ExpandoObject> Select(string query, Dictionary<string, object> values = null)
        {
            List<ExpandoObject> results = new();
            try
            {
                using var cmd = new MySqlCommand(query, this.Conn);
                if (values != null)
                {
                    foreach (var p in values)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }

                }

                using var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var temp = new ExpandoObject() as IDictionary<string, Object>;
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        string name = char.ToLower(rdr.GetName(i)[0]) + rdr.GetName(i).Substring(1);
                        temp.TryAdd(name, rdr.GetValue(i));
                    }

                    results.Add((ExpandoObject)temp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Select Query Error");
                Console.WriteLine(e.Message);
            }

            return results;
        }

        public void Insert(string query, Dictionary<string, object> values)
        {
            QueryWithData(query, values);
        }

        public void Update(string query, Dictionary<string, object> values)
        {
            QueryWithData(query, values);
        }

        private void QueryWithData(string query, Dictionary<string, object> values)
        {
            try
            {
                using var cmd = new MySqlCommand(query, this.Conn);
                foreach (var p in values)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Inserting Data");
                Console.WriteLine(e.Message);
            }
        }

        public void StoredProc(string procName)
        {
            try
            {
                using var cmd = new MySqlCommand(procName, this.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                int rows_affected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rows_affected} Rows Effected By {procName}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Running Stored Proc: {procName}" +
                    Environment.NewLine + e.Message);
            }
        }

    }
}