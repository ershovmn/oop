using System;
using System.Collections.Generic;

class Section {
    public string Name = "";
    private Dictionary<string, string> Data = new Dictionary<string, string>();
    
    public Section(string name) {
        Name = name;
    }

    public Section(string name, Dictionary<string, string> data) {
        Name = name;
        Data = data;
    }

    public void AddData(Dictionary<string, string> data) {
        foreach(var item in data) {
            Data.Add(item.Key, item.Value);
        }
    }

    public void AddData(string key, string value) {
        Data.Add(key, value);
    }

    public string Find(string key) {
        try {
            return Data[key];
        } catch {
            throw new Exception($"Not found key '{key}' in section '{Name}'");
        }
    }

    public void PrintSection() {
        Console.WriteLine();
        Console.WriteLine("--------------------");
        Console.WriteLine($"Print section: {Name}");
        Console.WriteLine("Data:");
        foreach(var item in Data) {
            Console.WriteLine($"\t{item.Key} : {item.Value}");
        }
        Console.WriteLine("------------------");
        Console.WriteLine();
    }
}