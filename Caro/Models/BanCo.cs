namespace Caro.Models
{
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Caro.Properties;
    using System.IO;

    #region Enums CheDoChoi
    public enum CheDoChoi
    {
        Player,
        Computer
    }
    #endregion

    public class BanCo
    {
        public int BoardSize { get; set; }  // Kích thước bàn cờ
        public static OCo.CellValues[,] Cells { get; set; } // Các ô cờ
        public OCo.CellValues ActivePlayer { get; set; }    // Người chơi hiện hành
        public OCo.CellValues Won { get; set; }             // Đã thắng

        public bool IsOnline = false;                       // Chơi online?

        public CheDoChoi CheDoChoi { get; set; }            // Chế độ chơi

        // Danh sách các Event
        public event PlayerWinHandler OnPlayerWin;
        public event PlayerAtHandler OnPlayerAt;
        public event UndoHandler OnUndo;
        public event RedoHandler OnRedo;
        public event LoadHandler OnLoad;

        private BackgroundWorkerModel bw;
        public AI AI { get; set; }


        /// <summary>
        /// Khởi tạo một thể hiện mới của class BanCo
        /// </summary>
        public BanCo()
        {
            BoardSize = Settings.Default.BOARD_SIZE;

            ActivePlayer = OCo.CellValues.Player1;
            Won = OCo.CellValues.None;

            AI = new AI();
            bw = new BackgroundWorkerModel(this, AI);
        }

        /// <summary>
        /// Xử lý trên sự kiện OnPlayAt của class SocketModel
        /// </summary>
        void CurrentSocket_OnPlayAt(int row, int col)
        {
            Cells[row, col] = ActivePlayer;

            AI.CacNuocDaDi.Add(new OCo { Col = col, Row = row, OfPlayer = ActivePlayer });

            if (ActivePlayer == OCo.CellValues.Player1)
            {
                ActivePlayer = OCo.CellValues.Player2;
            }
            else
            {
                ActivePlayer = OCo.CellValues.Player1;
            }
        }

        /// <summary>
        /// Xử lý sự kiện OnStart trên class SocketModel
        /// </summary>
        void CurrentSocket_OnStart(OCo.CellValues FirstPlayer)
        {
            ActivePlayer = FirstPlayer;
        }

        /// <summary>
        /// Reset các thuộc tín trong class BanCo
        /// </summary>
        public void Reset()
        {
            Cells = new OCo.CellValues[BoardSize, BoardSize];

            ChangePlayer();

            Won = Won = OCo.CellValues.None;

            AI.Reset();
        }

        /// <summary>
        /// Đánh tại vị trí (row, col)
        /// </summary>
        public void PlayAt(int row, int col)
        {
            Cells[row, col] = ActivePlayer;

            AI.CacNuocDaDi.Add(new OCo { Col = col, Row = row, OfPlayer = ActivePlayer });
            AI.CacNuocUndo.Clear();

            // Check win state
            // Vertiacal check
            if (CountPlayerItem(row, col, 1, 0) >= 5
                || CountPlayerItem(row, col, 0, 1) >= 5
                || CountPlayerItem(row, col, 1, 1) >= 5
                || CountPlayerItem(row, col, 1, -1) >= 5)
            {
                if (OnPlayerWin != null)
                {
                    Won = ActivePlayer;
                    OnPlayerAt(row: row, col: col);
                    OnPlayerWin(player: ActivePlayer);
                }
                return;
            }

            if (OnPlayerAt != null)
            {
                OnPlayerAt(row: row, col: col);
            }

            ChangePlayer();
        }

        /// <summary>
        /// Đổi lượt chơi cho người chơi khác
        /// </summary>
        private void ChangePlayer()
        {
            if (ActivePlayer == OCo.CellValues.Player1)
            {
                ActivePlayer = OCo.CellValues.Player2;
            }
            else
            {
                ActivePlayer = OCo.CellValues.Player1;
            }
        }

        /// <summary>
        /// Kiểm tra nằm trong bàn cờ
        /// </summary>
        private bool IsInBoard(int row, int col)
        {
            return row >= 0 && row < BoardSize && col >= 0 && col < BoardSize;
        }


        /// <summary>
        /// Máy tự chơi
        /// </summary>
        public void AutoPlay()
        {
            bw.backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Đếm số ô đã đánh
        /// </summary>
        private int CountPlayerItem(int row, int col, int drow, int dcol)
        {
            int crow = row + drow;
            int ccol = col + dcol;
            int count = 1;
            while (IsInBoard(crow, ccol) && Cells[crow, ccol] == ActivePlayer)
            {
                count++;
                crow = crow + drow;
                ccol = ccol + dcol;
            }

            crow = row - drow;
            ccol = col - dcol;
            while (IsInBoard(crow, ccol) && Cells[crow, ccol] == ActivePlayer)
            {
                count++;
                crow = crow - drow;
                ccol = ccol - dcol;
            }

            return count;
        }

        #region Undo/Redo
        public bool Undo()
        {
            if (CheDoChoi == Models.CheDoChoi.Computer)
            {
                if (AI.CacNuocDaDi.Count < 2)
                {
                    return false;
                }

                OCo oCo = AI.CacNuocDaDi[AI.CacNuocDaDi.Count - 1];

                AI.CacNuocUndo.Add(oCo);
                AI.CacNuocDaDi.Remove(oCo);
                Cells[oCo.Row, oCo.Col] = OCo.CellValues.None;

                ChangePlayer();

                OnUndo(oCo);
            }

            if (AI.CacNuocDaDi.Count < 1)
            {
                return false;
            }

            OCo oCo1 = AI.CacNuocDaDi[AI.CacNuocDaDi.Count - 1];

            AI.CacNuocUndo.Add(oCo1);
            AI.CacNuocDaDi.Remove(oCo1);
            Cells[oCo1.Row, oCo1.Col] = OCo.CellValues.None;

            ChangePlayer();

            OnUndo(oCo1);

            return true;
        }
        public bool Redo()
        {
            if (CheDoChoi == Models.CheDoChoi.Computer)
            {
                if (AI.CacNuocUndo.Count < 2)
                {
                    return false;
                }

                OCo oCo = AI.CacNuocUndo[AI.CacNuocUndo.Count - 1];
                AI.CacNuocDaDi.Add(oCo);
                Cells[oCo.Row, oCo.Col] = oCo.OfPlayer;
                OnRedo(oCo);
                AI.CacNuocUndo.Remove(oCo);
                ChangePlayer();
            }

            if (AI.CacNuocUndo.Count < 1)
            {
                return false;
            }

            OCo oCo1 = AI.CacNuocUndo[AI.CacNuocUndo.Count - 1];
            AI.CacNuocDaDi.Add(oCo1);
            Cells[oCo1.Row, oCo1.Col] = oCo1.OfPlayer;
            OnRedo(oCo1);
            AI.CacNuocUndo.Remove(oCo1);
            ChangePlayer();

            return true;
        }
        #endregion

        #region Save / Load
        public void Save(string path)
        {
            FileStream f = new FileStream(path, FileMode.Create);
            if (File.Exists(path))
            {
                StreamWriter sw = new StreamWriter(f);
                for (int i = 0; i < BoardSize; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        if (Cells[i, j] == OCo.CellValues.Player1)
                        {
                            sw.Write("1");
                        }
                        else if (Cells[i, j] == OCo.CellValues.Player2)
                        {
                            sw.Write("2");
                        }
                        else
                        {
                            sw.Write("0");
                        }
                    }
                    sw.Write("\n");
                }

                if (ActivePlayer == OCo.CellValues.Player1)
                {
                    sw.Write("1");
                }
                else
                {
                    sw.Write("2");
                }

                sw.Flush();
                sw.Close();
                f.Close();
            }
        }

        public void Load(string path)
        {
            if (File.Exists(path))
            {
                FileStream f = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(f);

                Reset();

                for (int i = 0; i < BoardSize; i++)
                {
                    string Dong = sr.ReadLine();
                    for (int j = 0; j < BoardSize; j++)
                    {
                        char c = Dong[j];
                        Cells[i, j] = (OCo.CellValues)(Convert.ToInt32(c) - 48);

                        if (Cells[i, j] != OCo.CellValues.None)
                        {
                            OCo oCo = new OCo() { Row = i, Col = j, OfPlayer = Cells[i, j] };
                            AI.CacNuocDaDi.Add(oCo);
                            OnLoad(oCo);
                        }
                    }
                }

                ActivePlayer = (OCo.CellValues)(Convert.ToInt32(sr.ReadLine()[0]) - 48);
                sr.Close();
                f.Close();
            }
        }
        #endregion

        public delegate void PlayerWinHandler(OCo.CellValues player);
        public delegate void PlayerAtHandler(int row, int col);
        public delegate void UndoHandler(OCo OCo);
        public delegate void RedoHandler(OCo OCo);
        public delegate void LoadHandler(OCo Oco);
    }
}
