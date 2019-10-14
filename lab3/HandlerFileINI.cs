using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class HandlerFileINI {
    public string fileName =  "";
    public List<Section> Sections = new List<Section>();

    public HandlerFileINI(string pathFile) {
        if (Path.GetExtension(pathFile) != ".ini") {
            throw new Exception("Invalid type file");
        }
        else {
            try {
                using(StreamReader sr = new StreamReader(pathFile)) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        line = line.Trim();
                        if (line[0] == '[') {
                            line = line.Substring(1, line.Length - 2);
                            Sections.Add(new Section(line));
                        } else if (line != "" && line[0] != ';') {
                            string[] data = line.Split(new [] {"=", " ", ";"}, StringSplitOptions.RemoveEmptyEntries);
                            if (Sections.Count > 0 && data.Length >= 2) {
                                Sections[Sections.Count - 1].AddData(data[0], data[1]);
                            }
                        }
                    }
                }
            } catch {
                throw new Exception("Error read file");
            }
        }
    }

    private string GetValue(string sectionName, string key) {
        Section mySection = null;
        try { 
            mySection = Sections.Where(section => section.Name == sectionName).ToList()[0];
        } catch {
            throw new Exception("Not found section");
        }
        if (mySection != null) {
            return mySection.Find(key);
        }
        return "";
    }

    public Int32 GetValueINT(string sectionName, string key) {
        string res = GetValue(sectionName, key);
        try {
            return Convert.ToInt32(res);
        } catch {
            throw new Exception("Another type");
        }
    }

    public Double GetValueDouble(string sectionName, string key) {
        string res = GetValue(sectionName, key);
        try {
            return Convert.ToDouble(res);
        } catch {
            throw new Exception("Another type");
        }
    }

    public string GetValueString(string sectionName, string key) {
        string res = GetValue(sectionName, key);
        return res;
    }

    public void PrintAll() {
        foreach(var item in Sections) {
            item.PrintSection();
        }
    }
}