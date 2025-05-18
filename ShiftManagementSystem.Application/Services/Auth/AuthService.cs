using System.Security.Cryptography;
using System.Text;
using ShiftManagementSystem.Application.DTOs.Auth;
using ShiftManagementSystem.Domain.Entities;
using ShiftManagementSystem.Domain.Interfaces;
using ShiftManagementSystem.Domain.Enums;

namespace ShiftManagementSystem.Application.Services.Auth;

public class AuthService
{
    
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Handle user login
    public async Task<(bool Success, string Message, string Token)> LoginAsync(LoginDto model)
    {
        
        if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
        {
            return (false, "Please enter both email and password", string.Empty);
        }

        // Find the user by email
        var user = await _userRepository.GetByEmailAsync(model.Email);
        
        
        if (user == null)
        {
            return (false, "Email or password is incorrect", string.Empty);
        }

        
        var hashedPassword = HashPassword(model.Password);
        if (user.PasswordHash != hashedPassword)
        {
            return (false, "Email or password is incorrect", string.Empty);
        }

        //temporary login solution
        var loginToken = $"user-{user.Id}-{DateTime.UtcNow.Ticks}";

        
        return (true, "You have successfully logged in", loginToken);
    }

   //new registration
    public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto model)
    {
       
        if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
        {
            return (false, "Please provide email and password");
        }

        // Check if email is already taken
        var emailAlreadyExists = await _userRepository.EmailExistsAsync(model.Email);
        if (emailAlreadyExists)
        {
            return (false, "This email is already registered");
        }

       
        var newUser = new User
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = HashPassword(model.Password),
            FullName = model.FullName,
            Role = Role.Employee 
        };

      
        await _userRepository.AddAsync(newUser);

        
        return (true, "Your account has been created successfully");
    }

  
    private string HashPassword(string password)
    {
       
        using var hasher = SHA256.Create();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashedBytes = hasher.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hashedBytes);
    }
} 