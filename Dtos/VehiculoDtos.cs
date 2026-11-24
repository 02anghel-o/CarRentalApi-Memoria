namespace CarRental.Api.Dtos;

public record CrearVehiculoDto(string Marca, string Modelo, decimal PrecioPorDia, int AnioFabricacion);
public record VehiculoTablaDto(int Id, string Marca, string Modelo, decimal PrecioPorDia, int AnioFabricacion);