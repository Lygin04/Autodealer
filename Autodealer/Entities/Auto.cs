namespace Autodealer.Model;

public class Auto
{
    /// <summary>
    /// Индификатор.
    /// </summary>
    public int Id { get; set; }
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
    public string Engine { get; set; }
}