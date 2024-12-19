using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebSolution.Models
{
    public partial class GamePlayedModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("account_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("game_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GameId { get; set; }

        [BsonElement("score")]
        public string Score { get; set; }

        [BsonElement("coin")]
        public string Coin { get; set; }
        [BsonElement("level")]
        public string Level { get; set; }
        [BsonElement("map")]
        public string Map { get; set; }
    }
}
