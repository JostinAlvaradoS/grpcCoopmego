using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace grpc_postgresql_project.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertUser(string name, string cedula, string loginName, string loginGit)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand("insert_user", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("cedula", cedula);
            command.Parameters.AddWithValue("login_name", loginName);
            command.Parameters.AddWithValue("login_git", loginGit);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = new List<User>();
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand("get_all_users", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Cedula = reader.GetString(2),
                    LoginName = reader.GetString(3),
                    LoginGit = reader.GetString(4)
                });
            }

            return users;
        }

        public void UpdateUser(int id, string name, string cedula, string loginName, string loginGit)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand("update_user", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("cedula", cedula);
            command.Parameters.AddWithValue("login_name", loginName);
            command.Parameters.AddWithValue("login_git", loginGit);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void DeleteUser(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand("delete_user", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}