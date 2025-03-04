using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace grpc_postgresql_project.Data
{
    public class RequirementRepository
    {
        private readonly string _connectionString;

        public RequirementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertRequirement(Requirement requirement)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand("insert_requirement", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("justification", requirement.Justification);
            command.Parameters.AddWithValue("detail", requirement.Detail);
            command.Parameters.AddWithValue("git_link", requirement.GitLink);
            command.Parameters.AddWithValue("registration_date", requirement.RegistrationDate);
            command.Parameters.AddWithValue("registration_time", requirement.RegistrationTime);
            command.Parameters.AddWithValue("manual_date", requirement.ManualDate);
            command.Parameters.AddWithValue("ticket_mesa", requirement.TicketMesa);
            command.Parameters.AddWithValue("programmer", requirement.Programmer);
            command.Parameters.AddWithValue("requester", requirement.Requester);
            command.Parameters.AddWithValue("system", requirement.System);
            command.Parameters.AddWithValue("qa", requirement.QA);

            connection.Open();
            command.ExecuteNonQuery();
        }

        // Additional methods for retrieving, updating, and deleting requirements can be added here.
    }
}