using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using grpc_postgresql_project.Data;
using grpc_postgresql_project.Models;

namespace grpc_postgresql_project.Services
{
    public class UserService : UserService.UserServiceBase
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(UserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public override async Task<InsertUserResponse> InsertUser(InsertUserRequest request, ServerCallContext context)
        {
            var user = new User
            {
                Name = request.Name,
                Cedula = request.Cedula,
                LoginName = request.LoginName,
                LoginGit = request.LoginGit
            };

            var result = await _userRepository.InsertUserAsync(user);
            return new InsertUserResponse { Success = result };
        }

        public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            return new GetUserResponse
            {
                User = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Cedula = user.Cedula,
                    LoginName = user.LoginName,
                    LoginGit = user.LoginGit
                }
            };
        }

        public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var user = new User
            {
                Id = request.Id,
                Name = request.Name,
                Cedula = request.Cedula,
                LoginName = request.LoginName,
                LoginGit = request.LoginGit
            };

            var result = await _userRepository.UpdateUserAsync(user);
            return new UpdateUserResponse { Success = result };
        }

        public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            var result = await _userRepository.DeleteUserAsync(request.Id);
            return new DeleteUserResponse { Success = result };
        }
    }
}