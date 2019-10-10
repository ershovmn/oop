using System.Collections.Generic;

class Genre {
    public HashSet<Genre> parentGenres = new HashSet<Genre>();
    public HashSet<Genre> childGenres = new HashSet<Genre>();

    public string _id;

    public string Name;

    public Genre(string name) {
        Name = name;
        _id = Name + "isgenre";
    }

    public Genre(string name, List<Genre> parents) {
        Name = name;
        foreach(var item in parents) {
            parentGenres.Add(item);
        }
        //parentGenres = parents;
        _id = Name + "isgenre";
    }

    public Genre(string name, List<Genre> parents, List<Genre> children) {
        Name = name;
        foreach(var item in parents) {
            parentGenres.Add(item);
        }
        foreach(var item in children) {
            childGenres.Add(item);
        }
        //parentGenres = parents;
        //childGenres = children;
        _id = Name + "isgenre";
    }

    public void AddParentGenre(Genre genre) {
        parentGenres.Add(genre);
    }

    public void AddParentGenre(List<Genre> genres) {
        foreach(var item in genres) {
            parentGenres.Add(item);
        }
    }

    public void AddChildGenre(Genre genre) {
        childGenres.Add(genre);
    }

    public void AddChildGenre(List<Genre> genres) {
        foreach(var item in genres) {
            childGenres.Add(item);
        }
    }

    public List<Genre> GetChildGenres() {
        List<Genre> AllChildGenres = new List<Genre>() { this };
        foreach(var item in childGenres) {
            AllChildGenres.AddRange(item.GetChildGenres());
        }
        return(AllChildGenres);
    }
}