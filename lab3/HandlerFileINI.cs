using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

class HandlerFileINI {
    public string fileName =  "";
    public List<Section> Sections = new List<Section>();

    public HandlerFileINI(string pathFile) {
        if (Path.GetExtension(pathFile) != ".ini") {
            throw new InvalidTypeFile();
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
                throw new ErrorReadFile();
            }
        }
    }

    private string GetValue(string sectionName, string key) {
        Section mySection = null;
        try { 
            mySection = Sections.Where(section => section.Name == sectionName).ToList()[0];
        } catch {
            throw new SectionNotFound();
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
            throw new AnotherType();
        }
    }

    public Double GetValueDouble(string sectionName, string key) {
        string res = GetValue(sectionName, key);
        try {
            return Convert.ToDouble(res, CultureInfo.InvariantCulture);
        } catch {
            throw new AnotherType();
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