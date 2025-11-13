using CarWash.Application.Contracts.Persistence;
using CarWash.Application.Models.Clients;
using CarWash.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(
        IClientRepository clientRepository,
        ILogger<ClientsController> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetClients()
    {
        var clients = await _clientRepository.GetAllAsync();
        var dtos = clients.Select(c => new ClientDto(c.Id, c.FullName, c.PhoneNumber)).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDto>> GetClient(Guid id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null) return NotFound();
        return Ok(new ClientDto(client.Id, client.FullName, client.PhoneNumber));
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> CreateClient(CreateClientDto dto)
    {
        var client = new Client(dto.FullName, dto.PhoneNumber);
        await _clientRepository.AddAsync(client);
        _logger.LogInformation("Создан клиент с ID: {ClientId}", client.Id);
        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, 
            new ClientDto(client.Id, client.FullName, client.PhoneNumber));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        await _clientRepository.DeleteAsync(id);
        _logger.LogInformation("Удалён клиент с ID: {ClientId}", id);
        return NoContent();
    }
}