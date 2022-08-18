namespace Marketplace.EntityFramework.Concrete;

internal sealed class UserRepository : IUserRepository
{
    private readonly MarketplaceDbContext _context;

    public UserRepository(MarketplaceDbContext context) =>
        _context = context;

    public async Task AddAsync(UserDetails user) =>
        await _context.Users.AddAsync(user);

    public async Task<UserDetails?> ByIdAsync(Guid id) =>
        await _context.Users.FindAsync(id);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
