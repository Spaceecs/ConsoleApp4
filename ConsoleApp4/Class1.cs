using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Verse
    {
        public string Name { get; set; }
        public string Autor { get; set; }
        public DateTime CreatDate { get; set; }
        public string Text { get; set; }
        public string Info { get; set; }
        public Verse() { }
        public Verse(string name, string autor, DateTime creatDate, string text, string info)
        {
            Name = name;
            Autor = autor;
            CreatDate = creatDate;
            Text = text;
            Info = info;
        }
        public override string ToString()
        {
            return $"Verse Name: {Name}\nAutor Name: {Autor}\nCreat Date: {CreatDate.ToShortDateString()}\n{Text}\n\nTeam: {Info}";
        }
    }
    class Verses
    {
        public List<Verse> verses { get; set; }
        public Verses() { }
        public Verses(List<Verse> verses)
        {
            this.verses = verses;
        }
        public void Add(Verse verse)
        {
            verses.Add(verse);
        }
        public void Remove(Verse verse)
        {
            verses.Remove(verse);
        }
        public Verses GetVerse(string info)
        {
            Verses versesres = new Verses();
            foreach(Verse verse in verses)
            {
                if (verse.Info.Equals(info))
                {
                    versesres.Add(verse);
                }
            }
            return versesres;
        }
        public void Redact(Verse verse)
        {
            for(int i=0; i<verses.Count; i++)
            {
                if (verses[i].Name == verse.Name)
                {
                    verses[i]= verse;
                }
            }
        }
        public void SaveToFile(string FileName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, verses);
                }
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        public static Verses ReadFromFile(string FileName)
        {
            Verses verses = null;
            try
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    verses = (Verses)binaryFormatter.Deserialize(fileStream);
                }
                Console.WriteLine("Data loaded successfully.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            return verses;
        }
    }
}
