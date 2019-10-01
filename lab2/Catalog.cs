using System.Collections.Generic;

class Catalog {
    public List<BaseObject> Albums;
    public List<BaseObject> Collectors;
    public BaseObject anotherSongs;

    public void NewAlbum(string name) {
        Albums.Add(new BaseObject(name));
    }

    public void NewAlbum(string name, Song song) {
        Albums.Add(new BaseObject(name, song));
    }

    public void NewAlbum(string name, List<Song> songs) {
        Albums.Add(new BaseObject(name, songs));
    }

    public void NewCollector(string name) {
        Collectors.Add(new BaseObject(name));
    }

    public void NewCollector(string name, Song song) {
        Collectors.Add(new BaseObject(name, song));
    }

    public void NewCollector(string name, List<Song> songs) {
        Collectors.Add(new BaseObject(name, songs));
    }

    public void AddSong(Song song) {
        anotherSongs.Add(song);
    }

    public void AddSong(List<Song> songs) {
        anotherSongs.Add(songs);
    }

    public List<Song> FindSongs(string name) {
        List<Song> result = new List<Song>();
        foreach(var album in Albums) {
            foreach(var song in album.Songs) {
                if(song.Name == name) {
                    result.Add(song);
                }
            }
        foreach(var collector in Collectors) {
            foreach(var song in collector.Songs) {
                if(song.Name == name) {
                    result.Add(song);
                }
            }
        
        foreach(var song in anotherSongs.Songs) {
            if(song.Name == name) {
                result.Add(song);
            }
        }
        result
    }
}