using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebSolution.Models
{
    public class GameCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("category_name")]
        public string CategoryName { get; set; }
    }

    public class GameModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("game_avatar")]
        public string GameAvatar { get; set; }

        [BsonElement("game_category_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GameCategoryId { get; set; }

        [BsonElement("game_name")]
        public string GameName { get; set; }

        [BsonElement("game_url")]
        public string GameUrl { get; set; }
    }
}