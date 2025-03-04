using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Threading.Tasks;

namespace grpc_postgresql_project.Data
{
    public class ObjectRepository
    {
        private readonly string _connectionString;

        public ObjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Object> GetObjectByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new NpgsqlCommand("get_object_by_id", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("object_id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Object
                {
                    ReqId = reader.GetInt32(reader.GetOrdinal("req_id")),
                    BaseId = reader.GetInt32(reader.GetOrdinal("base_id")),
                    SP = reader.GetString(reader.GetOrdinal("sp"))
                };
            }

            return null;
        }

        public async Task InsertObjectAsync(Object obj)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new NpgsqlCommand("insert_object", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("req_id", obj.ReqId);
            command.Parameters.AddWithValue("base_id", obj.BaseId);
            command.Parameters.AddWithValue("sp", obj.SP);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateObjectAsync(Object obj)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new NpgsqlCommand("update_object", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("req_id", obj.ReqId);
            command.Parameters.AddWithValue("base_id", obj.BaseId);
            command.Parameters.AddWithValue("sp", obj.SP);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteObjectAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new NpgsqlCommand("delete_object", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("object_id", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}