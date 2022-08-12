using Marketplace.Domain.UserProfile.ValueObjects;

namespace Marketplace.Domain.UserProfile;

public interface IUserProfileRepository
{
    Task LoadAsync(UserId id);

    Task AddAsync(UserProfile entity);

    Task<bool> ExistsAsync(UserId id);
}
