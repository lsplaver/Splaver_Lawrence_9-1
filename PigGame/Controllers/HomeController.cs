using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PigGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PigGame.Controllers
{
    public class HomeController : Controller
    {
        public static PigViewModel PigDefaults()
        {
            PigViewModel vm = new PigViewModel
            {
                Pig = new Pig
                {
                    CurrentPlayer = 1,
                    CurrentRollScore = 0,
                    CurrentTurnScore = 0,
                    PlayerOneTotalScore = 0,
                    PlayerTwoTotalScore = 0
                }
            };

            return vm;
        }

        public PigViewModel GetPigSessionValues()
        {
            var session = new PigSession(HttpContext.Session);
            PigViewModel vm = new PigViewModel
            {
                Pig = new Pig
                {
                    PlayerOneTotalScore = (int)session.GetPlayerOneScore(),
                    PlayerTwoTotalScore = (int)session.GetPlayerTwoScore(),
                    CurrentPlayer = (int)session.GetCurrentPlayer(),
                    CurrentRollScore = (int)session.GetCurrentRoll(),
                    CurrentTurnScore = (int)session.GetCurrentTurnScore()
                }
            };
            return vm;
        }
        public void SetPigSessions(int roll, int turnscore, int player, int playeronescore, int playertwoscore, string newgame)
        {
            var session = new PigSession(HttpContext.Session);
            session.SetCurrentPlayer(player);
            session.SetCurrentRoll(roll);
            session.SetCurrentTurnScore(turnscore);
            session.SetPlayerOneScore(playeronescore);
            session.SetPlayerTwoScore(playertwoscore);
            session.SetNewGame(newgame);
        }

        public void SetPigSessions(int roll, int turnscore, int player, int playeronescore, int playertwoscore)
        {
            var session = new PigSession(HttpContext.Session);
            session.SetCurrentPlayer(player);
            session.SetCurrentRoll(roll);
            session.SetCurrentTurnScore(turnscore);
            session.SetPlayerOneScore(playeronescore);
            session.SetPlayerTwoScore(playertwoscore);
        }

        public void SetPigSessions(int roll, int turnscore, int player)
        {
            var session = new PigSession(HttpContext.Session);
            session.SetCurrentPlayer(player);
            session.SetCurrentRoll(roll);
            session.SetCurrentTurnScore(turnscore);
        }

        [HttpGet]
        public ViewResult Index()
        {
            PigViewModel vm = new PigViewModel();
            var session = new PigSession(HttpContext.Session);
            string NewGameValue = session.GetNewGame();
            if (session.GetCurrentPlayer() == null && NewGameValue == "true")
            {
                PigDefaults();
                vm.NewGameValue = "false";
                SetPigSessions(PigDefaults().Pig.CurrentRollScore, PigDefaults().Pig.CurrentTurnScore, PigDefaults().Pig.CurrentPlayer,
                    PigDefaults().Pig.PlayerOneTotalScore, PigDefaults().Pig.PlayerTwoTotalScore, vm.NewGameValue);
                return View(PigDefaults());
            }
            else if (session.GetCurrentPlayer() == null)
            {
                PigDefaults();
                SetPigSessions(PigDefaults().Pig.CurrentRollScore, PigDefaults().Pig.CurrentTurnScore, PigDefaults().Pig.CurrentPlayer,
                    PigDefaults().Pig.PlayerOneTotalScore, PigDefaults().Pig.PlayerTwoTotalScore);
                TempData.Clear();
                return View(PigDefaults());
            }
            else if (session.GetCurrentPlayer() != null && NewGameValue == "true")
            {
                vm.NewGameValue = "false";
                SetPigSessions(PigDefaults().Pig.CurrentRollScore, PigDefaults().Pig.CurrentTurnScore, PigDefaults().Pig.CurrentPlayer,
                    PigDefaults().Pig.PlayerOneTotalScore, PigDefaults().Pig.PlayerTwoTotalScore, vm.NewGameValue);
                return View(PigDefaults());
            }
            else
            {
                GetPigSessionValues();
                SetPigSessions(GetPigSessionValues().Pig.CurrentRollScore, GetPigSessionValues().Pig.CurrentTurnScore, GetPigSessionValues().Pig.CurrentPlayer,
                    GetPigSessionValues().Pig.PlayerOneTotalScore, GetPigSessionValues().Pig.PlayerTwoTotalScore);
                return View(GetPigSessionValues());
            }
        }

        public RedirectToActionResult NewGame(PigViewModel vm)
        {
            var session = new PigSession(HttpContext.Session);
            vm.NewGameValue = "true";
            session.SetNewGame(vm.NewGameValue);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Roll(PigViewModel vm)
        {
            var session = new PigSession(HttpContext.Session);
            Random rand = new Random();
            int roll = rand.Next(1, 7);
            vm = GetPigSessionValues();
            if (roll > 1)
            {
                vm.Pig.CurrentRollScore = roll;
                vm.Pig.CurrentTurnScore = roll + (int)session.GetCurrentTurnScore();
                vm.Pig.CurrentPlayer = (int)session.GetCurrentPlayer();
                SetPigSessions(vm.Pig.CurrentRollScore, vm.Pig.CurrentTurnScore, vm.Pig.CurrentPlayer);
                return RedirectToAction("Index");
            }
            else
            {
                vm.Pig.CurrentRollScore = 0;
                vm.Pig.CurrentTurnScore = 0;
                if (vm.Pig.CurrentPlayer == 1)
                {
                    vm.Pig.PlayerOneTotalScore += vm.Pig.CurrentTurnScore;
                    SetPigSessions(vm.Pig.CurrentRollScore, vm.Pig.CurrentTurnScore, vm.Pig.CurrentPlayer);
                    session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
                    return RedirectToAction("Hold");
                }
                else
                { 
                    vm.Pig.PlayerTwoTotalScore += vm.Pig.CurrentTurnScore;
                    SetPigSessions(vm.Pig.CurrentRollScore, vm.Pig.CurrentTurnScore, vm.Pig.CurrentPlayer);
                    session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);
                    return RedirectToAction("Hold");
                }
            }
        }

        public RedirectToActionResult Hold(PigViewModel vm)
        {
            var session = new PigSession(HttpContext.Session);
            vm = GetPigSessionValues();
            if (vm.Pig.CurrentPlayer == 1)
            {
                vm.Pig.PlayerOneTotalScore += vm.Pig.CurrentTurnScore;
                session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
                if (vm.Pig.PlayerOneTotalScore >= 20)
                {
                    TempData["message"] = $"Player {vm.Pig.CurrentPlayer} wins!";
                    return RedirectToAction("NewGame");
                }
                vm.Pig.CurrentPlayer++;
            }
            else
            {
                vm.Pig.PlayerTwoTotalScore += vm.Pig.CurrentTurnScore;
                session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);
                if (vm.Pig.PlayerTwoTotalScore >= 20)
                {
                    TempData["message"] = $"Player {vm.Pig.CurrentPlayer} wins!";
                    return RedirectToAction("NewGame");
                }
                vm.Pig.CurrentPlayer--;
            }
            vm.Pig.CurrentRollScore = 0;
            vm.Pig.CurrentTurnScore = 0;
            SetPigSessions(vm.Pig.CurrentRollScore, vm.Pig.CurrentTurnScore, vm.Pig.CurrentPlayer);
            return RedirectToAction("Index");
        }
    }
}
