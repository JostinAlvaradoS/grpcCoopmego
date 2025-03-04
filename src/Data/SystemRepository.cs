using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace grpc_postgresql_project.Data
{
    public class SystemRepository
    {
        private readonly string _connectionString;

        public SystemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertSystem(System system)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand("insert_system", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("name", system.Name);
            command.Parameters.AddWithValue("url", system.URL);
            command.Parameters.AddWithValue("implementation_date", system.ImplementationDate);
            command.Parameters.AddWithValue("current_version", system.CurrentVersion);
            command.Parameters.AddWithValue("logo", system.Logo);
            command.Parameters.AddWithValue("description", system.Description);
            command.Parameters.AddWithValue("validity", system.Validity);
            command.Parameters.AddWithValue("type", system.Type);
            command.Parameters.AddWithValue("production_date", system.ProductionDate);
            command.Parameters.AddWithValue("update_date", system.UpdateDate);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public System GetSystemById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand("get_system_by_id", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new System
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    URL = reader.GetString(2),
                    ImplementationDate = reader.GetDateTime(3),
                    CurrentVersion = reader.GetString(4),
                    Logo = reader.GetString(5),
                    Description = reader.GetString(6),
                    Validity = reader.GetBoolean(7),
                    Type = reader.GetString(8),
                    ProductionDate = reader.GetDateTime(9),
                    UpdateDate = reader.GetDateTime(10)
                };
            }
            return null;
        }

        public void UpdateSystem(System system)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand("update_system", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("id", system.Id);
            command.Parameters.AddWithValue("name", system.Name);
            command.Parameters.AddWithValue("url", system.URL);
            command.Parameters.AddWithValue("implementation_date", system.ImplementationDate);
            command.Parameters.AddWithValue("current_version", system.CurrentVersion);
            command.Parameters.AddWithValue("logo", system.Logo);
            command.Parameters.AddWithValue("description", system.Description);
            command.Parameters.AddWithValue("validity", system.Validity);
            command.Parameters.AddWithValue("type", system.Type);
            command.Parameters.AddWithValue("production_date", system.ProductionDate);
            command.Parameters.AddWithValue("update_date", system.UpdateDate);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void DeleteSystem(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand("delete_system", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}