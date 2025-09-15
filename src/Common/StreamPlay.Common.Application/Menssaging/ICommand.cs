using MediatR;
using StreamPlay.Common.Domain;

namespace StreamPlay.Common.Application.Menssaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;