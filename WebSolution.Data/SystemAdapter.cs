using System;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using WebSolution.Models;

namespace WebSolution.Data
{
    public class SystemAdapter
    {

        private const String collectionName = "SystemObj";
        private MongoDatabase _db;
        private MongoCollection<SystemObj> _collection;
        private MongoClient _client;
        private MongoServer _server;

        public SystemAdapter(string dbName)
        {
            if (_client == null)
                _client = new MongoClient(CoreDataConfig.MongoConnectionString);
            if (_server == null)
                _server = _client.GetServer();
            if (_db == null)
            {
                _db = _server.GetDatabase(dbName);
            }
            if (_collection == null)
            {
                _collection = _db.GetCollection<SystemObj>(collectionName);
                _collection.CreateIndex(IndexKeys.Ascending("Key"), IndexOptions.SetName("Key"));

            }
        }

        public int GetIncreaseID(String keyname)
        {
            var query = Query.EQ("Key", keyname);
            var sortBy = SortBy.Null;
            var update = Update.Inc("Value", 1);
            try
            {
                var result =
                    (SystemObj)
                        _collection.FindAndModify(new FindAndModifyArgs()
                        {
                            Query = query,
                            SortBy = sortBy,
                            Update = update,
                            Upsert = true,
                            VersionReturned = FindAndModifyDocumentVersion.Modified
                        })
                            .GetModifiedDocumentAs(typeof(SystemObj));
                if (result == null)
                {
                    _collection.Insert(new SystemObj(keyname, 1));
                    return 1;
                }
                return result.Value;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool SaveKey(SystemObj obj)
        {
            if (_collection.Save(obj).Ok)
                return true;
            return false;
        }

        public bool UpdateKey(SystemObj obj)
        {
            var query = Query.EQ("Key", obj.Key);
            var update = Update.Set("Value", obj.Value);
            try
            {
                if (_collection.Update(query, update, UpdateFlags.Upsert, WriteConcern.Acknowledged).Ok) return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public SystemObj GetObj(string key)
        {
            var query = Query.EQ("Key", key);
            return _collection.FindOne(query);
        }

    }
}
