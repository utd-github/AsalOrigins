using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AsalOrigins.Models;
using SQLite;
namespace AsalOrigins.Data
{
    class PostDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public PostDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Post>().Wait();
        }

        public Task<int> SavePostAsync(Post post)
        {
            if (post.ID.Equals(""))
            {
                return _database.UpdateAsync(post);
            }
            else
            {
                return _database.InsertAsync(post);
            }
        }

        public Task<List<Post>> GetPostsAsync()
        {
            return _database.Table<Post>().ToListAsync();
        }

        public Task<Post> GetPostAsync(string id)
        {
            return _database.Table<Post>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> DeleteNoteAsync(Post note)
        {
            return _database.DeleteAsync(note);
        }
    }
}
