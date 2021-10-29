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
        public IActionResult Index(PigViewModel vm)
        {
            string? NewGameValue = vm.NewGameValue;
            var session = new PigSession(HttpContext.Session);
            string tempCurrentPlayer = session.GetCurrentPlayer().ToString();
            string tempCurrentRoll = session.GetCurrentRoll().ToString();
            string tempCurrentTurnScore = session.GetCurrentTurnScore().ToString();
            string tempPlayerOneScore = session.GetPlayerOneScore().ToString();
            string tempPlayerTwoScore = session.GetPlayerTwoScore().ToString();
            var tempSession = session;
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
                return View();
            }
        }

        // [HttpGet]
        public IActionResult NewGame(PigViewModel vm)
        {
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
            return RedirectToAction("Index", vm);
        }

        // [HttpPost]
        [HttpGet]
        public IActionResult Roll(PigViewModel vm)
        {
            var session = new PigSession(HttpContext.Session);
            string tempCurrentPlayer = session.GetCurrentPlayer().ToString();
            // PigViewModel vm = new PigViewModel();
            Random rand = new Random();
            int roll = rand.Next(1, 7);
            if (roll > 1)
            {
                vm.Pig.CurrentRollScore = roll;
                vm.Pig.CurrentTurnScore += vm.Pig.CurrentRollScore;
                // if (roll >= 20)
                // {
                    return View(vm);
                // }    
            }
            else
            {
                vm.Pig.CurrentRollScore = 0;
                vm.Pig.CurrentTurnScore = 0;
                switch(vm.Pig.CurrentPlayer)
                {
                    case 1:
                        vm.Pig.PlayerOneTotalScore += vm.Pig.CurrentTurnScore;
                        vm.Pig.CurrentPlayer++;
                        // return View(vm);
                        return RedirectToAction("Hold", vm);
                    case 2:
                    default:
                        vm.Pig.PlayerTwoTotalScore += vm.Pig.CurrentTurnScore;
                        vm.Pig.CurrentPlayer--;
                        // return View(vm);
                        return RedirectToAction("Hold", vm);
                }
            }
        }

        /* [HttpPost]
        public ViewResult Hold(PigViewModel vm)
        {
            if (vm.Pig.CurrentPlayer == 1)
            {
                vm.Pig.PlayerOneTotalScore += vm.Pig.CurrentTurnScore;
                vm.Pig.CurrentPlayer++;
            }
            else
            {
                vm.Pig.PlayerTwoTotalScore += vm.Pig.CurrentTurnScore;
                vm.Pig.CurrentPlayer--;
            }
            return View(vm);
        } */
    }
}
