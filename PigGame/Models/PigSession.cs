using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PigGame.Models
{
    public class PigSession
    {
        private const string CurrentPlayerKey = "currplayer", CurrentRollKey = "currroll", CurrentTurnScoreKey = "currscore", PlayerOneScoreKey = "playerone", PlayerTwoScoreKey = "playertwo";

        private ISession session { get; set; }

        public PigSession(ISession session)
        {
            this.session = session;
        }

        public void SetCurrentPlayer(int currentPlayer) => session.SetInt32(CurrentPlayerKey, currentPlayer);

        public int? GetCurrentPlayer() => session.GetInt32(CurrentPlayerKey);

        public void SetCurrentRoll(int currentRoll) => session.SetInt32(CurrentRollKey, currentRoll);

        public int? GetCurrentRoll() => session.GetInt32(CurrentRollKey);

        public void SetCurrentTurnScore(int currentTurnScore) => session.SetInt32(CurrentTurnScoreKey, currentTurnScore);

        public int? GetCurrentTurnScore() => session.GetInt32(CurrentTurnScoreKey);

        public void SetPlayerOneScore(int playerOneScore) => session.SetInt32(PlayerOneScoreKey, playerOneScore);

        public int? GetPlayerOneScore() => session.GetInt32(PlayerOneScoreKey);

        public void SetPlayerTwoScore(int playerTwoScore) => session.SetInt32(PlayerTwoScoreKey, playerTwoScore);

        public int? GetPlayerTwoScore() => session.GetInt32(PlayerTwoScoreKey);

    }
}
