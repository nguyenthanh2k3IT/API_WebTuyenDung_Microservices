﻿using MediatR;

namespace BuildingBlock.CQRS;

public interface ICommand : ICommand<Unit> { }

public interface ICommand<out TResponse> : IRequest<TResponse> { }


public interface ICommandHandler<in TCommand>
	: ICommandHandler<TCommand, Unit>
	where TCommand : ICommand<Unit>
{ }

public interface ICommandHandler<in TCommand, TResponse>
	: IRequestHandler<TCommand, TResponse>
	where TCommand : ICommand<TResponse>
	where TResponse : notnull
{ }
