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
    public class RankPlayerAdapter
    {
        private MongoCollection<GamePlayedModel> _collection;
        public List<GamePlayedModel> GetRankByGameId(string GameId)
        {
            var result = _collection.Find(Query.EQ("game_id", GameId)).ToList();
            return result;
        }
    }
}
