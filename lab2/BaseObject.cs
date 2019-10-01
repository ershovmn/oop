using System.Collections.Generic;

class BaseObject {
    public string Name;
    public List<Artist> Artists;
    public List<Genre> Genres;
    public List<Song> Songs;

    public BaseObject() {

    }

    public BaseObject(string name) {
        Name = name;
    }

    public BaseObject(string name, Song song) {
        Name = name;
        Songs.Add(song);
        this.LoadInfo(song);
    }

    public BaseObject(string name, List<Song> songs) {
        Name = name;
        Songs.AddRange(songs);
        this.LoadInfo(songs);
    }

    public void Add(Song song) {
        Songs.Add(song);
        this.LoadInfo(song);
    }

    public void Add(List<Song> songs) {
        Songs.AddRange(songs);
        this.LoadInfo(songs);
    }

    private void LoadInfo(List<Song> songs) {
        foreach(Song song in songs) {
            foreach(Artist artist in song.Artists) {
                if(!Artists.Contains(artist)) {
                    Artists.Add(artist);
                }
            }
            if(!Genres.Contains(song.Genre)) {
                Genres.Add(song.Genre);
            }
        }
    }

    private void LoadInfo(Song song) {
        this.LoadInfo(new List<Song> {song});
    }
}
