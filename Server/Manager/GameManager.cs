using System.Collections.Generic;
using Server.Objects;

namespace Server.Manager
{
    public class GameManager
    {
        
        private List<Player> _players;
        public GameManager(Player player1 , Player player2)
        {
            _players = new List<Player>();
            _players.Add(player1);
            _players.Add(player2);
        }
        public void StartGame()
        {
            _players[0].Char = 'O';
            _players[1].Char = 'X';
            
        }
        
        
            
            
    }
}