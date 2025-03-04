using System;

namespace grpc_postgresql_project.Models
{
    public class System
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public DateTime ImplementationDate { get; set; }
        public string CurrentVersion { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public DateTime Validity { get; set; }
        public string Type { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}