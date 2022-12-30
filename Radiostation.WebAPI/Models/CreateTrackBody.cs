namespace Radiostation.WebAPI.Models;

public class CreateTrackBody
{
    public string Name { get; set; }

    public int Rating { get; set; }

    public int PerformerId { get; set; }

    public int GenreId { get; set; }

    public string Duration { get; set; }
}