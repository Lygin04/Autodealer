namespace Autodealer.Dto;

public class EngineDto
{
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    /// <summary>
    /// Объем в литрах.
    /// </summary>
    public double Capacity { get; set; }
    
    /// <summary>
    /// Количество цилиндров.
    /// </summary>
    public int CountBlock { get; set; }
}