namespace API.Models;

public class Question
{
    public int Id { get; set; }
    public string Title { get; set; }
    public KindOfQuestion KindOfQuestion { get; set; }
    public string Ask { get; set; }
    public string Answer { get; set; }
}