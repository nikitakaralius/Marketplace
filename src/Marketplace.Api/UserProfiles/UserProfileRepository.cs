using Marketplace.Domain.UserProfile;
using Marketplace.Domain.UserProfile.ValueObjects;
using Marketplace.Infrastructure.EntityFramework;

namespace Marketplace.UserProfiles;

internal sealed class UserProfileRepository : IUserProfileRepository, IDisposable
{
    private readonly MarketplaceDbContext _dbContext;

    public UserProfileRepository(MarketplaceDbContext dbContext) => _dbContext = dbContext;

    public async Task<UserProfile?> LoadAsync(UserId id) =>
        await _dbContext.UserProfiles.FindAsync(id.Value);

    public async Task AddAsync(UserProfile entity) =>
        await _dbContext.UserProfiles.AddAsync(entity);

    public async Task<bool> ExistsAsync(UserId id) =>
        await _dbContext.UserProfiles.FindAsync(id.Value) is not null;

    public void Dispose() => _dbContext.Dispose();
}
