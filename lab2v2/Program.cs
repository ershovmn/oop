using System;
using System.Collections.Generic;

namespace lab2v2 {
    class Program {
        static void Main(string[] args) {
            Artist artist = new Artist("tehnarenok");
            Genre first_genre = new Genre("tehnarenokGenre");
            Genre second_genre = new Genre("tehnarenokGenre2");
            Song first_song = new Song("wrhwqr", artist, first_genre);
            Song second_song = new Song("wrhgfrwyq", artist, second_genre);
            Storage storage = new Storage();
            storage.AddArtist(artist);

            storage.PrintAll();

            var songs = storage.FindSong(name: "q", artistsNames: new[] {"tehnarenok"}, genresNames: new[] {"tehnarenokGenre2"});

            Console.WriteLine("Print find songs:");
            foreach(var song in songs) {
                Console.WriteLine($"{song.Name}");
            }
        }
    }
}
