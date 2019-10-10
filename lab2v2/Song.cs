using System;
using System.Collections.Generic;

class Song {
    public string Name;
    public Genre Genre;
    public HashSet<Artist> Artists = new HashSet<Artist>();
    public string _id;

    public Song(string name, Artist artist, Genre genre) {
        Name = name;
        Genre = genre;
        Artists.Add(artist);
        _id = Name + "issong";
        artist.AddSong(this);
    }

    public Song(string name, List<Artist> artists, Genre genre) {
        Name = name;
        Genre = genre;
        foreach(var item in artists) {
            Artists.Add(item);
        }
        _id = Name + "issong";
        foreach (var item in artists) {
            item.AddSong(this);
        }
    }
}