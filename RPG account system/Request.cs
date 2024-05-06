using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPG_account_system
{
    public class Request
    {
        protected static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Host=localhost;Username=postgres;Password=paloma123;Database=postgres;Encoding=UTF8");
        }

        public static object RequestSQL(string request)
        {
            try
            {
                StringBuilder value = new StringBuilder();
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();

                    string sql = request;
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            return reader;
                            while (reader.Read())
                            {
                                int playerId = reader.GetInt32(0);
                                value.Append(playerId.ToString() + " "); 
                                value.Append("\n");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }
    }
}
