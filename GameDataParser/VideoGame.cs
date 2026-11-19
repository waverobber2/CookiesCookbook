public class VideoGame
{
    public string Title { get; init; }
    public int ReleaseYear { get; init; }
    public decimal Rating { get; init; }
    public override string ToString() => $"{Title} was released {ReleaseYear} and is rated {Rating}";
}

/*
games.json
gamesInvalid.json
*/
