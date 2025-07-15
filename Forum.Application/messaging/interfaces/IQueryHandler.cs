﻿namespace Forum.Application.Abstractions.Messaging
{
    public interface IQueryHandler<in TQuery, TQueryResult>
    {
        Task<TQueryResult> Handle(TQuery query, CancellationToken cancellationToken); 
    }
}
