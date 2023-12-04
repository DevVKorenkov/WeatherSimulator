
namespace WheatherSimulator.Models.DTOs;

public class CityDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public override bool Equals(object? obj) =>
        obj is not null && 
        obj is CityDTO dTO && 
        Id == dTO.Id &&
        obj.GetHashCode() == GetHashCode();

    public override int GetHashCode() => (Id * Id).GetHashCode();
}