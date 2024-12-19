using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using WebSolution.Models;
using WebSolution.WebHelper;

namespace WebSolution.Data
{
    public class AccountAdapter
    {
        private const String collectionName = "Account";
        private MongoDatabase _db;
        private MongoCollection<AccountModel> _collection;
        private MongoClient _client;
        private MongoServer _server;
        private SystemAdapter _systemAdapter;

        public AccountAdapter(string dbName)
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
            if (_collection == null)
            {
                _collection = _db.GetCollection<AccountModel>(collectionName);
                _collection.CreateIndex(IndexKeys.Descending("username"),
                                        IndexOptions.SetName("username"));
                _collection.CreateIndex(IndexKeys.Descending("email"),
                        IndexOptions.SetName("email"));
            }
        }
        public ObjectId SaveAccount(AccountModel account)
        {
            try
            {
                if (account.id == ObjectId.Empty)
                {
                    account.id = ObjectId.GenerateNewId();
                }

                var result = _collection.Save(account);
                return result.Ok ? account.id : ObjectId.Empty;
            }
            catch
            {
                return ObjectId.Empty;
            }
        }
        public List<AccountModel> GetAccounts()
        {
            //IMongoSortBy sort = SortBy.Descending("_DiemSo");
            //var result = _collection.FindAll().SetSortOrder(sort).SetSkip(100).SetLimit(100).ToList();//Lay top 100-200 nguowif co diem so cao nhat
            return _collection.FindAll().ToList();
        }
        public AccountModel GetAccountByEmailAndPassword(string email, string password)
        {
            var result = _collection.Find(Query.And(Query.EQ("email", email), Query.EQ("password", password))).SingleOrDefault();
            return result;
        }

        public AccountModel GetAccountByEmail(string email)
        {
            var result = _collection.Find(Query.EQ("email", email)).SingleOrDefault();
            return result;
        }
        public bool Remove(long id)
        {
            try
            {
                MongoCollection<AccountModel> _collection = _db.GetCollection<AccountModel>(collectionName);
                return _collection.Remove(Query.EQ("_id", id), WriteConcern.Acknowledged).Ok;
            }
            catch
            {
                return false;
            }
        }
    }
}
