using System.Collections.Generic;

class Genre {
    public List<Genre> parentGenres;
    public List<Genre> childGenres;

    public string Name;

    public Genre(string name) {
        Name = name;
    }

    public Genre(string name, List<Genre> parents) {
        Name = name;
        parentGenres = parents;
    }

    public Genre(string name, List<Genre> parents, List<Genre> children) {
        Name = name;
        parentGenres = parents;
        childGenres = children;
    }

    public void AddParentGenre(Genre genre) {
        parentGenres.Add(genre);
    }

    public void AddParentGenre(List<Genre> genres) {
        parentGenres.AddRange(genres);
    }

    public void AddChildGenre(Genre genre) {
        childGenres.Add(genre);
    }

    public void AddChildGenre(List<Genre> genres) {
        childGenres.AddRange(genres);
    }
}