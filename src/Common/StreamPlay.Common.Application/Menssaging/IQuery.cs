using MediatR;
using StreamPlay.Common.Domain;

namespace StreamPlay.Common.Application.Menssaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;