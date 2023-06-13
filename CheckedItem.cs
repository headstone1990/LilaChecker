namespace LilaChecker;

// Определяем класс CheckedItem, который хранит информацию об URL и последнем полученном значении
public class CheckedItem
{
    public required string Url { get; init; }
    public string? LastValue { get; set; }
}