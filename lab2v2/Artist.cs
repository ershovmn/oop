using System.Collections.Generic;

class Artist {
    public string Name;
    public string _id;

    public HashSet<Song> Songs = new HashSet<Song>();
    //public HashSet<Genre> Genres = new HashSet<Genre>();

    public Artist(string name) {
        Name = name;
        _id = Name + "isartist";
    }

    public void AddSong(Song song) {
        Songs.Add(song);
    }
}