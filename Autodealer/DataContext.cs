using Autodealer.Model;

namespace Autodealer;

public static class DataContext
{
    public static List<Auto?> Autos =
    [
        new Auto { Id = 1, Brand = "Lada", Model = "Granta", Generation = "1", Engine = "1.6" },
        new Auto { Id = 2, Brand = "Lada", Model = "Vesta", Generation = "1", Engine = "1.6" },
        new Auto { Id = 2, Brand = "Lada", Model = "Niva", Generation = "1", Engine = "1.6" },
        new Auto { Id = 3, Brand = "Hyundai", Model = "Getz", Generation = "2", Engine = "1.4" },
        new Auto { Id = 4, Brand = "Hyundai", Model = "Solaris", Generation = "4", Engine = "1.4" },
        new Auto { Id = 5, Brand = "Kia", Model = "RioX", Generation = "4", Engine = "1.6" }
    ];

    public static int Id = Autos.Count - 1;
}