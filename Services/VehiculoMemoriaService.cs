using CarRental.Api.Models;

namespace CarRental.Api.Services;

public class VehiculoMemoriaService
{
    private readonly List<Vehiculo> _vehiculos = new()
    {
        new Vehiculo { Id = 1, Marca = "Toyota", Modelo = "Corolla", PrecioPorDia = 45m, AnioFabricacion = 2024 },
        new Vehiculo { Id = 2, Marca = "Hyundai", Modelo = "Tucson", PrecioPorDia = 60m, AnioFabricacion = 2025 }
    };

    private int _siguienteId = 3;

    public IEnumerable<Vehiculo> ObtenerTodos() => _vehiculos;

    public Vehiculo? BuscarPorId(int id) =>
        _vehiculos.FirstOrDefault(v => v.Id == id);

    public Vehiculo Agregar(string marca, string modelo, decimal precioPorDia, int anioFabricacion)
    {
        var nuevo = new Vehiculo
        {
            Id = _siguienteId++,
            Marca = marca,
            Modelo = modelo,
            PrecioPorDia = precioPorDia,
            AnioFabricacion = anioFabricacion
        };

        _vehiculos.Add(nuevo);
        return nuevo;
    }
}