namespace MyApp.SharedDomain.Commands
{
    public sealed class CommandResponse
    {
        public bool Success { get; } = true;
        public required Guid Id { get; set; }
        public required string Message { get; set; }
    }
}
