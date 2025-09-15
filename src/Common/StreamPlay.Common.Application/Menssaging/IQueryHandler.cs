using MediatR;
using StreamPlay.Common.Domain;

namespace StreamPlay.Common.Application.Menssaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;