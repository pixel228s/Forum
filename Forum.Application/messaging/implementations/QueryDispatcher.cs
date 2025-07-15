using Forum.Application.Abstractions.Messaging;
using Forum.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Application.messaging.implementations
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken)
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
            return handler.Handle(query, cancellationToken);
        }
    }
}
