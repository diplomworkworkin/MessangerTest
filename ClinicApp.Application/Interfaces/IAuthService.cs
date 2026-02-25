using ClinicApp.Domain.Entities;

namespace ClinicApp.Application.Interfaces;

public interface IAuthService
{
    User? CurrentUser { get; }
    Task<bool> LoginAsync(string username, string password);
    void Logout();
    bool IsInRole(UserRole role);
}
