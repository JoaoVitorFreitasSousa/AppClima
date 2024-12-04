using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WeatherAppMAUI.Models;

namespace WeatherAppMAUI.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Tempo>().Wait();
        }

        public Task<int> Insert(Tempo t)
        {
            return _conn.InsertAsync(t);
        }

        public Task<List<Tempo>> GetAll()
        {
            return _conn.Table<Tempo>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Tempo>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Tempo>> Search(string q)
        {
            string sql = "SELECT * FROM Tempo WHERE Title LIKE '%" + q + "%'";

            return _conn.QueryAsync<Tempo>(sql);
        }

        public Task<List<Tempo>> SearchEqual(string cidade, string data)
        {
            string sql = "SELECT * FROM Tempo WHERE DataPesquisa = '" + data + "' AND Title LIKE '%" + cidade + "%'";
            return _conn.QueryAsync<Tempo>(sql);
        }
    }
}
