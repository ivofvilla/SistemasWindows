using API.Dados;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand, Unit>
{
    private readonly DbContexto _contexto;

    public DeleteClienteHandler(DbContexto contexto) => _contexto = contexto;

    public async Task<Unit> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _contexto.Clientes.Include(c => c.Endereco)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException("Cliente não encontrado");

        _contexto.Clientes.Remove(cliente);
        await _contexto.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
