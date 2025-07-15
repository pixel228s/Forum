namespace Forum.Application.Abstractions.Messaging
{
    public interface ICommandHandler<in TCommand, TResponse>
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
