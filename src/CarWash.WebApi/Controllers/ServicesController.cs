using CarWash.Application.Contracts.Persistence;
using CarWash.Application.Models.Services;
using CarWash.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ILogger<ServicesController> _logger;

    public ServicesController(
        IServiceRepository serviceRepository,
        ILogger<ServicesController> logger)
    {
        _serviceRepository = serviceRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ServiceDto>>> GetServices()
    {
        var services = await _serviceRepository.GetAllAsync();
        var dtos = services.Select(s => new ServiceDto(s.Id, s.Name, s.Description, s.Price)).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ServiceDto>> GetService(Guid id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);
        if (service == null) return NotFound();
        return Ok(new ServiceDto(service.Id, service.Name, service.Description, service.Price));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceDto>> CreateService(CreateServiceDto dto)
    {
        var service = new Service(dto.Name, dto.Description, dto.Price);
        await _serviceRepository.AddAsync(service);
        _logger.LogInformation("Создана услуга с ID: {ServiceId}", service.Id);
        return CreatedAtAction(nameof(GetService), new { id = service.Id }, 
            new ServiceDto(service.Id, service.Name, service.Description, service.Price));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteService(Guid id)
    {
        await _serviceRepository.DeleteAsync(id);
        _logger.LogInformation("Удалена услуга с ID: {ServiceId}", id);
        return NoContent();
    }
}