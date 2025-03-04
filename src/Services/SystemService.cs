using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using YourNamespace.Models; // Adjust the namespace as necessary
using YourNamespace.Data; // Adjust the namespace as necessary

namespace YourNamespace.Services // Adjust the namespace as necessary
{
    public class SystemService : SystemService.SystemServiceBase
    {
        private readonly SystemRepository _systemRepository;

        public SystemService(SystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        public override async Task<SystemResponse> InsertSystem(SystemRequest request, ServerCallContext context)
        {
            var system = new System
            {
                Name = request.Name,
                URL = request.Url,
                ImplementationDate = request.ImplementationDate.ToDateTime(),
                CurrentVersion = request.CurrentVersion,
                Logo = request.Logo,
                Description = request.Description,
                Validity = request.Validity,
                Type = request.Type,
                ProductionDate = request.ProductionDate.ToDateTime(),
                UpdateDate = request.UpdateDate.ToDateTime()
            };

            var result = await _systemRepository.InsertSystemAsync(system);
            return new SystemResponse { Success = result };
        }

        public override async Task<SystemResponse> GetSystem(SystemIdRequest request, ServerCallContext context)
        {
            var system = await _systemRepository.GetSystemByIdAsync(request.Id);
            if (system == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "System not found"));
            }

            return new SystemResponse
            {
                Id = system.Id,
                Name = system.Name,
                URL = system.URL,
                ImplementationDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(system.ImplementationDate),
                CurrentVersion = system.CurrentVersion,
                Logo = system.Logo,
                Description = system.Description,
                Validity = system.Validity,
                Type = system.Type,
                ProductionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(system.ProductionDate),
                UpdateDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(system.UpdateDate)
            };
        }

        public override async Task<SystemResponse> UpdateSystem(SystemRequest request, ServerCallContext context)
        {
            var system = new System
            {
                Id = request.Id,
                Name = request.Name,
                URL = request.Url,
                ImplementationDate = request.ImplementationDate.ToDateTime(),
                CurrentVersion = request.CurrentVersion,
                Logo = request.Logo,
                Description = request.Description,
                Validity = request.Validity,
                Type = request.Type,
                ProductionDate = request.ProductionDate.ToDateTime(),
                UpdateDate = request.UpdateDate.ToDateTime()
            };

            var result = await _systemRepository.UpdateSystemAsync(system);
            return new SystemResponse { Success = result };
        }

        public override async Task<SystemResponse> DeleteSystem(SystemIdRequest request, ServerCallContext context)
        {
            var result = await _systemRepository.DeleteSystemAsync(request.Id);
            return new SystemResponse { Success = result };
        }
    }
}