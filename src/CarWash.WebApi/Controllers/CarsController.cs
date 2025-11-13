using CarWash.Application.Contracts.Persistence;
using CarWash.Application.Models.Cars;
using CarWash.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<CarsController> _logger;

    public CarsController(
        ICarRepository carRepository,
        ILogger<CarsController> logger)
    {
        _carRepository = carRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<CarDto>>> GetCars()
    {
        var cars = await _carRepository.GetAllAsync();
        var dtos = cars.Select(c => new CarDto(c.Id, c.Brand, c.Model, c.LicensePlate, c.ClientId)).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CarDto>> GetCar(Guid id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null) return NotFound();
        return Ok(new CarDto(car.Id, car.Brand, car.Model, car.LicensePlate, car.ClientId));
    }

    [HttpPost]
    public async Task<ActionResult<CarDto>> CreateCar(CreateCarDto dto)
    {
        var car = new Car(dto.Brand, dto.Model, dto.LicensePlate, dto.ClientId);
        await _carRepository.AddAsync(car);
        _logger.LogInformation("Создан автомобиль с ID: {CarId}", car.Id);
        return CreatedAtAction(nameof(GetCar), new { id = car.Id }, 
            new CarDto(car.Id, car.Brand, car.Model, car.LicensePlate, car.ClientId));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        await _carRepository.DeleteAsync(id);
        _logger.LogInformation("Удалён автомобиль с ID: {CarId}", id);
        return NoContent();
    }
}