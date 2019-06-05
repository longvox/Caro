namespace Caro.Models
{
    using Caro.Properties;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BangDiem
    {
        private int soDong;   // Số dòng của bàn cờ
        private int soCot;    // Số cột của bàn cờ
        private int[,] diem; // Bảng lưu điểm 

        public int[,] Diem
        {
            get { return diem; }
            set { diem = value; }
        }

        /// <summary>
        /// Khởi tạo một thể hiện mới cho class BanCo
        /// </summary>
        public BangDiem()
        {
            soDong = soCot = Settings.Default.BOARD_SIZE;
            diem = new int[soDong + 2, soCot + 2];
        }

        /// <summary>
        /// Reset bảng điểm
        /// </summary>
        public void ResetDiem()
        {
            for (int i = 0; i < soDong + 2; i++)
            {
                for ( int j = 0; j < soCot + 2; j++)
                {
                    Diem[i, j] = 0;
                }
            }
        }

        public OCo MaxPos()
        {
            long max = 0;

            OCo oco = new OCo();
            for (int i = 0; i < soDong; i++)
            {
                for (int j = 0; j < soCot; j++)
                {
                    if (Diem[i, j] > max)
                    {
                        oco.Row = i;
                        oco.Col = j;
                        max = Diem[i, j];
                    }
                }
            }

            return oco;
        }
    }
}
