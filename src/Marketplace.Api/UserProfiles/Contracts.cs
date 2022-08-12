namespace Marketplace.UserProfiles;

public static class Contracts
{
    public static class V1
    {
        public class RegisterUser
        {
            public Guid Id { get; init; }

            public string FullName { get; init; } = "";

            public string DisplayName { get; init; } = "";
        }

        public class UpdateUserFullName
        {
            public Guid Id { get; init; }

            public string FullName { get; init; } = "";
        }

        public class UpdateUserDisplayName
        {
            public Guid Id { get; init; }

            public string DisplayName { get; init; } = "";
        }

        public class UpdateUserProfilePhoto
        {
            public Guid Id { get; init; }

            public string PhotoUrl { get; init; } = "";
        }
    }
}
