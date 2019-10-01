using System.Collections.Generic;

class ListSongs {
    public List<Song> Songs;
    public List<Genre> Genres;
    public List<Artist> Artists;
    public void AddSong(Song song) {
        Songs.Add(song);
    }

    public void AddGenre(Genre genre) {
        Genres.Add(genre);
    }

    public void AddArtist(Artist artist) {
        Artists.Add(artist);
    }

    public List<Song> FindArtist()
}