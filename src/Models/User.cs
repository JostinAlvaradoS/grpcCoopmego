using System;

namespace grpc_postgresql_project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cedula { get; set; }
        public string LoginName { get; set; }
        public string LoginGit { get; set; }
    }
}