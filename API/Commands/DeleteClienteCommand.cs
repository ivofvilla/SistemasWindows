using API.Dados;
using MediatR;

public record DeleteClienteCommand(Guid Id) : IRequest<Unit>;

