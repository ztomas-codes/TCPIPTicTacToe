using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Server.Objects;

namespace Server.Manager
{
    public class GameManager
    {
        
        private Player _player1;
        private Player _player2;
        private char[,] board = new char[,] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        NetworkStream StreamPlayer1 {get; set;}
        NetworkStream StreamPlayer2 {get; set;}
        List<string> PossiblePackets = new List<string>();
        private bool _isPlayer1Turn { get; set; } = true;
        private bool _isPlayer2Turn { get; set; } = false;
        private List<TcpClient> players = new List<TcpClient>();


        public GameManager(Player player1 , Player player2, NetworkStream str , NetworkStream str2)
        {
            _player1 = player1;
            _player2 = player2;
            StreamPlayer1 = str;
            StreamPlayer2 = str2;
            PossiblePackets.Add(PacketManager.WIN);
            PossiblePackets.Add(PacketManager.MOVE);
            PossiblePackets.Add(PacketManager.TURN);
            PossiblePackets.Add(PacketManager.PLAYERTURN);
            PossiblePackets.Add(PacketManager.NOTYOURTURN);
            players.Add(player1.Client);
            players.Add(player2.Client);
        }
        
        public void StartGame()
        {
            byte[] packet = PacketManager.CreatePacket($"{PacketManager.STARTGAME}|game started {_player1.Name} vs {_player2.Name}");
                _player1.Char = 'O';
                _player2.Char = 'X';
                StreamPlayer1.Write(packet, 0, packet.Length);
                StreamPlayer1.Flush();
                StreamPlayer2.Write(packet, 0, packet.Length);
                StreamPlayer2.Flush();
                _isPlayer1Turn = true;
                _isPlayer2Turn = false;
                while (true)
                {
                    byte[] bytes = new byte[1024];
                    StreamPlayer1.Read(bytes, 0, bytes.Length);
                    StreamPlayer2.Read(bytes, 0, bytes.Length);
                   string mess = PacketManager.GetPacket(bytes);
                   if (!CheckIfBothConnected())
                   {
                       packet = PacketManager.CreatePacket($"{PacketManager.DISCONNECT}|{_player1.Name} disconnected");
                       StreamPlayer1.Write(packet , 0 , packet.Length);
                       StreamPlayer1.Dispose();
                       StreamPlayer2.Write(packet , 0 , packet.Length);
                       StreamPlayer2.Dispose();
                       break;
                   }
                    SortPackets(bytes);
                   int move = RemoveAllCharFromInt(mess);
                   CheckIfFree(5 , _player1);
                  
                   
                }
                
                
        }
        private bool CheckIfBothConnected()
        {
            return _player1.Client.Connected && _player2.Client.Connected;

        }
        private string CheckWinner()
        {
            return _player1.Name;
        }
        private void SortPackets(byte[] packet)
        {
            string packetString = Encoding.ASCII.GetString(packet);
            foreach (var _packet in PossiblePackets)
            {
                if (!packetString.StartsWith(_packet))
                {
                    byte[] error = Encoding.ASCII.GetBytes($"{PacketManager.DISCONNECT}|Disconnected because of wrong packet");
                    StreamPlayer1.Write(error , 0 , error.Length);
                    StreamPlayer1.Flush();
                    StreamPlayer1.Dispose();
                    StreamPlayer2.Write(error , 0 , error.Length);
                    StreamPlayer2.Flush();
                    StreamPlayer2.Dispose();
                    
                }
            }
        }
        private void InsertIntoBoard(int move, char charToInsert)
        {
            byte[] packet = Encoding.Default.GetBytes($"{PacketManager.WRONGMOVE}|Wrong move");
            switch (move)
            {
                case 1:
                    board[0, 0] = charToInsert;
                    break;
                case 2:
                    board[0, 1] = charToInsert;
                    break;
                case 3:
                    board[0, 2] = charToInsert;
                    break;
                case 4:
                    board[1, 0] = charToInsert;
                    break;
                case 5:
                    board[1, 1] = charToInsert;
                    break;
                case 6:
                    board[1, 2] = charToInsert;
                    break;
                case 7:
                    board[2, 0] = charToInsert;
                    break;
                case 8:
                    board[2, 1] = charToInsert;
                    break;
                case 9:
                    board[2, 2] = charToInsert;
                    break;
                default:
                    StreamPlayer1.Write(packet, 0, packet.Length);
                    StreamPlayer2.Write(packet, 0, packet.Length);
                    break;
            }
        }
        private bool CheckIfFree(int move, Player pl)
        {
            byte[] packet = Encoding.Default.GetBytes($"{PacketManager.WRONGMOVE}|Wrong move place is already taken");
            switch (move)
            {
                case 1:
                    if (board[0,0] != ' ')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 2:
                    if (board[0,1] != ' ')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 3:
                    if (board[0,2] != ' ')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }     
                    break;
                case 4:
                    if (board[1,0] != ' ')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 5:
                    if (board[1,1] != ' ')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 6:
                    if(board[1,2] != ' ')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 7:
                    if (board[2,0] != ' ')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 8:
                    if (board[2,1] != ' ')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 9:
                    if (board[2,2] != ' ')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                default:
                    //TODO: Send error packet if input is wrong
                     byte[] errorMove = Encoding.Default.GetBytes($"{PacketManager.WRONGMOVE}|Wrong move");
                    StreamPlayer1.Write(errorMove, 0, errorMove.Length);
                    return true;
            }

            return true;
        }

        private int RemoveAllCharFromInt(string str)
        {
            string result = Regex.Replace(str, @"[^\d]", "");
            return int.Parse(result);
        }
    }

  
}