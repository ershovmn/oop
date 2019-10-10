using System;
using System.Linq;
using System.Collections.Generic;

class Storage {
    private HashSet<Song> AllSongs = new HashSet<Song>();
    private HashSet<Artist> AllArtists = new HashSet<Artist>();
    private HashSet<Album> AllAlbums = new HashSet<Album>();
    private HashSet<Collection> AllCollections = new HashSet<Collection>();
    private HashSet<Genre> AllGenres = new HashSet<Genre>();

    public void AddSong(Song song) {
        AllSongs.Add(song);
        foreach(var item in song.Artists) {
            AllArtists.Add(item);
        }
        AllGenres.Add(song.Genre);
    } 

    public void AddArtist(Artist artist) {
        AllArtists.Add(artist);
        foreach(var song in artist.Songs) {
            this.AddSong(song);
        }
    }

    public void AddAlbum(Album album) {
        AllAlbums.Add(album);
        foreach(var song in album.Songs) {
            this.AddSong(song);
        }
    }

    public void AddCollection(Collection collection) {
        AllCollections.Add(collection);
        foreach(var song in collection.Songs) {
            this.AddSong(song);
        }
    }

    private List<Song> FindSongs(HashSet<Song> songs, string name = "", List<Genre> genres = null, List<Artist> artists = null) {
        List<Genre> allGenres = new List<Genre>();
        
        if(genres != null) {
            foreach(var item in genres) {
                allGenres.AddRange(item.GetChildGenres());
            }

            genres = allGenres;
        }

        var shouldLookupGenres = genres != null && genres.Count > 0;
        var shouldLookupArtists = artists != null && artists.Count > 0;

        return songs
            .Where(song => song.Name.IndexOf(name) != -1)
            .Where(song => shouldLookupGenres ? genres.Any(genre => genre == song.Genre) : true)
            .Where(song => shouldLookupArtists ? song.Artists.Any(artist => artists.Contains(artist)) : true)
            .ToList();
    }

    private List<Artist> ArtistName(IEnumerable<string> artistsNames) {
        List<Artist> artists = new List<Artist>();

        if(artistsNames != null) {
            foreach(var artist in AllArtists) {
                foreach(var artistName in artistsNames) {
                    if(artist.Name.ToLower().IndexOf(artistName.ToLower()) != -1) {
                        artists.Add(artist);
                    }
                }
            }
        } else {
            artists = null;
        }

        return artists;
    }

    private List<Genre> GenreName(IEnumerable<string> genresNames) {
        List<Genre> genres = new List<Genre>();
    
        if(genresNames != null) {
            foreach(var item in AllGenres) {
                foreach(var genreName in genresNames) {
                    if(item.Name.ToLower().IndexOf(genreName.ToLower()) != -1) {
                        genres.Add(item);
                    }
                }
            }
        } else {
            genres = null;
        }

        return genres;
    }

    private HashSet<Song> AlbumName(IEnumerable<string> albumsNames) {
        List<Album> albums = new List<Album>();

        if(albumsNames != null){
            foreach(var item in AllAlbums) {
                foreach(var albumName in albumsNames) {
                    if(item.Name.ToLower().IndexOf(albumName.ToLower()) != -1) {
                        albums.Add(item);
                    }
                }
            }
        }

        HashSet<Song> songs = new HashSet<Song>();
        foreach(var album in albums) {
            foreach(var song in album.Songs) {
                songs.Add(song);
            }
        }

        return songs;
    }

    public List<Song> FindSong(string name = "", IEnumerable<string> genresNames = null, IEnumerable<string> artistsNames = null) {
        return(this.FindSongs(AllSongs, name, GenreName(genresNames), ArtistName(artistsNames)));
    }

    public List<Song> FindSongInAlbum(string albumName, string name = "", List<string> genresNames = null, List<string> artistsNames = null) {

        return(this.FindSongs(AlbumName(new List<string>{albumName}), name, GenreName(genresNames), ArtistName(artistsNames)));
    }

    public List<Song> FindSongInAlbum(List<string> albumsNames, string name = "", List<string> genresNames = null, List<string> artistsNames = null) {
        // HashSet<Song> songs = new HashSet<Song>();
        // foreach(var album in albums) {
        //     foreach(var song in album.Songs) {
        //         songs.Add(song);
        //     }
        // }
        return(this.FindSongs(AlbumName(albumsNames), name, GenreName(genresNames), ArtistName(artistsNames)));
    }

    // public List<Song> FindSongInCollection(Collection collection, string name = "", List<Genre> genres = null, List<Artist> artists = null) {
    //     return(this.FindSongs(collection.Songs, name, GenreName(genresNames), ArtistName(artistsNames)));
    // }

    // public List<Song> FindSongInCollection(List<Collection> collections, string name = "", List<Genre> genres = null, List<Artist> artists = null) {
    //     HashSet<Song> songs = new HashSet<Song>();
    //     foreach(var collection in collections) {
    //         foreach(var song in collection.Songs) {
    //             songs.Add(song);
    //         }
    //     }
    //     return(this.FindSongs(songs, name, GenreName(genresNames), ArtistName(artistsNames)));
    // }
    
    // public List<Artist> FindArtists(string name = "", List<Song> songs = null) {
    //     List<Artist> artists = new List<Artist>();
        
    //     foreach(var artist in AllArtists) {
    //         var flag = true;
    //         if(artist.Name.IndexOf(name) == -1) {
    //             flag = false;
    //         }
    //         if(songs != null) {
    //             var flag_1 = false;
    //             foreach(var mysong in songs) {
    //                 foreach(var song in artist.Songs) {
    //                     if(song == mysong) {
    //                         flag_1 = true;
    //                     }
    //                 }
    //             }
    //             flag = flag && flag_1;
                
    //         }
    //         if(flag)
    //     }
    // }

    public void PrintAll() {
        Console.WriteLine("Songs:");
        foreach(var item in AllSongs) {
            Console.WriteLine($"\t{item.Name}");
        }
        Console.WriteLine("Artists:");
        foreach(var item in AllArtists) {
            Console.WriteLine($"\t{item.Name}");
        }
        Console.WriteLine("Genres:");
        foreach(var item in AllGenres) {
            Console.WriteLine($"\t{item.Name}");
        }
        Console.WriteLine("Albums:");
        foreach(var item in AllAlbums) {
            Console.WriteLine($"\t{item.Name}");
        }
        Console.WriteLine("Collection:");
        foreach(var item in AllCollections) {
            Console.WriteLine($"\t{item.Name}");
        }
        Console.WriteLine("End print");
    }
}