using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using YourNamespace.Models; // Adjust the namespace as necessary
using YourNamespace.Data; // Adjust the namespace as necessary
using YourNamespace.Protos; // Adjust the namespace as necessary

namespace YourNamespace.Services // Adjust the namespace as necessary
{
    public class ObjectService : ObjectService.ObjectServiceBase
    {
        private readonly ObjectRepository _objectRepository;

        public ObjectService(ObjectRepository objectRepository)
        {
            _objectRepository = objectRepository;
        }

        public override async Task<ObjectResponse> InsertObject(ObjectRequest request, ServerCallContext context)
        {
            var obj = new Object
            {
                ReqId = request.ReqId,
                BaseId = request.BaseId,
                SP = request.SP
            };

            var result = await _objectRepository.InsertAsync(obj);
            return new ObjectResponse { Success = result };
        }

        public override async Task<ObjectResponse> GetObject(ObjectIdRequest request, ServerCallContext context)
        {
            var obj = await _objectRepository.GetByIdAsync(request.Id);
            return new ObjectResponse
            {
                Success = obj != null,
                Object = obj != null ? new ObjectMessage
                {
                    ReqId = obj.ReqId,
                    BaseId = obj.BaseId,
                    SP = obj.SP
                } : null
            };
        }

        public override async Task<ObjectResponse> UpdateObject(ObjectRequest request, ServerCallContext context)
        {
            var obj = new Object
            {
                ReqId = request.ReqId,
                BaseId = request.BaseId,
                SP = request.SP
            };

            var result = await _objectRepository.UpdateAsync(obj);
            return new ObjectResponse { Success = result };
        }

        public override async Task<ObjectResponse> DeleteObject(ObjectIdRequest request, ServerCallContext context)
        {
            var result = await _objectRepository.DeleteAsync(request.Id);
            return new ObjectResponse { Success = result };
        }
    }
}