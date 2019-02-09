using System;
using System.Linq;
using System.Collections.Generic;
using Npgsql;
using System.Configuration;

namespace WaterMeter.MobileAppService.Services
{
    public class DBService
    {
        public static string ConnectionString { get; set; }

        public int Insert(TStorage obj)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    string fields = string.Empty;
                    string values = string.Empty;
                    for (int i = 0; i < obj.Fields.Count; i++)
                    {
                        var field = obj.Fields.ElementAt(i);
                        if (field.Key == obj.PKField)
                            continue;

                        string p = "p" + i.ToString();
                        cmd.Parameters.AddWithValue(p, field.Value);
                        fields += field.Key;
                        values += "@" + p;

                        if (i != obj.Fields.Count - 1)
                        {
                            fields += ", ";
                            values += ", ";
                        }
                    }

                    cmd.CommandText =
                        "INSERT INTO " + obj.Table + " (" + fields + ") VALUES (" + values + ") RETURNING " + obj.PKField;

                    object retVal = cmd.ExecuteScalar();
                    return Convert.ToInt32(retVal);
                }
            }
        }

        public void Update(TStorage obj)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    string fields = string.Empty;
                    string values = string.Empty;
                    for (int i = 0; i < obj.Fields.Count; i++)
                    {
                        var field = obj.Fields.ElementAt(i);

                        string p = "p" + i.ToString();
                        cmd.Parameters.AddWithValue(p, field.Value);
                        fields += field.Key + "=@" + p;

                        if (i != obj.Fields.Count - 1)
                        {
                            fields += ", ";
                        }
                    }

                    cmd.CommandText =
                        "UPDATE " + obj.Table + " SET " + fields + " WHERE " + obj.PKField + "=" + obj.ID;
                    cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(TStorage obj)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText =
                        "DELETE FROM " + obj.Table + " WHERE " + obj.PKField + "=" + obj.ID;
                    cmd.ExecuteScalar();
                }
            }
        }

        public TStorage LoadDetails(TStorage query)
        {
            TStorage result = null;
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM " + query.Table + " WHERE " + query.PKField + "=" + query.ID;
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        result = new TStorage();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string key = reader.GetName(i);
                            object value = reader.GetValue(i);
                            result.Fields.Add(key, value);
                        }
                    }
                }
            }
            return result;
        }

        public IEnumerable<TStorage> LoadList(TStorage query)
        {
            List<TStorage> result = new List<TStorage>();
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM " + query.Table;

                    if (query.Fields != null && query.Fields.Any())
                    {
                        cmd.CommandText += " WHERE ";

                        string fields = string.Empty;
                        for (int i = 0; i < query.Fields.Count; i++)
                        {
                            var field = query.Fields.ElementAt(i);

                            string p = "p" + i.ToString();
                            cmd.Parameters.AddWithValue(p, field.Value);
                            fields += field.Key + "=@" + p;

                            if (i != query.Fields.Count - 1)
                            {
                                fields += " AND ";
                            }
                        }
                        cmd.CommandText += fields;
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TStorage s = new TStorage();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string key = reader.GetName(i);
                                object value = reader.GetValue(i);
                                s.Fields.Add(key, value);
                            }
                            result.Add(s);
                        }
                    }
                }
            }
            return result;
        }
    }
}