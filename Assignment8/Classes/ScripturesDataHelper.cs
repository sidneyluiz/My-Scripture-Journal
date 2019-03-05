using System;

namespace MyScriptureJournal.Classes
{
    public class ScripturesDataHelper
    {
        const string Scriptures = "Scriptures";

        public static dynamic GetByID(string id)
        {
            var command = $@"SELECT * FROM {Scriptures}
                             WHERE ID = @0";

            var data = DatabaseHelper.GetOne(command, id);
            return data;
        }

        public static bool Delete(string id)
        {
            var command = $@"DELETE FROM {Scriptures} WHERE Id=@0";

            return DatabaseHelper.Execute(command, id);
        }

        public static bool Update(string id, string book = null, int chapter = 0, int verse = 0, string note = null)
        {
            var command = $@"UPDATE {Scriptures} 
                                SET Book=@1, Chapter=@2, Verse=@3, Note=@4
                                WHERE Id=@0";

            return DatabaseHelper.Execute(command, id, book, chapter, verse, note);
        }

        public static dynamic Search(string searchBook = null, string searchKeyword = null)
        {
            var command = "SELECT * FROM Scriptures WHERE Id <> 0 ";

            if (!string.IsNullOrEmpty(searchBook))
            {
                command += " AND Book LIKE @0 ";
                searchBook = $"%{searchBook}%";
            }

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                command += @" AND Note LIKE @1 ";
                searchKeyword = $"%{searchKeyword}%";
            }

            return DatabaseHelper.Get(command, searchBook, searchKeyword);
        }

        public static bool Insert(string book, int chapter, int verse, string note)
        {
            var insertCommand = $@"INSERT INTO {Scriptures}
                                    (Book, Chapter, Verse, Note, Date)
                                VALUES(@0, @1, @2, @3, @4)";

            return DatabaseHelper.Execute(insertCommand, book, chapter, verse, note, DateTime.Now);
        }
    }
}