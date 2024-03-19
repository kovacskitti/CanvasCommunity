namespace CanvasCommunity;

public class Artist
{
    public string Id { set; get; }
    public string Name { set; get; }
    public List<Painting> Paintings { set; get; }
    public string Description { set; get; }
    public string Gender { set; get; }
    public string Biography { set; get; }
    public int Birthday { set; get; }
    public int Deathday { set; get; }
    public string Hometown { set; get; }
    public string Location { set; get; }
    public string Nationality { set; get; }
  }