using ClinicApp.Domain.Entities;
using Xunit;

namespace ClinicApp.Tests;

public class DomainTests
{
    [Fact]
    public void Patient_FullName_ShouldReturnCorrectFormat()
    {
        // Arrange
        var patient = new Patient
        {
            FirstName = "Иван",
            LastName = "Иванов"
        };

        // Act
        var fullName = patient.FullName;

        // Assert
        Assert.Equal("Иванов Иван", fullName);
    }

    [Fact]
    public void User_FullName_ShouldReturnCorrectValue()
    {
        // Arrange
        var user = new User
        {
            FullName = "Петров Петр Сергеевич"
        };

        // Act
        var fullName = user.FullName;

        // Assert
        Assert.Equal("Петров Петр Сергеевич", fullName);
    }

    [Fact]
    public void Appointment_DefaultStatus_ShouldNotBeCancelled()
    {
        // Arrange
        var appointment = new Appointment();

        // Assert
        Assert.False(appointment.IsCancelled);
    }
}
