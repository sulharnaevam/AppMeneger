using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AppMeneger
{
    public static class DataStorage
    {
        private static string dataFile = "notes.txt";  

        public static List<Note> Load()
        {
            List<Note> notes = new List<Note>();

            try
            {
                if (File.Exists(dataFile))
                {
                    string[] lines = File.ReadAllLines(dataFile);
                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            Note note = Note.FromFileString(line);
                            if (note != null)
                            {
                                notes.Add(note);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка загрузки: {ex.Message}");
            }

            return notes;
        }

        public static void Save(List<Note> notes)
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (Note note in notes)
                {
                    lines.Add(note.ToFileString());
                }
                File.WriteAllLines(dataFile, lines);
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка сохранения: {ex.Message}");
            }
        }
    }
}
