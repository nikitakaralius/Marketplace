namespace Marketplace.ClassifiedAds;

public static class ClassifiedAdContract
{
    public static class V1
    {
        public interface ICommand { }

        public sealed class Create : ICommand
        {
            public Guid Id { get; init; }

            public Guid OwnerId { get; init; }
        }

        public sealed class SetTitle : ICommand
        {
            public Guid Id { get; init; }

            public string Title { get; init; } = "";
        }

        public sealed class UpdateDescription : ICommand
        {
            public Guid Id { get; init; }

            public string Description { get; init; } = "";
        }

        public sealed class UpdatePrice : ICommand
        {
            public Guid Id { get; init; }

            public decimal Price { get; init; }

            public string Currency { get; init; } = "";
        }

        public sealed class RequestToPublish : ICommand
        {
            public Guid Id { get; init; }
        }

        public sealed class Publish : ICommand
        {
            public Guid Id { get; init; }

            public Guid ApprovedBy { get; init; }
        }
    }
}
