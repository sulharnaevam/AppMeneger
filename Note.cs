using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMeneger
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Data { get; set; }
        public string Prioritet { get; set; }
        public bool IsCompleted { get; set; }

        public Note()
        {
            Name = "";
            Content = "";
            Prioritet = "Средний";
            Data = DateTime.Now.AddDays(1);
            IsCompleted = false;
        }

        public string ToFileString()
        {
            return $"{Id}|{Name}|{Content}|{Data:yyyy-MM-dd HH:mm:ss}|{Prioritet}|{IsCompleted}";
        }

        public static Note FromFileString(string line)
        {
            string[] parts = line.Split('|');
            if (parts.Length != 6) return null;

            Note note = new Note();
            note.Id = int.Parse(parts[0]);
            note.Name = parts[1];
            note.Content = parts[2];
            note.Data = DateTime.Parse(parts[3]);
            note.Prioritet = parts[4];
            note.IsCompleted = bool.Parse(parts[5]);
            return note;
        }
    }
}
