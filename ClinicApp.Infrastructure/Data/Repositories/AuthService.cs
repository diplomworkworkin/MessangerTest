using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Data.Repositories;

public class AuthService : IAuthService
{
    private readonly ClinicDbContext _context;

    public User? CurrentUser { get; private set; }

    public AuthService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        // В реальном приложении здесь должна быть проверка хэша пароля
        // Для дипломного проекта используем упрощенную проверку
        var user = await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);

        if (user != null)
        {
            CurrentUser = user;
            return true;
        }

        return false;
    }

    public void Logout()
    {
        CurrentUser = null;
    }

    public bool IsInRole(UserRole role)
    {
        return CurrentUser?.Role == role;
    }
}
