using API.Dados;
using MediatR;

public record DeleteClienteCommand(string cpf) : IRequest<Unit>;

