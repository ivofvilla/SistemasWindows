using API.Dados;
using API.Modelos;
using MediatR;

public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, Guid>
{
    private readonly DbContexto _context;

    public CreateClienteHandler(DbContexto context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            CPF = request.CPF,
            Nome = request.Nome,
            RG = request.RG,
            DataExpedicao = request.DataExpedicao,
            OrgaoExpedicao = request.OrgaoExpedicao,
            UF = request.UF,
            DataNascimento = request.DataNascimento,
            Sexo = request.Sexo,
            EstadoCivil = request.EstadoCivil,
            Endereco = new Endereco
            {
                Id = Guid.NewGuid(),
                CEP = request.CEP,
                Logradouro = request.Logradouro,
                Numero = request.Numero,
                Complemento = request.Complemento,
                Bairro = request.Bairro,
                Cidade = request.Cidade,
                UF = request.EnderecoUF
            }
        };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync(cancellationToken);

        return cliente.Id;
    }
}