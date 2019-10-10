using System;
using System.Collections.Generic;

class Collection {
    public string Name;
    
    public HashSet<Artist> Artists = new HashSet<Artist>();
    public HashSet<Song> Songs = new HashSet<Song>();
    public HashSet<Genre> Genres = new HashSet<Genre>();

    public Collection(string name) {
        Name = name;
    }

    public Collection(string name, Song song) {
        Name = name;
        Songs.Add(song);
        foreach(var item in song.Artists) {
            Artists.Add(item);
        }
        Genres.Add(song.Genre);
    }

    public void AddSong(Song song) {
        Songs.Add(song);
        foreach(var item in song.Artists) {
            Artists.Add(item);
        }
        Genres.Add(song.Genre);
    }
}