using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;
using ClinicApp.Infrastructure.Data.Repositories;
using ClinicApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClinicApp.Tests;

public class AuthServiceTests
{
    private ClinicDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ClinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ClinicDbContext(options);
    }

    [Fact]
    public async Task LoginAsync_WithCorrectCredentials_ShouldReturnTrue()
    {
        // Arrange
        var dbContext = GetDbContext();
        var user = new User { Username = "test", PasswordHash = "password", FullName = "Test User", Role = UserRole.Admin };
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        var authService = new AuthService(dbContext);

        // Act
        var result = await authService.LoginAsync("test", "password");

        // Assert
        Assert.True(result);
        Assert.NotNull(authService.CurrentUser);
        Assert.Equal("test", authService.CurrentUser.Username);
    }

    [Fact]
    public async Task LoginAsync_WithWrongPassword_ShouldReturnFalse()
    {
        // Arrange
        var dbContext = GetDbContext();
        var user = new User { Username = "test", PasswordHash = "password", FullName = "Test User" };
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        var authService = new AuthService(dbContext);

        // Act
        var result = await authService.LoginAsync("test", "wrong");

        // Assert
        Assert.False(result);
        Assert.Null(authService.CurrentUser);
    }

    [Fact]
    public void IsInRole_ShouldReturnCorrectValue()
    {
        // Arrange
        var dbContext = GetDbContext();
        var user = new User { Username = "admin", PasswordHash = "123", Role = UserRole.Admin };
        var authService = new AuthService(dbContext);
        
        // Manual set for testing
        var prop = typeof(AuthService).GetProperty("CurrentUser");
        if (prop != null)
        {
            prop.SetValue(authService, user);
        }

        // Act & Assert
        Assert.True(authService.IsInRole(UserRole.Admin));
        Assert.False(authService.IsInRole(UserRole.Doctor));
    }
}
