using System;

namespace grpc_postgresql_project.Models
{
    public class Requirement
    {
        public string Justification { get; set; }
        public string Detail { get; set; }
        public string GitLink { get; set; }
        public DateTime RegistrationDate { get; set; }
        public TimeSpan RegistrationTime { get; set; }
        public DateTime ManualDate { get; set; }
        public string TicketMesa { get; set; }
        public string Programmer { get; set; }
        public string Requester { get; set; }
        public string System { get; set; }
        public string QA { get; set; }
    }
}