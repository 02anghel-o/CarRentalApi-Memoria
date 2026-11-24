using CarRental.Api.Dtos;
using CarRental.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiculosController : ControllerBase
{
    private readonly VehiculoMemoriaService _servicio;

    public VehiculosController(VehiculoMemoriaService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VehiculoTablaDto>> ObtenerTablaVehiculos()
    {
        var tabla = _servicio.ObtenerTodos()
            .Select(v => new VehiculoTablaDto(v.Id, v.Marca, v.Modelo, v.PrecioPorDia, v.AnioFabricacion));

        return Ok(tabla);
    }

    [HttpGet("{id:int}")]
    public ActionResult<VehiculoTablaDto> ObtenerPorId(int id)
    {
        var vehiculo = _servicio.BuscarPorId(id);
        if (vehiculo is null)
            return NotFound("Vehículo no encontrado.");

        return Ok(new VehiculoTablaDto(
            vehiculo.Id,
            vehiculo.Marca,
            vehiculo.Modelo,
            vehiculo.PrecioPorDia,
            vehiculo.AnioFabricacion));
    }

    [HttpPost]
    public IActionResult Crear([FromBody] CrearVehiculoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Marca) || string.IsNullOrWhiteSpace(dto.Modelo))
            return BadRequest("La marca y el modelo son obligatorios.");

        if (dto.AnioFabricacion < DateTime.UtcNow.Year - 5)
            return BadRequest("El vehículo no puede tener más de 5 años de antigüedad.");

        var creado = _servicio.Agregar(dto.Marca, dto.Modelo, dto.PrecioPorDia, dto.AnioFabricacion);
        var respuesta = new VehiculoTablaDto(
            creado.Id,
            creado.Marca,
            creado.Modelo,
            creado.PrecioPorDia,
            creado.AnioFabricacion);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, respuesta);
    }
}