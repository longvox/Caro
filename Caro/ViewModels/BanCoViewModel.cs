namespace Caro.ViewModels
{

    using System;
    using Caro.Models;
    using System.Windows;

    internal class BanCoViewModel
    {
        public BanCo CurrentBanCo { get; set; }

        /// <summary>
        /// Khởi tạo thể hiện mới cho class BanCoViewModel
        /// </summary>
        public BanCoViewModel()
        {
            CurrentBanCo = new BanCo();
        }

        /// <summary>
        /// Chơi với người chơi
        /// </summary>
        public void PvP()
        {
            CurrentBanCo.CheDoChoi = CheDoChoi.Player;
            CurrentBanCo.Reset();
        }

        /// <summary>
        /// Máy chơi
        /// </summary>
        public void PvC()
        {
            CurrentBanCo.CheDoChoi = CheDoChoi.Computer;
            CurrentBanCo.Reset();

            if (CurrentBanCo.ActivePlayer == OCo.CellValues.Player2)
            {
                CurrentBanCo.AutoPlay();
            }
        }

        /// <summary>
        /// Kiểm tra có thể đánh tại vị trí đó ko
        /// </summary>
        public bool CanPlayAt(int row, int col)
        {
            if (BanCo.Cells[row, col] == OCo.CellValues.None && CurrentBanCo.Won == OCo.CellValues.None)
            {
                return true;
            }
            return false;
        }

        public bool CanUndo()
        {
            if (CurrentBanCo.CheDoChoi == CheDoChoi.Player)
            {
                return (CurrentBanCo.AI.CacNuocDaDi.Count > 0) ? true : false;
            }
            else
            {
                return (CurrentBanCo.AI.CacNuocDaDi.Count > 1) ? true : false;
            }
        }

        public bool CanRedo()
        {
            return (CurrentBanCo.AI.CacNuocUndo.Count > 0) ? true : false;
        }
    }
}