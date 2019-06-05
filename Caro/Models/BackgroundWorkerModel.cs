namespace Caro.Models
{
    using System;
    using System.ComponentModel;

    internal class BackgroundWorkerModel
    {
        public BackgroundWorker backgroundWorker { get; set; }
        private BanCo BanCo;
        private AI AI;

        /// <summary>
        /// Khởi tạo một thể hiện mới cho class BackgroundWorker
        /// </summary>
        public BackgroundWorkerModel(BanCo banCo, AI aI)
        {
            BanCo = banCo;
            AI = aI;

            if (backgroundWorker == null)
            {
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerSupportsCancellation = true;

                backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
                backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            }
        }

        #region BackgroundWorker Event

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OCo OCo = (OCo)e.Result;
            BanCo.PlayAt(OCo.Row, OCo.Col);

        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = AI.KhoiDongComputer();
        }

        #endregion
    }
}
