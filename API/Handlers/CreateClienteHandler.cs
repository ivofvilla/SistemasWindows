using API.Dados;
using API.Modelos;
using MediatR;

public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, Guid>
{
    private readonly DbContexto _contexto;

    public CreateClienteHandler(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Guid> Handle(CreateClienteCommand requisicao, CancellationToken cancellationToken)
    {
        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            CPF = requisicao.CPF,
            Nome = requisicao.Nome,
            RG = requisicao.RG,
            DataExpedicao = requisicao.DataExpedicao,
            OrgaoExpedicao = requisicao.OrgaoExpedicao,
            UF = requisicao.UF,
            DataNascimento = requisicao.DataNascimento,
            Sexo = requisicao.Sexo,
            EstadoCivil = requisicao.EstadoCivil,
            Endereco = new Endereco
            {
                Id = Guid.NewGuid(),
                CEP = requisicao.CEP,
                Logradouro = requisicao.Logradouro,
                Numero = requisicao.Numero,
                Complemento = requisicao.Complemento,
                Bairro = requisicao.Bairro,
                Cidade = requisicao.Cidade,
                UF = requisicao.EnderecoUF
            }
        };

        _contexto.Clientes.Add(cliente);
        await _contexto.SaveChangesAsync(cancellationToken);

        return cliente.Id;
    }
}