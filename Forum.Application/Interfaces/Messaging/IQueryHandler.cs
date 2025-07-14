namespace Forum.Application.Abstractions.Messaging
{
    public interface IQueryHandler<in TQuery, DtoResponse> 
        where TQuery : IQuery<DtoResponse>
    {
        Task<DtoResponse> Handle(TQuery query, CancellationToken cancellationToken); 
    }
}
