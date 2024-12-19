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
    public class CategoryAdapter
    {
        private const String collectionName = "Category";
        private MongoDatabase _db;
        private MongoCollection<Category> _collection;
        private MongoClient _client;
        private MongoServer _server;
        private SystemAdapter _systemAdapter;

        public CategoryAdapter(string dbName)
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
                _collection = _db.GetCollection<Category>(collectionName);
                _collection.CreateIndex(IndexKeys.Descending("language").Descending("parent").Descending("orderNumber"),
                                        IndexOptions.SetName("language_parent_orderNumber"));
            }
        }
        public void CategoryViewModel(ref Category category, int language = 0)
        {
            if (category.categoryLanguage != null)
                category.categoryLanguage = category.categoryLanguage.Where(x => x.language == language).ToList();
        }
        public Category CategoryViewModel(Category category, int language = 0)
        {
            if (category.categoryLanguage != null)
            {
                category.categoryLanguage = category.categoryLanguage.Where(x => x.language == language).ToList();
                return category;
            }
            else
                return category;
        }
        public int Save(Category obj)
        {
            try
            {
                if (obj.categoryLanguage != null)
                {
                    List<CategoryLanguage> listCategoryLanguage = new List<CategoryLanguage>();
                    foreach (Language lang in Enum.GetValues(typeof(Language)))
                    {
                        var getLangObj = obj.categoryLanguage.Where(x => x.language == (int)lang).FirstOrDefault();
                        if (getLangObj != null && getLangObj.id > 0)
                        {
                            listCategoryLanguage.Add(getLangObj);
                        }
                        else
                        {
                            listCategoryLanguage.Add(new CategoryLanguage() { id = 9999, language = (int)lang });
                        }
                    }
                    obj.categoryLanguage = listCategoryLanguage;
                }
                if (obj.id == 0)
                {
                    obj.id = _systemAdapter.GetIncreaseID(collectionName);
                }
                if (_collection.Save(obj).Ok)
                    return obj.id;
                return -1;
            }
            catch
            {
                return 0;
            }
        }
        public int SaveNow(Category obj)
        {
            try
            {
                if (obj.id == 0)
                {
                    obj.id = _systemAdapter.GetIncreaseID(collectionName);
                }
                if (_collection.Save(obj).Ok)
                    return obj.id;
                return -1;
            }
            catch
            {
                return 0;
            }
        }
        public List<Category> GetCategories()
        {
            var result = _collection.FindAll().ToList();
            return result;
        }
        public List<Category> GetCategoriesLanguage(int language = 0)
        {
            var result = _collection.FindAll().ToList();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    result[i] = CategoryViewModel(result[i], language);
                }
            }
            return result;
        }
        public List<Category> GetCategoriesNoHome()
        {
            var result = _collection.FindAll().Where(x => x.id != 1).ToList();
            return result;
        }
        public List<Category> GetCategoriesByLevel(int lv)
        {
            var result = lv == 1 ? _collection.Find(Query.EQ("parent", 0)).SetSortOrder(SortBy.Descending("_id")).ToList() : _collection.Find(Query.GT("parent", 0)).SetSortOrder(SortBy.Descending("_id")).ToList();
            return result;
        }
        public Category GetCategoryByID(int id, bool fullData = false, int language = 0)
        {
            var result = _collection.Find(Query.EQ("_id", id)).SingleOrDefault();
            if (result != null && !fullData)            
                return CategoryViewModel(result, language);
            
            else
                return result;
        }
        public List<Category> GetCategoryByListId(List<int> listId)
        {
            List<BsonValue> bson = new List<BsonValue>();
            foreach (var item in listId)
            {
                bson.Add(BsonValue.Create(item));
            }
            var result = _collection.Find(Query.In("_id", bson)).ToList();
            return result;
        }
        public List<Category> GetCategoriesByParentId(int id)
        {
            var result = _collection.Find(Query.EQ("parent", id)).SetSortOrder(SortBy.Ascending("name")).ToList();
            return result;
        }
        public Category GetCategoryByLowerName(string lowerName)
        {
            lowerName = lowerName.ToLower().Trim();
            var allCat = GetCategories();
            foreach (var itemCat in allCat)
            {
                foreach (var itemLang in itemCat.categoryLanguage)
                {
                    if (!string.IsNullOrEmpty(itemLang.name))
                    {
                        if (itemLang.name.ToLower() == lowerName)
                        {
                            return itemCat;
                        }
                    }
                }
            }
            return null;
        }
        public Category GetCategoryByOriginalUrl(string originalUrl)
        {
            var allCat = GetCategories();
            foreach (var itemCat in allCat)
            {
                if (itemCat.originalUrl == originalUrl)
                {
                    return itemCat;
                }
            }
            return null;
        }
        public bool Remove(int id)
        {
            try
            {
                MongoCollection<Category> _collection = _db.GetCollection<Category>(collectionName);
                return _collection.Remove(Query.EQ("_id", id), WriteConcern.Acknowledged).Ok;
            }
            catch
            {
                return false;
            }
        }
    }
}
