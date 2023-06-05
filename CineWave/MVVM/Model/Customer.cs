namespace CineWave.MVVM.Model;

public abstract record Customer(int Id, string? Name)
{
    public int Id { get; set; } = Id;
    public string? Name { get; set; } = Name;
}
