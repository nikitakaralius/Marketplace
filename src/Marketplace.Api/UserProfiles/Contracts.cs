namespace Marketplace.UserProfiles;

public static class Contracts
{
    public static class V1
    {
        public interface ICommand { }

        public class RegisterUser : ICommand
        {
            public Guid Id { get; init; }

            public string FullName { get; init; } = "";

            public string DisplayName { get; init; } = "";
        }

        public class UpdateUserFullName : ICommand
        {
            public Guid Id { get; init; }

            public string FullName { get; init; } = "";
        }

        public class UpdateUserDisplayName : ICommand
        {
            public Guid Id { get; init; }

            public string DisplayName { get; init; } = "";
        }

        public class UpdateUserProfilePhoto : ICommand
        {
            public Guid Id { get; init; }

            public string PhotoUrl { get; init; } = "";
        }
    }
}
