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
        private char[,] board = new char[,] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        NetworkStream StreamPlayer1 { get; set; }
        NetworkStream StreamPlayer2 { get; set; }
        List<string> PossiblePackets = new List<string>();
        private bool _isPlayer1Turn { get; set; } = true;
        private bool _isPlayer2Turn { get; set; } = false;
        private byte[] Player1Packet { get; set; }
        private byte[] Player2PAcket { get; set; }
        private List<TcpClient> players = new List<TcpClient>();


        public GameManager(Player player1, Player player2, NetworkStream str, NetworkStream str2)
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
            _player1.Turn = true;
            _player2.Turn = false;
            var board = Encoding.Default.GetBytes(BuildBoard());
            StreamPlayer1.Write(board , 0 , board.Length);
            StreamPlayer2.Write(board , 0 , board.Length);
       


        }
        private void Listen()
        {
            while(true)
            {
                while (true)
                {
                    byte[] bytes1 = new byte[1024];
                    byte[] bytes2 = new byte[1024];
                    StreamPlayer1.Read(bytes1, 0, bytes1.Length);
                    StreamPlayer2.Read(bytes2, 0, bytes2.Length);
                    
                    if (!CheckIfBothConnected())
                    {
                         var packet = PacketManager.CreatePacket($"{PacketManager.DISCONNECT}|{_player1.Name} disconnected");
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        StreamPlayer1.Dispose();
                        StreamPlayer2.Write(packet, 0, packet.Length);
                        StreamPlayer2.Dispose();
                        break;
                    }
                    SortPackets(bytes1 , _player1 , _player2);
                    SortPackets(bytes2, _player2, _player1);
                    
                }
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
        private void SortPackets(byte[] packet , Player pl , Player pl1)
        {
            string packetString = Encoding.ASCII.GetString(packet);
            foreach (var _packet in PossiblePackets)
            {
                if (!packetString.StartsWith(_packet))
                {
                    byte[] error = Encoding.ASCII.GetBytes($"{PacketManager.DISCONNECT}|Disconnected because of wrong packet");
                    StreamPlayer1.Write(error, 0, error.Length);
                    StreamPlayer1.Flush();
                    StreamPlayer1.Dispose();
                    StreamPlayer2.Write(error, 0, error.Length);
                    StreamPlayer2.Flush();
                    StreamPlayer2.Dispose();

                }
                else
                {
                    
                   if(packetString.StartsWith(PacketManager.MOVE) && pl.Turn == true)
                   {
                        int move = RemoveAllCharFromInt(packetString);
                        CheckIfFree(move, pl);
                        InsertIntoBoard(move , pl.Char);
                        pl.Turn = false;
                        pl1.Turn = true;
                   }
                   
                }
            }
        }
        private void InsertIntoBoard(int move, char charToInsert)
        {
            byte[] packet = Encoding.Default.GetBytes($"{PacketManager.WRONGMOVE}|Wrong move");
            var boardPacket = Encoding.Default.GetBytes(BuildBoard());
            switch (move)
            {
                case 1:
                    board[0, 0] = charToInsert;
                    StreamPlayer1.Write(boardPacket, 0, boardPacket.Length);
                    StreamPlayer2.Write(boardPacket , 0 , boardPacket.Length);
                    break;
                case 2:
                    board[0, 1] = charToInsert;
                    StreamPlayer1.Write(boardPacket, 0, boardPacket.Length);
                    StreamPlayer2.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 3:
                    board[0, 2] = charToInsert;
                    StreamPlayer1.Write(boardPacket, 0, boardPacket.Length);
                    StreamPlayer2.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 4:
                    board[1, 0] = charToInsert;
                    StreamPlayer1.Write(boardPacket, 0, boardPacket.Length);
                    StreamPlayer2.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 5:
                    board[1, 1] = charToInsert;
                    StreamPlayer1.Write(boardPacket, 0, boardPacket.Length);
                    StreamPlayer2.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 6:
                    board[1, 2] = charToInsert;
                    StreamPlayer1.Write(boardPacket, 0, boardPacket.Length);
                    StreamPlayer2.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 7:
                    board[2, 0] = charToInsert;
                    StreamPlayer1.Write(boardPacket, 0, boardPacket.Length);
                    StreamPlayer2.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 8:
                    board[2, 1] = charToInsert;
                    StreamPlayer1.Write(boardPacket, 0, boardPacket.Length);
                    StreamPlayer2.Write(boardPacket, 0, boardPacket.Length);
                    break;
                case 9:
                    board[2, 2] = charToInsert;
                    StreamPlayer1.Write(boardPacket, 0, boardPacket.Length);
                    StreamPlayer2.Write(boardPacket, 0, boardPacket.Length);
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
                    if (board[0, 0] != '1')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 2:
                    if (board[0, 1] != '2')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 3:
                    if (board[0, 2] != '3')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 4:
                    if (board[1, 0] != '4')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 5:
                    if (board[1, 1] != '5')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 6:
                    if (board[1, 2] != '6')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 7:
                    if (board[2, 0] != '7')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 8:
                    if (board[2, 1] != '8')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                case 9:
                    if (board[2, 2] != '9')
                    {
                        StreamPlayer1.Write(packet, 0, packet.Length);
                        return false;
                    }
                    break;
                default:
                    
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
        private string BuildBoard()
        {
            string Board = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Board += board[i, j] + " | ";
                }
                Board += "\n";
               
                    
            }
            return Board;
        }
    }

  
}