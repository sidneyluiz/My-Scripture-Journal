using System;
using System.Diagnostics;
using System.Linq;
using WebMatrix.Data;

namespace MyScriptureJournal.Classes
{
    class DatabaseHelper
    {
        public static object GetOne(string command, params object[] @params)
        {
            var db = OpenNewConnection();
            Log(command, @params);

            try
            {
                var data = db.QuerySingle(command, @params);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public static object Get(string command, params object[] @params)
        {
            var db = OpenNewConnection();
            Log(command, @params);

            try
            {
                var data = db.Query(command, @params);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public static bool Execute(string command, params object[] @params)
        {
            var db = OpenNewConnection();
            Log(command, @params);

            try
            {
                Debug.WriteLine($"---------------------------------------");
                Debug.WriteLine($"SQL Command: {command}");
                Debug.WriteLine($"Params: {@params}");
                Debug.WriteLine($"---------------------------------------");

                var result = db.Execute(command, @params);
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        static Database OpenNewConnection()
        {
            return Database.Open(nameof(MyScriptureJournal));
        }

        static void Log(string command, params object[] @params)
        {
            Debug.WriteLine($"---------------------------------------");
            Debug.WriteLine($"SQL Command: {command}");

            if (@params != null && @params.Any(wh => !string.IsNullOrEmpty(wh?.ToString())))
                Debug.WriteLine($"Params: {string.Join(";", @params)}");

            Debug.WriteLine($"---------------------------------------");
        }
    }
}