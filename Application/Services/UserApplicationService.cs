using UserManagementApi.Domain.Models;
using UserManagementApi.Domain.Ports;

namespace UserManagementApi.Application.Services
{
    public class UserApplicationService
    {
        private readonly IUserRepository _userRepository;

        public UserApplicationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsersAsync() =>
            await _userRepository.GetAllAsync();

        public async Task<User?> GetUserByIdAsync(string id) =>
            await _userRepository.GetByIdAsync(id);

        public async Task CreateUserAsync(User user) =>
            await _userRepository.CreateAsync(user);

        public async Task UpdateUserAsync(string id, User user) =>
            await _userRepository.UpdateAsync(id, user);

        public async Task DeleteUserAsync(string id) =>
            await _userRepository.DeleteAsync(id);
    }
}