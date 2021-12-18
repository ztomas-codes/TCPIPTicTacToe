using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Server.Objects;

namespace Server.Manager
{
    public class GameManager
    {

        private Player _player1;
        private Player _player2;
        private char[,] board = new char[,] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        //NetworkStream StreamPlayer1 { get; set; }
        //NetworkStream StreamPlayer2 { get; set; }
        List<string> PossiblePackets = new List<string>();
        private List<TcpClient> players = new List<TcpClient>();

        private Task _task { get; set; } 
        private Task _task2 { get; set; }


        public GameManager(Player player1, Player player2 , NetworkStream _streamPlayer1 , NetworkStream _streamPlayer2)
        {
            _player1 = player1;
            _player2 = player2;
            _player1.NetworkStream = _streamPlayer1;
            _player2.NetworkStream = _streamPlayer2;
            PossiblePackets.Add(PacketManager.WIN);
            PossiblePackets.Add(PacketManager.MOVE);
            PossiblePackets.Add(PacketManager.TURN);
            PossiblePackets.Add(PacketManager.PLAYERTURN);
            PossiblePackets.Add(PacketManager.NOTYOURTURN);
            players.Add(player1.Client);
            players.Add(player2.Client);
            _task = Task.Run(async() => ListenPl1());
            _task2 = Task.Run(async() => ListenPl2());
            
        }

        public void StartGame()
        {
            byte[] packet = PacketManager.CreatePacket($"{PacketManager.STARTGAME}|game started {_player1.Name} vs {_player2.Name}");
            _player1.Char = 'O';
            _player2.Char = 'X';
            _player1.NetworkStream.Write(packet, 0, packet.Length);
            _player1.NetworkStream.Flush();
            _player2.NetworkStream.Write(packet, 0, packet.Length);
            _player2.NetworkStream.Flush();
            _player1.Turn = true;
            _player2.Turn = false;
            var board = PacketManager.CreatePacket(BuildBoard());
            _player1.NetworkStream.Write(board , 0 , board.Length);
            _player2.NetworkStream.Write(board , 0 , board.Length);
       


        }
        private void ListenPl1()
        {
            while(true)
            {
                while (true)
                {
                    byte[] bytes1 = new byte[1024];
                    
                    _player1.NetworkStream.Read(bytes1, 0, bytes1.Length);
                    if (!CheckIfBothConnected())
                    {
                        var packet = PacketManager.CreatePacket($"{PacketManager.DISCONNECT}|{_player1.Name} disconnected");
                        _player1.NetworkStream.Write(packet, 0, packet.Length);
                        _player1.NetworkStream.Dispose();
                        _player2.NetworkStream.Write(packet, 0, packet.Length);
                        _player2.NetworkStream.Dispose();
                        break;
                    }
                    SortPackets(bytes1 , _player1 , _player2);
                    System.Console.WriteLine(PacketManager.GetPacket(bytes1));
                    
                    
                }
            }
        }
        private void ListenPl2()
        {
            while (true)
            {
                byte[] bytes2 = new byte[1024];
                _player2.NetworkStream.Read(bytes2, 0, bytes2.Length);
                if (!CheckIfBothConnected())
                {
                    var packet = PacketManager.CreatePacket($"{PacketManager.DISCONNECT}|{_player1.Name} disconnected");

                    _player2.NetworkStream.Write(packet, 0, packet.Length);
                    _player2.NetworkStream.Dispose();
                    break;
                }
                SortPackets(bytes2, _player2, _player1);
                System.Console.WriteLine(PacketManager.GetPacket(bytes2));
            }

        }
        private bool CheckIfBothConnected()
        {
            return _player1.NetworkStream.CanRead && _player2.NetworkStream.CanRead;

        }
        private string CheckWinner()
        {
            if (board[0, 0] == _player1.Char && board[0, 1] == _player1.Char && board[0, 2] == _player1.Char)
            {
                _player1.Score++;
                return _player1.Name;
            }
            else if (board[1, 0] == _player1.Char && board[1, 1] == _player1.Char && board[1, 2] == _player1.Char)
            {
                _player1.Score++;
                return _player1.Name;
            }
            else if (board[2, 0] == _player1.Char && board[2, 1] == _player1.Char && board[2, 2] == _player1.Char)
            {
                _player1.Score++;
                return _player1.Name;
            }
            else if (board[0, 0] == _player1.Char && board[1, 0] == _player1.Char && board[2, 0] == _player1.Char)
            {
                _player1.Score++;
                return _player1.Name;
            }
            else if (board[0, 1] == _player1.Char && board[1, 1] == _player1.Char && board[2, 1] == _player1.Char)
            {
                _player1.Score++;
                return _player1.Name;
            }
            else if (board[0, 2] == _player1.Char && board[1, 2] == _player1.Char && board[2, 2] == _player1.Char)
            {
                _player1.Score++;
                return _player1.Name;
            }
            else if (board[0, 0] == _player1.Char && board[1, 1] == _player1.Char && board[2, 2] == _player1.Char)
            {
                _player1.Score++;
                return _player1.Name;
            }
            else if (board[0, 2] == _player1.Char && board[1, 1] == _player1.Char && board[2, 0] == _player1.Char)
            {
                _player1.Score++;
                return _player1.Name;
            }


            if (board[0, 0] == _player2.Char && board[0, 1] == _player2.Char && board[0, 2] == _player2.Char)
            {
                _player2.Score++;
                return _player2.Name;
            }
            else if (board[1, 0] == _player2.Char && board[1, 1] == _player2.Char && board[1, 2] == _player2.Char)
            {
                _player2.Score++;
                return _player2.Name;
            }
            else if (board[2, 0] == _player2.Char && board[2, 1] == _player2.Char && board[2, 2] == _player2.Char)
            {
                _player2.Score++;
                return _player2.Name;
            }
            else if (board[0, 0] == _player2.Char && board[1, 0] == _player2.Char && board[2, 0] == _player2.Char)
            {
                _player2.Score++;
                return _player2.Name;
            }
            else if (board[0, 1] == _player2.Char && board[1, 1] == _player2.Char && board[2, 1] == _player2.Char)
            {
                _player2.Score++;
                return _player2.Name;
            }
            else if (board[0, 2] == _player2.Char && board[1, 2] == _player2.Char && board[2, 2] == _player2.Char)
            {
                _player2.Score++;
                return _player2.Name;
            }
            else if (board[0, 0] == _player2.Char && board[1, 1] == _player2.Char && board[2, 2] == _player2.Char)
            {
                _player2.Score++;
                return _player2.Name;
            }
            else if (board[0, 2] == _player2.Char && board[1, 1] == _player2.Char && board[2, 0] == _player2.Char)
            {
                _player2.Score++;
                return _player2.Name;
            }

            if (board[0,0] != '1' && board[0,1] != '2' && board[0,2] != '3' && board[1,0] != '4' && board[1,1] != '5' && board[1,2] != '6' && board[2,0] != '7' && board[2,1] != '8' && board[2,2] != '9')
            {
                return "Tie";
               
            }
            return null;
            
        }
        private void SortPackets(byte[] packet , Player pl , Player pl1)
        {
                 string packetString = PacketManager.GetPacket(packet);
           
                if(packetString.StartsWith(PacketManager.MOVE) && pl.Turn == true)  
                {
                    int move = RemoveAllCharFromInt(packetString.Replace("|" , ""));
                    if (CheckIfFree(move)) 
                    {
                        
                        InsertIntoBoard(move, pl.Char);
                        
                        pl.Turn = false;
        
                        pl1.Turn = true;
                        System.Console.WriteLine(move);
                    }
                }
            
        }
        private void InsertIntoBoard(int move, char charToInsert)
        {
            byte[] packet = PacketManager.CreatePacket($"{PacketManager.WRONGMOVE}|Wrong move");
            var boardPacket = PacketManager.CreatePacket(BuildBoard());
            switch (move)
            {
                case 1:
                    board[0, 0] = charToInsert;
                    boardPacket = PacketManager.CreatePacket(BuildBoard());
                    _player1.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    _player2.NetworkStream.Write(boardPacket , 0 , boardPacket.Length);
                    
                    break;
                case 2:
                    board[0, 1] = charToInsert;
                    boardPacket = PacketManager.CreatePacket(BuildBoard());
                    _player1.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    _player2.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 3:
                    board[0, 2] = charToInsert;
                    boardPacket = PacketManager.CreatePacket(BuildBoard());
                    _player1.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    _player2.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 4:
                    board[1, 0] = charToInsert;
                    boardPacket = PacketManager.CreatePacket(BuildBoard());
                    _player1.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    _player2.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 5:
                    board[1, 1] = charToInsert;
                    boardPacket = PacketManager.CreatePacket(BuildBoard());
                    _player1.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    _player2.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 6:
                    board[1, 2] = charToInsert;
                    boardPacket = PacketManager.CreatePacket(BuildBoard());
                    _player1.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    _player2.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 7:
                    board[2, 0] = charToInsert;
                    boardPacket = PacketManager.CreatePacket(BuildBoard());
                    _player1.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    _player2.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 8:
                    board[2, 1] = charToInsert;
                    boardPacket = PacketManager.CreatePacket(BuildBoard());
                    _player1.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    _player2.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 9:
                    board[2, 2] = charToInsert;
                    boardPacket = PacketManager.CreatePacket(BuildBoard());
                    _player1.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    _player2.NetworkStream.Write(boardPacket, 0, boardPacket.Length);
                    break;
                default:
                    _player1.NetworkStream.Write(packet, 0, packet.Length);
                    _player2.NetworkStream.Write(packet, 0, packet.Length);
                    break;
            }
        }
        private bool CheckIfFree(int move)
        {
            byte[] packet = PacketManager.CreatePacket($"{PacketManager.WRONGMOVE}|Wrong move place is already taken");
            if (_player1.Turn == true)
            {
                switch (move)
                {
                    case 1:
                        if (board[0, 0] != '1')
                        {
                            _player1.NetworkStream.Write(packet, 0, packet.Length);
                            return false;
                        }
                        break;
                    case 2:
                        if (board[0, 1] != '2')
                        {
                            _player1.NetworkStream.Write(packet, 0, packet.Length);
                            return false;
                        }
                        break;
                    case 3:
                        if (board[0, 2] != '3')
                        {
                            _player1.NetworkStream.Write(packet, 0, packet.Length);
                            return false;
                        }
                        break;
                    case 4:
                        if (board[1, 0] != '4')
                        {
                            _player1.NetworkStream.Write(packet, 0, packet.Length);
                            return false;
                        }
                        break;
                    case 5:
                        if (board[1, 1] != '5')
                        {
                            _player1.NetworkStream.Write(packet, 0, packet.Length);
                            return false;
                        }
                        break;
                    case 6:
                        if (board[1, 2] != '6')
                        {
                            _player1.NetworkStream.Write(packet, 0, packet.Length);
                            return false;
                        }
                        break;
                    case 7:
                        if (board[2, 0] != '7')
                        {
                            _player1.NetworkStream.Write(packet, 0, packet.Length);
                            return false;
                        }
                        break;
                    case 8:
                        if (board[2, 1] != '8')
                        {
                            _player1.NetworkStream.Write(packet, 0, packet.Length);
                            return false;
                        }
                        break;
                    case 9:
                        if (board[2, 2] != '9')
                        {
                            _player1.NetworkStream.Write(packet, 0, packet.Length);
                            return false;
                        }
                        break;
                    default:

                        byte[] errorMove = PacketManager.CreatePacket($"{PacketManager.WRONGMOVE}|Wrong move");
                        _player1.NetworkStream.Write(errorMove, 0, errorMove.Length);
                        return true;
                }
            }
            else
            {
                if (_player2.Turn == true)
                {
                    switch (move)
                    {
                        case 1:
                            if (board[0, 0] != '1')
                            {
                                _player2.NetworkStream.Write(packet, 0, packet.Length);
                                return false;
                            }
                            break;
                        case 2:
                            if (board[0, 1] != '2')
                            {
                                _player2.NetworkStream.Write(packet, 0, packet.Length);
                                return false;
                            }
                            break;
                        case 3:
                            if (board[0, 2] != '3')
                            {
                                _player2.NetworkStream.Write(packet, 0, packet.Length);
                                return false;
                            }
                            break;
                        case 4:
                            if (board[1, 0] != '4')
                            {
                                _player2.NetworkStream.Write(packet, 0, packet.Length);
                                return false;
                            }
                            break;
                        case 5:
                            if (board[1, 1] != '5')
                            {
                                _player2.NetworkStream.Write(packet, 0, packet.Length);
                                return false;
                            }
                            break;
                        case 6:
                            if (board[1, 2] != '6')
                            {
                                _player2.NetworkStream.Write(packet, 0, packet.Length);
                                return false;
                            }
                            break;
                        case 7:
                            if (board[2, 0] != '7')
                            {
                                _player2.NetworkStream.Write(packet, 0, packet.Length);
                                return false;
                            }
                            break;
                        case 8:
                            if (board[2, 1] != '8')
                            {
                                _player2.NetworkStream.Write(packet, 0, packet.Length);
                                return false;
                            }
                            break;
                        case 9:
                            if (board[2, 2] != '9')
                            {
                                _player2.NetworkStream.Write(packet, 0, packet.Length);
                                return false;
                            }
                            break;
                        default:

                            byte[] errorMove = PacketManager.CreatePacket($"{PacketManager.WRONGMOVE}|Wrong move");
                            _player2.NetworkStream.Write(errorMove, 0, errorMove.Length);
                            return true;
                    }
                }
            }

            return true;
        }
        private void Win()
        {
            string winner = CheckWinner();
            if(winner != null)
            {
                var packet = PacketManager.CreatePacket($"{PacketManager.WIN} | {winner}");
                _player1.NetworkStream.Write(packet, 0, packet.Length);
                _player2.NetworkStream.Write(packet , 0 , packet.Length);
            }
        }

        private int RemoveAllCharFromInt(string str)
        {
            string result = Regex.Replace(str, @"[^\d]", "");
            return int.Parse(result);
        }
        private string BuildBoard()
        {
            return $"BOARD|{board[0,0]}|{board[0, 1]}|{board[0, 2]}|{board[1, 0]}|{board[1, 1]}|{board[1, 2]}|{board[2, 0]}|{board[2, 1]}|{board[2, 2]}";
        }
    }

  
}