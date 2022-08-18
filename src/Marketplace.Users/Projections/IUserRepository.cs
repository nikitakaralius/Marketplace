namespace Marketplace.Users.Projections;

public interface IUserRepository
{
    Task AddAsync(UserDetails user);

    Task<UserDetails?> ByIdAsync(Guid id);

    Task SaveChangesAsync();
}
