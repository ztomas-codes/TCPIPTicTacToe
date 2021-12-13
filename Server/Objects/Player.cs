namespace Server.Objects
{
    public class Player
    {
        public string Name { get; private set; }
        public int Score { get; private set; }
        public string IP { get; private set; }
        public int Port { get; private set; }
        public  char Char { get; set; }
        
        public Player(string name, int score, string ip, int port)
        {
            Name = name;
            Score = score;
            IP = ip;
            Port = port;
        }

        
    }
}