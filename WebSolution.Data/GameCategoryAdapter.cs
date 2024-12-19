using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using WebSolution.Models;

namespace WebSolution.Data
{
    public class GameCategoryAdapter
    {
        private const String collectionName = "GameCategory";
        private MongoDatabase _db;
        private readonly MongoCollection<GameCategory> _categories;
        private MongoClient _client;
        private MongoServer _server;
        private SystemAdapter _systemAdapter;

        public GameCategoryAdapter(string dbName)
        {
            _systemAdapter = new SystemAdapter(dbName);
            if (_client == null)
                _client = new MongoClient(CoreDataConfig.MongoConnectionString);
            if (_server == null)
                _server = _client.GetServer();
            if (_db == null)
            {
                _db = _server.GetDatabase(dbName);
            }
            if (_categories == null)
            {
                _categories = _db.GetCollection<GameCategory>(collectionName);
                _categories.CreateIndex(IndexKeys.Descending("category_name"), IndexOptions.SetName("category_name"));
            }
        }
        public ObjectId SaveGameCategory(GameCategory categories)
        {
           
            try
            {
                if (string.IsNullOrEmpty(categories.Id))
                {
                    categories.Id = ObjectId.GenerateNewId().ToString();
                }
               
                _categories.Save(categories);
                return ObjectId.Parse(categories.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game categories: {ex.Message}");
                return ObjectId.Empty;
            }
        }

       
        public List<GameCategory> GetGameCategories()
        {
            return _categories.FindAll().ToList();
        }

        public bool Remove(long id)
        {
            try
            {
                MongoCollection<GameModel> _games = _db.GetCollection<GameModel>(collectionName);
                return _games.Remove(Query.EQ("_id", id), WriteConcern.Acknowledged).Ok;
            }
            catch
            {
                return false;
            }
        }
    }
}
