using Autodealer.Entities;

namespace Autodealer.Dto;

public class CarMutationDto
{
    /// <summary>
    /// Марка.
    /// </summary>
    public string Brand { get; set; }
    /// <summary>
    /// Модель.
    /// </summary>
    public string Model { get; set; }
    /// <summary>
    /// Поколение.
    /// </summary>
    public string Generation { get; set; }
    /// <summary>
    /// Двигатель.
    /// </summary>
    public Engine Engine { get; set; }
}