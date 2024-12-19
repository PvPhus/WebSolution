using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSolution.Data;
using WebSolution.Models;

namespace WebSolution.Games.Controllers
{
    public class GameController : Controller
    {
        private static readonly GameAdapter _gameAdapter = new GameAdapter(CoreDataConfig.DbName);
        private static readonly GameCategoryAdapter _gameCategoryAdapter = new GameCategoryAdapter(CoreDataConfig.DbName);

        private Dictionary<string, List<GameModel>> GetGroupedGames()
        {
            var games = _gameAdapter.GetGames();
            var categories = _gameCategoryAdapter.GetGameCategories();

            return categories.ToDictionary(
                category => category.CategoryName,
                category => games.Where(game => game.GameCategoryId == category.Id).ToList()
            );
        }

        public ActionResult Index()
        {
            var groupedGames = GetGroupedGames();
            return View(groupedGames);
        }

        [HttpGet]
        public JsonResult GetAllGames()
        {
            try
            {
                var groupedGames = GetGroupedGames();
                return Json(new
                {
                    Result = "Success",
                    Data = groupedGames.Select(g => new
                    {
                        Category = g.Key,
                        Games = g.Value.Select(game => new
                        {
                            game.GameName,
                            game.GameUrl,
                            game.GameAvatar
                        })
                    }),
                    Error = "",
                    Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Fail",
                    Error = ex.Message,
                    Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
