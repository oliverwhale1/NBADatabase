using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Basketball_Project.Models;


namespace Basketball_Project.Controllers
{

    public class HomeController : Controller
    {
        private PlayersDBEntities4 _db = new PlayersDBEntities4();
        public ActionResult Index(string playerTeam, string playerTeam2, string searchString, string searchString2, string playerPosition, string playerPosition2, string sortOrder)
        {
            //team search
            var TeamList = new List<string>();
            var TeamQuery = from t in _db.PlayerInfoes
                            orderby t.Team
                            select t.Team.ToUpper();

            TeamList.AddRange(TeamQuery.Distinct());
            ViewBag.playerTeam = new SelectList(TeamList);

            var TeamList2 = new List<string>();
            var TeamQuery2 = from t in _db.PlayerInfoes
                            orderby t.Team
                            select t.Team.ToUpper();

            TeamList2.AddRange(TeamQuery2.Distinct());
            ViewBag.playerTeam2 = new SelectList(TeamList2);

            //position search
            var PositionList = new List<string>();
            var PositionQuery = from f in _db.PlayerInfoes
                            orderby f.Position
                            select f.Position.ToUpper();

            PositionList.AddRange(PositionQuery.Distinct());
            ViewBag.playerPosition = new SelectList(PositionList);

            var PositionList2 = new List<string>();
            var PositionQuery2 = from f in _db.PlayerInfoes
                                orderby f.Position
                                select f.Position.ToUpper();

            PositionList2.AddRange(PositionQuery2.Distinct());
            ViewBag.playerPosition2 = new SelectList(PositionList2);

            var players = from p in _db.PlayerInfoes select p;

            //name search
            if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(searchString2))
            {
                players = players.Where(s => s.Name.Contains(searchString) || s.Name.IndexOf(searchString2) != -1);
            }


            if (!String.IsNullOrEmpty(playerTeam) || !String.IsNullOrEmpty(playerTeam2))
            {
                players = players.Where(x => x.Team == playerTeam || x.Team == playerTeam2);
            }

            if (!String.IsNullOrEmpty(playerPosition) || !String.IsNullOrEmpty(playerPosition2))
            {
                players = players.Where(t => t.Position == playerPosition || t.Position == playerPosition2);
                
            }

            //sort by points, assists, rebounds
            ViewBag.PointsSortParm = sortOrder == "Points" ? "points_desc" : "Points";
                ViewBag.AssistsSortParm = sortOrder == "Assists" ? "assists_desc" : "Assists";
                ViewBag.ReboundsSortParm = sortOrder == "Rebounds" ? "rebounds_desc" : "Rebounds";

                switch (sortOrder)
                {
                    case "Points":
                        players = players.OrderByDescending(s => s.Points);
                        break;
                    case "points_desc":
                        players = players.OrderBy(s => s.Points);
                        break;
                    case "Assists":
                        players = players.OrderByDescending(s => s.Assists);
                        break;
                    case "assists_desc":
                        players = players.OrderBy(s => s.Assists);
                        break;
                    case "Rebounds":
                        players = players.OrderByDescending(s => s.Rebounds);
                        break;
                    case "rebounds_desc":
                        players = players.OrderBy(s => s.Rebounds);
                        break;
                    default:
                        players = players.OrderBy(s => s.Name);
                        break;
                }
            
            return View(players.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] PlayerInfo player)
        {


            if (player.Picture == null)
            {
                player.Picture = "https://www.logodesignlove.com/images/classic/nba-logo.jpg";
            }

            

            if (ModelState.IsValid)
            {
                //try
                //{
                _db.PlayerInfoes.Add(player);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                //}

                //catch (DbEntityValidationException dbEx)
                //{
                //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    {
                //        foreach (var validationError in validationErrors.ValidationErrors)
                //        {
                //            Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}", validationErrors.Entry.Entity.GetType().FullName, validationError.PropertyName, validationError.ErrorMessage);
                //        }
                //    }
                //}
            }
            return View(player);
        }

        public ActionResult Edit(int? id)
        {
            PlayerInfo player = _db.PlayerInfoes.Find(id);
            return View(player);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit([Bind(Include = "id, Picture, Name, Team, Position, Points, Assists, Rebounds")] PlayerInfo player)
        {


            if (player.Picture == null)
            {
                player.Picture = "https://www.logodesignlove.com/images/classic/nba-logo.jpg";
            }
            if (ModelState.IsValid)
            {
                _db.Entry(player).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        public ActionResult Delete(int? id)
        {
            PlayerInfo player = _db.PlayerInfoes.Find(id);
            return View(player);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id)
        {
            //find the player again
            PlayerInfo player = _db.PlayerInfoes.Find(id);

            //delete from database
            _db.PlayerInfoes.Remove(player);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        

    }
}