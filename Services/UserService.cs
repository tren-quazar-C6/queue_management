using queue_management.Data;
using queue_management.Models;
using queue_management.Responses;

namespace queue_management.Services;

public class UserService
{
    private readonly MySqlDbContext _context;

    public UserService(MySqlDbContext context)
    {
        _context = context;
    }

    public ServiceResponse<IEnumerable<User>> GetAllUsers()
    {
        var users = _context.users.ToList();
        return new ServiceResponse<IEnumerable<User>>()
        {
            Success = true,
            Data = users
        };
    }

    public ServiceResponse<User> GetUserById(int id)
    {
        var user = _context.users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return new ServiceResponse<User>()
            {
                Success = false,
                Data = null,
                Message = "User not found"
            };
        }

        return new ServiceResponse<User>()
        {
            Success = true,
            Data = user,
            Message = "User found"
        };
    }

    public ServiceResponse<User> GetUserByDocument(string documentNumber)
    {
        var user = _context.users.FirstOrDefault(u => u.DocumentNumber == documentNumber);

        if (user == null)
        {
            return new ServiceResponse<User>()
            {
                Success = false,
                Data = null,
                Message = "User not found"
            };
        }

        return new ServiceResponse<User>()
        {
            Success = true,
            Data = user,
            Message = "User found"
        };
    }

    public ServiceResponse<User> SaveUser(User user)
    {
        var userExists = _context.users.FirstOrDefault(u => u.DocumentNumber == user.DocumentNumber);

        if (userExists != null)
        {
            return new ServiceResponse<User>()
            {
                Success = false,
                Data = user,
                Message = "A user with that document number already exists"
            };
        }

        _context.users.Add(user);
        _context.SaveChanges();

        return new ServiceResponse<User>()
        {
            Success = true,
            Data = user,
            Message = "User created successfully"
        };
    }

    public ServiceResponse<User> UpdateUser(User user)
    {
        var userDb = _context.users.Find(user.Id);

        if (userDb == null)
        {
            return new ServiceResponse<User>()
            {
                Success = false,
                Data = null,
                Message = "User not found, cannot update"
            };
        }
        
        var documentTaken = _context.users.FirstOrDefault(u => u.DocumentNumber == user.DocumentNumber && u.Id != user.Id);

        if (documentTaken != null)
        {
            return new ServiceResponse<User>()
            {
                Success = false,
                Data = user,
                Message = "That document number is already used by another user"
            };
        }

        userDb.FullName = user.FullName;
        userDb.DocumentNumber = user.DocumentNumber;
        _context.SaveChanges();

        return new ServiceResponse<User>()
        {
            Success = true,
            Data = userDb,
            Message = "User updated successfully"
        };
    }

    public ServiceResponse<User> DeleteUser(User user)
    {
        var userDb = _context.users.Find(user.Id);

        if (userDb == null)
        {
            return new ServiceResponse<User>()
            {
                Success = false,
                Data = null,
                Message = "User not found, cannot delete"
            };
        }

        _context.users.Remove(userDb);
        _context.SaveChanges();

        return new ServiceResponse<User>()
        {
            Success = true,
            Data = userDb,
            Message = "User removed successfully"
        };
    }
}