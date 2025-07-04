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
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPorId), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdateClienteCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Apagar(Guid id)
    {
        await _mediator.Send(new DeleteClienteCommand(id));
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
                Endereco = new
                {
                    c.Endereco.Logradouro,
                    c.Endereco.Cidade,
                    c.Endereco.UF
                }
            }).ToList();

        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public IActionResult GetPorId(Guid id)
    {
        var cliente = _contexto.Clientes
            .Where(c => c.Id == id)
            .Select(c => new
            {
                c.Id,
                c.Nome,
                c.CPF,
                c.Sexo,
                c.EstadoCivil,
                Endereco = new
                {
                    c.Endereco.Logradouro,
                    c.Endereco.Cidade,
                    c.Endereco.UF
                }
            })
            .FirstOrDefault();

        if (cliente == null) 
            return NotFound();

        return Ok(cliente);
    }
}