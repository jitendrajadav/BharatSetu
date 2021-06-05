using BharatSetu.Models;
using BharatSetu.Services;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BharatSetu.Data
{
    internal class BharatSetuDB
    {
        private static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<BharatSetuDB> Instance = new AsyncLazy<BharatSetuDB>(async () =>
        {
            var instance = new BharatSetuDB();
            CreateTableResult result = await Database.CreateTableAsync<Session>();
            return instance;
        });

        public BharatSetuDB()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<Session>> GetItemsAsync()
        {
            return Database.Table<Session>().ToListAsync();
        }

        public Task<List<Session>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<Session>("SELECT * FROM [Session] WHERE [Done] = 0");
        }

        public Task<Session> GetItemAsync(int id)
        {
            return Database.Table<Session>().Where(i => i.Center_id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Session item)
        {
            if (item.Center_id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> SaveAllAsync(IList<Session> item)
        {
            return Database.InsertAllAsync(item);
        }
        public Task<int> DeleteItemAsync(Session item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<int> DeleteItemsAsync()
        {
            return Database.DeleteAllAsync<Session>();
        }

    }
}
