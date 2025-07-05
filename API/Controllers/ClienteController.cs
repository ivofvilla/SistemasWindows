using API.Dados;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly DbContexto _contexto;

    public ClienteController(IMediator mediator, DbContexto contexto)
    {
        _mediator = mediator;
        _contexto = contexto;
    }

    [HttpPost]
    public async Task<IActionResult> Novo([FromBody] CreateClienteCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }

    [HttpPut("{cpf}")]
    public async Task<IActionResult> Atualizar(string cpf, [FromBody] UpdateClienteCommand command)
    {
        if (!cpf.Equals(command.CPF)) 
            return BadRequest("CPF inválido");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{cpf}")]
    public async Task<IActionResult> Apagar(string cpf)
    {
        await _mediator.Send(new DeleteClienteCommand(cpf));
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetTodos()
    {
        var clientes = _contexto.Clientes
            .Select(c => new
            {
                c.Id,
                c.Nome,
                c.CPF,
                c.Sexo,
                c.EstadoCivil,
                c.DataNascimento,
                Endereco = new
                {
                    c.Endereco.Logradouro,
                    c.Endereco.Cidade,
                    c.Endereco.UF
                }
            }).ToList();

        return Ok(clientes);
    }

    [HttpGet("{cpf}")]
    public IActionResult GetPorCPF(string cpf)
    {
        var cliente = _contexto.Clientes
            .Where(c => c.CPF == cpf)
            .Select(c => new
            {
                c.Id,
                c.Nome,
                c.CPF,
                c.Sexo,
                c.EstadoCivil,
                c.RG,
                c.DataNascimento,
                c.DataExpedicao,
                c.OrgaoExpedicao,
                c.UF,
                Endereco = new
                {
                    c.Endereco.Logradouro,
                    c.Endereco.Cidade,
                    c.Endereco.UF,
                    c.Endereco.CEP,
                    c.Endereco.Bairro,
                    c.Endereco.Numero,
                    c.Endereco.Complemento
                }
            })
            .FirstOrDefault();

        if (cliente == null) 
            return NotFound();

        return Ok(cliente);
    }
}