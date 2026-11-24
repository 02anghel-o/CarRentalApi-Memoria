namespace CarRental.Api.Models;

public class Vehiculo
{
    public int Id { get; set; }
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public decimal PrecioPorDia { get; set; }
    public int AnioFabricacion { get; set; }
}