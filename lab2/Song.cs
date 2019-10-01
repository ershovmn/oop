using System.Collections.Generic;
class Song {
    public string Name;
    public Genre Genre;
    public List<Artist> Artists;

    public Song(string name, Artist artist, Genre genre) {
        Name = name;
        Artists.Add(artist);
        Genre = genre;
    }

    public Song(string name, List<Artist> artists, Genre genre) {
        Name = name;
        Artists.AddRange(artists);
        Genre = genre;
    }
}