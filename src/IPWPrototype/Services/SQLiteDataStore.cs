using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IPWPrototype.Models;
using SQLite;

namespace IPWPrototype.Services
{
    public class SQLiteDataStore : IDataStore<Item> 
    {
        private readonly SQLiteAsyncConnection _database;

        public SQLiteDataStore()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ItemSQLite.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Item>().Wait();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            int result = await _database.InsertAsync(item);
            return result == 1;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            int result = await _database.DeleteAsync<Item>(id);
            return result == 1;
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await _database.Table<Item>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await _database.Table<Item>().ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            int result = await _database.UpdateAsync(item);
            return result == 1;
        }
    }
}
