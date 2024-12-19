using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using WebSolution.Models;
using MongoDB.Bson;
using System.Collections;
using System.Collections.ObjectModel;

namespace WebSolution.Data
{
    public class GameAdapter
    {
        private const String collectionName = "Games";
        private MongoDatabase _db;
        private readonly MongoCollection<GameModel> _games;
        private MongoClient _client;
        private MongoServer _server;
        private SystemAdapter _systemAdapter;
        
        public GameAdapter(string dbName)
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
            if (_games == null)
            {
                _games = _db.GetCollection<GameModel>(collectionName);
                _games.CreateIndex(IndexKeys.Descending("game_avatar"), IndexOptions.SetName("game_avatar"));
                _games.CreateIndex(IndexKeys.Descending("game_category"), IndexOptions.SetName("game_category"));
                _games.CreateIndex(IndexKeys.Descending("game_name"), IndexOptions.SetName("game_name"));
                _games.CreateIndex(IndexKeys.Descending("game_url"), IndexOptions.SetName("game_url"));
            }
        }
        public ObjectId SaveGame(GameModel game)
        {
            try
            {
                if (string.IsNullOrEmpty(game.Id))
                {
                    game.Id = ObjectId.GenerateNewId().ToString();
                }
                _games.Save(game);
                return ObjectId.Parse(game.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
                return ObjectId.Empty;
            }
        }

        public List<GameModel> GetGames() 
        { 
            return _games.FindAll().ToList();
        }

        public GameModel GetGameByName(string game_name)
        {
            var result = _games.Find(Query.EQ("game_name", game_name)).SingleOrDefault();
            return result;
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
