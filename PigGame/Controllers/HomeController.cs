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
        [HttpGet]
        public ViewResult Index()// PigViewModel vm)
        {
            PigViewModel vm = new PigViewModel();
            var session = new PigSession(HttpContext.Session);
            string NewGameValue = session.GetNewGame(); // vm.NewGameValue;
            //int? tempCurrentPlayer = session.GetCurrentPlayer();
            //int? tempCurrentRoll = session.GetCurrentRoll();
            //int? tempCurrentTurnScore = session.GetCurrentTurnScore();
            //int? tempPlayerOneScore = session.GetPlayerOneScore();
            //int? tempPlayerTwoScore = session.GetPlayerTwoScore();
            //var tempSession = session;
            if ((session.GetCurrentPlayer() == null || session.GetCurrentPlayer().ToString() == "") && NewGameValue == "true")
            {
                // set values to default
                //Pig pig = new Pig();
                //pig.CurrentPlayer = 1;
                //pig.CurrentRollScore = 0;
                //pig.CurrentTurnScore = 0;
                //pig.PlayerOneTotalScore = 0;
                //pig.PlayerTwoTotalScore = 0;
                vm.Pig = new Pig
                {
                    CurrentPlayer = 1,
                    CurrentRollScore = 0,
                    CurrentTurnScore = 0,
                    PlayerOneTotalScore = 0,
                    PlayerTwoTotalScore = 0
                };
                vm.NewGameValue = "false";
                session.SetCurrentRoll(vm.Pig.CurrentRollScore);
                session.SetCurrentPlayer(vm.Pig.CurrentPlayer);
                session.SetCurrentTurnScore(vm.Pig.CurrentTurnScore);
                session.SetNewGame(vm.NewGameValue);
                session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
                session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);
                return View(vm);
            }
            else if (session.GetCurrentPlayer() == null || session.GetCurrentPlayer().ToString() == "")
            {
                // set values to default
                //Pig pig = new Pig();
                //PigViewModel vm1 = new PigViewModel();
                //pig.CurrentPlayer = 1;
                //pig.CurrentRollScore = 0;
                //pig.CurrentTurnScore = 0;
                //pig.PlayerOneTotalScore = 0;
                //pig.PlayerTwoTotalScore = 0;
                //vm1.NewGameValue = "false";
                //vm1.Pig = new Pig
                //{
                //    CurrentPlayer = pig.CurrentPlayer,
                //    CurrentRollScore = pig.CurrentRollScore,
                //    CurrentTurnScore = pig.CurrentTurnScore,
                //    PlayerOneTotalScore = pig.PlayerOneTotalScore,
                //    PlayerTwoTotalScore = pig.PlayerTwoTotalScore
                //};
                // vm1.Pig.CurrentRollScore = pig.CurrentRollScore;
                // vm1.Pig.CurrentTurnScore = pig.CurrentTurnScore;
                // vm1.Pig.PlayerOneTotalScore = pig.PlayerOneTotalScore;
                // vm1.Pig.PlayerTwoTotalScore = pig.PlayerTwoTotalScore;
                vm.Pig = new Pig
                {
                    CurrentPlayer = 1,
                    CurrentRollScore = 0,
                    CurrentTurnScore = 0,
                    PlayerOneTotalScore = 0,
                    PlayerTwoTotalScore = 0
                };
                session.SetCurrentPlayer(vm.Pig.CurrentPlayer);
                session.SetCurrentRoll(vm.Pig.CurrentRollScore);
                session.SetCurrentTurnScore(vm.Pig.CurrentTurnScore);
                session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
                session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);
                return View(vm);
            }
            else
            {
                // keep values unchanged
                // var tempPigCurrentPlayer = ;
                vm.Pig = new Pig
                {
                    CurrentPlayer = (int)session.GetCurrentPlayer(),
                    CurrentRollScore = (int)session.GetCurrentRoll(),
                    CurrentTurnScore = (int)session.GetCurrentTurnScore(),
                    PlayerOneTotalScore = (int)session.GetPlayerOneScore(),
                    PlayerTwoTotalScore = (int)session.GetPlayerTwoScore()
                };
                session.SetCurrentPlayer(vm.Pig.CurrentPlayer);
                session.SetCurrentRoll(vm.Pig.CurrentRollScore);
                session.SetCurrentTurnScore(vm.Pig.CurrentTurnScore);
                session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
                session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);

                return View(vm);
            }
        }

        // [HttpGet]
        [HttpPost]
        public RedirectToActionResult NewGame(PigViewModel vm)
        {
            var session = new PigSession(HttpContext.Session);
            // PigViewModel vm = new PigViewModel();
            // Pig pig = new Pig();
            // pig.NewGameValue = "true";
            // bool NewGameValue = true;
            vm.NewGameValue = "true"; // NewGameValue.ToString();
            // vm.Pig.CurrentPlayer = pig.CurrentPlayer;
            // vm.Pig.CurrentRollScore = pig.CurrentRollScore;
            // vm.Pig.CurrentTurnScore = pig.CurrentTurnScore;
            // vm.Pig.PlayerOneTotalScore = pig.PlayerOneTotalScore;
            // vm.Pig.PlayerTwoTotalScore = pig.PlayerTwoTotalScore;
            session.SetNewGame(vm.NewGameValue);
            return RedirectToAction("Index"); // , vm);
        }

        [HttpPost]
        // [HttpGet]
        public RedirectToActionResult Roll(PigViewModel vm)
        {
            var session = new PigSession(HttpContext.Session);
            //int? tempCurrentPlayer = session.GetCurrentPlayer();
            //int? tempCurrentRoll = session.GetCurrentRoll();
            //int? tempCurrentTurnScore = session.GetCurrentTurnScore();
            //int? tempPlayerOneScore = session.GetPlayerOneScore();
            //int? tempPlayerTwoScore = session.GetPlayerTwoScore();
            // PigViewModel vm = new PigViewModel();
            Random rand = new Random();
            int roll = rand.Next(1, 7);
            vm.Pig = new Pig();
            if (roll > 1)
            {
                //vm.Pig = new Pig
                //{
                //    CurrentRollScore = roll,
                //    CurrentTurnScore = (roll + (int)tempCurrentTurnScore),
                //    CurrentPlayer = (int)tempCurrentPlayer,
                //    PlayerOneTotalScore = (int)tempPlayerOneScore,
                //    PlayerTwoTotalScore = (int)tempPlayerTwoScore
                //};
                vm.Pig.CurrentRollScore = roll;
                vm.Pig.CurrentTurnScore = roll + (int)session.GetCurrentTurnScore();
                vm.Pig.CurrentPlayer = (int)session.GetCurrentPlayer();
                vm.Pig.PlayerOneTotalScore = (int)session.GetPlayerOneScore();
                vm.Pig.PlayerTwoTotalScore = (int)session.GetPlayerTwoScore();
                session.SetCurrentPlayer(vm.Pig.CurrentPlayer);
                session.SetCurrentRoll(vm.Pig.CurrentRollScore);
                session.SetCurrentTurnScore(vm.Pig.CurrentTurnScore);
                session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
                session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);
                if (vm.Pig.CurrentPlayer == 1)
                {
                    vm.Pig.PlayerOneTotalScore += vm.Pig.CurrentTurnScore;
                    session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
                    if (vm.Pig.PlayerOneTotalScore >= 20)
                    {
                        TempData["message"] = $"Player {vm.Pig.CurrentPlayer} wins!";
                        return RedirectToAction("NewGame"); // , vm);
                    }
                    // return View("Index", vm);
                    else
                    {
                        return RedirectToAction("Index"); // , vm);
                    }
                }
                else
                {
                    vm.Pig.PlayerTwoTotalScore += vm.Pig.CurrentTurnScore;
                    session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);
                    if (vm.Pig.PlayerTwoTotalScore >= 20)
                    {
                        TempData["message"] = $"Player {vm.Pig.CurrentPlayer} wins!";
                        return RedirectToAction("NewGame"); // , vm);
                    }
                    else
                    {
                        return RedirectToAction("Index"); // , vm);
                    }
                }
                // return View("Index", vm);
            }
            else
            {
                vm.Pig.CurrentRollScore = 0;
                vm.Pig.CurrentTurnScore = 0;
                //tempCurrentRoll = 0;
                //tempCurrentTurnScore = 0;
                if (vm.Pig.CurrentPlayer == 1)
                {
                    //tempPlayerOneScore += tempCurrentTurnScore;
                     vm.Pig.PlayerOneTotalScore += vm.Pig.CurrentTurnScore;
                    // vm.Pig.CurrentPlayer++;
                    // return View(vm);
                    // vm.Pig.PlayerOneTotalScore = (int)tempPlayerOneScore;
                    session.SetCurrentPlayer(vm.Pig.CurrentPlayer);
                    session.SetCurrentRoll(vm.Pig.CurrentRollScore);
                    session.SetCurrentTurnScore(vm.Pig.CurrentTurnScore);
                    session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
                    session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);
                    return RedirectToAction("Hold"); // , vm);
                }
                else
                { 
                    //tempPlayerTwoScore += tempCurrentTurnScore;
                    vm.Pig.PlayerTwoTotalScore += vm.Pig.CurrentTurnScore;
                    //vm.Pig.CurrentPlayer--;
                    // return View(vm);
                    //vm.Pig.PlayerTwoTotalScore = (int)tempPlayerTwoScore;
                    session.SetCurrentPlayer((int)vm.Pig.CurrentPlayer);
                    session.SetCurrentRoll(vm.Pig.CurrentRollScore);
                    session.SetCurrentTurnScore(vm.Pig.CurrentTurnScore);
                    session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
                    session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);
                    return RedirectToAction("Hold"); // , vm);
                }
            }
        }

        [HttpPost]
        public RedirectToActionResult Hold(PigViewModel vm)
        {
            /* PigViewModel */ vm.Pig = new Pig();
            var session = new PigSession(HttpContext.Session);
            //int? tempCurrentPlayer = session.GetCurrentPlayer();
            //int? tempCurrentRoll = session.GetCurrentRoll();
            //int? tempCurrentTurnScore = session.GetCurrentTurnScore();
            //int? tempPlayerOneScore = session.GetPlayerOneScore();
            //int? tempPlayerTwoScore = session.GetPlayerTwoScore();
            //vm.Pig.CurrentPlayer = (int)tempCurrentPlayer;
            //vm.Pig.CurrentRollScore = (int)tempCurrentRoll;
            //vm.Pig.CurrentTurnScore = (int)tempCurrentTurnScore;
            //vm.Pig.PlayerOneTotalScore = (int)tempPlayerOneScore;
            //vm.Pig.PlayerTwoTotalScore = (int)tempPlayerTwoScore;
            vm.Pig.CurrentPlayer = (int)session.GetCurrentPlayer();
            vm.Pig.CurrentRollScore = (int)session.GetCurrentRoll();
            vm.Pig.CurrentTurnScore = (int)session.GetCurrentTurnScore();
            vm.Pig.PlayerOneTotalScore = (int)session.GetPlayerOneScore();
            vm.Pig.PlayerTwoTotalScore = (int)session.GetPlayerTwoScore();
            if (vm.Pig.CurrentPlayer == 1)
            {
                // vm.Pig.PlayerOneTotalScore += vm.Pig.CurrentTurnScore;
                vm.Pig.CurrentPlayer++;
            }
            else
            {
                // vm.Pig.PlayerTwoTotalScore += vm.Pig.CurrentTurnScore;
                vm.Pig.CurrentPlayer--;
            }
            vm.Pig.CurrentRollScore = 0;
            vm.Pig.CurrentTurnScore = 0;
            session.SetCurrentPlayer(vm.Pig.CurrentPlayer);
            session.SetCurrentRoll(vm.Pig.CurrentRollScore);
            session.SetCurrentTurnScore(vm.Pig.CurrentTurnScore);
            session.SetPlayerOneScore(vm.Pig.PlayerOneTotalScore);
            session.SetPlayerTwoScore(vm.Pig.PlayerTwoTotalScore);
            return RedirectToAction("Index"); // , vm);
        }
    }
}
