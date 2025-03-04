using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using YourNamespace.Models; // Adjust the namespace as necessary
using YourNamespace.Data; // Adjust the namespace as necessary

public class RequirementService : RequirementService.RequirementServiceBase
{
    private readonly RequirementRepository _requirementRepository;

    public RequirementService(RequirementRepository requirementRepository)
    {
        _requirementRepository = requirementRepository;
    }

    public override async Task<RequirementResponse> InsertRequirement(RequirementRequest request, ServerCallContext context)
    {
        var requirement = new Requirement
        {
            Justification = request.Justification,
            Detail = request.Detail,
            GitLink = request.GitLink,
            RegistrationDate = request.RegistrationDate.ToDateTime(),
            RegistrationTime = request.RegistrationTime.ToDateTime(),
            ManualDate = request.ManualDate.ToDateTime(),
            TicketMesa = request.TicketMesa,
            Programmer = request.Programmer,
            Requester = request.Requester,
            System = request.System,
            QA = request.QA
        };

        var result = await _requirementRepository.InsertRequirementAsync(requirement);
        return new RequirementResponse { Success = result };
    }
}