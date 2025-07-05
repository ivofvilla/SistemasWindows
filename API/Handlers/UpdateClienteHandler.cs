using API.Dados;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand, Unit>
{
    private readonly DbContexto _contexto;

    public UpdateClienteHandler(DbContexto contexto) => _contexto = contexto;

    public async Task<Unit> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _contexto.Clientes.Include(c => c.Endereco)
            .FirstOrDefaultAsync(c => c.CPF == request.CPF, cancellationToken)
            ?? throw new KeyNotFoundException("Cliente não encontrado");

        cliente.CPF = request.CPF;
        cliente.Nome = request.Nome;
        cliente.RG = request.RG;
        cliente.DataExpedicao = request.DataExpedicao;
        cliente.OrgaoExpedicao = request.OrgaoExpedicao;
        cliente.UF = request.UF;
        cliente.DataNascimento = request.DataNascimento;
        cliente.Sexo = request.Sexo;
        cliente.EstadoCivil = request.EstadoCivil;

        var endereco = cliente.Endereco;
        endereco.CEP = request.CEP;
        endereco.Logradouro = request.Logradouro;
        endereco.Numero = request.Numero;
        endereco.Complemento = request.Complemento;
        endereco.Bairro = request.Bairro;
        endereco.Cidade = request.Cidade;
        endereco.UF = request.EnderecoUF;

        await _contexto.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}