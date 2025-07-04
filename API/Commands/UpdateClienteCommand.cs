using MediatR;

public record UpdateClienteCommand(
    Guid Id,
    string CPF,
    string Nome,
    string RG,
    DateTime DataExpedicao,
    string OrgaoExpedicao,
    string UF,
    DateTime DataNascimento,
    string Sexo,
    string EstadoCivil,
    string CEP,
    string Logradouro,
    string Numero,
    string Complemento,
    string Bairro,
    string Cidade,
    string EnderecoUF
) : IRequest<Unit>;