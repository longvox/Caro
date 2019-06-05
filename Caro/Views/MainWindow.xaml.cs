using Caro.Properties;
using Caro.ViewModels;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Caro.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BanCoViewModel BanCoViewModel;

        public MainWindow()
        {
            InitializeComponent();

            SetButtonClick();

            BanCoViewModel = new BanCoViewModel();
            BanCoViewModel.CurrentBanCo.OnPlayerAt += CurrentBanCo_OnPlayerAt;
            BanCoViewModel.CurrentBanCo.OnPlayerWin += CurrentBanCo_OnPlayerWin;
            BanCoViewModel.CurrentBanCo.OnUndo += CurrentBanCo_OnUndo;
            BanCoViewModel.CurrentBanCo.OnRedo += CurrentBanCo_OnRedo;

            BanCoViewModel.PvC();
            DrawBoard(Settings.Default.BOARD_SIZE);
        }

        private void DrawBoard(int n)
        {
            board.Children.Clear();
            LinearGradientBrush even = new LinearGradientBrush();
            even.StartPoint = new Point(0.5, 0);
            even.EndPoint = new Point(0.5, 1);
            even.GradientStops.Add(new GradientStop(Color.FromRgb(255, 253, 253), 0));
            even.GradientStops.Add(new GradientStop(Color.FromRgb(255, 238, 238), 1));

            LinearGradientBrush odd = new LinearGradientBrush();
            odd.StartPoint = new Point(0.5, 0);
            odd.EndPoint = new Point(0.5, 1);
            odd.GradientStops.Add(new GradientStop(Colors.Gainsboro, 0.007));
            odd.GradientStops.Add(new GradientStop(Color.FromRgb(204, 204, 204), 1));

            for (int i = 0; i < n; i++)
            {
                board.ColumnDefinitions.Add(new ColumnDefinition());
                board.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Button but = new Button();
                    but.MinWidth = 580 / n;
                    but.MinHeight = 580 / n;
                    but.VerticalAlignment = VerticalAlignment.Stretch;
                    but.HorizontalAlignment = HorizontalAlignment.Stretch;
                    but.SetValue(Grid.RowProperty, i);
                    but.SetValue(Grid.ColumnProperty, j);
                    but.Click += Cell_Click;

                    if ((i + j) % 2 == 0)
                        but.Background = even;
                    else
                        but.Background = odd;
                    board.Children.Add(but);
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện bắt đầu
        /// </summary>
        void CurrentSocket_OnStart(Models.OCo.CellValues FirstPlayer)
        {
            if (BanCoViewModel.CurrentBanCo.CheDoChoi == Caro.Models.CheDoChoi.Computer && FirstPlayer == Models.OCo.CellValues.Player2)
                BanCoViewModel.CurrentBanCo.AutoPlay();
        }

        /// <summary>
        /// Xử lý sự kiện đánh tại một ô nào đó từ Server gửi về
        /// </summary>
        void CurrentSocket_OnPlayAt(int row, int col)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                SetButtonContent(board.Children[row * Settings.Default.BOARD_SIZE + col] as Button);

                if (BanCoViewModel.CurrentBanCo.CheDoChoi == Caro.Models.CheDoChoi.Computer &&
                    BanCoViewModel.CurrentBanCo.ActivePlayer == Models.OCo.CellValues.Player2 &&
                    BanCoViewModel.CurrentBanCo.Won == Models.OCo.CellValues.None)
                {
                    BanCoViewModel.CurrentBanCo.AutoPlay();
                }
            }));
        }

        /// <summary>
        /// Xuất kết quả
        /// </summary>
        void CurrentBanCo_OnPlayerWin(Models.OCo.CellValues player)
        {
            if (BanCoViewModel.CurrentBanCo.CheDoChoi == Caro.Models.CheDoChoi.Computer)
            {
                if (player.ToString() == "Player2")
                    MessageBox.Show("Computer win!", "Result", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                else
                    MessageBox.Show("Human win!", "Result", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show(player.ToString() + " win!", "Result", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Xử lý sự kiện Đánh tại một ô cờ tại vị trí (row, col)
        /// </summary>
        void CurrentBanCo_OnPlayerAt(int row, int col)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                SetButtonContent(board.Children[row * Settings.Default.BOARD_SIZE + col] as Button);

                if (BanCoViewModel.CurrentBanCo.CheDoChoi == Caro.Models.CheDoChoi.Computer &&
                    BanCoViewModel.CurrentBanCo.ActivePlayer == Models.OCo.CellValues.Player1 &&
                    BanCoViewModel.CurrentBanCo.Won == Models.OCo.CellValues.None)
                {
                    BanCoViewModel.CurrentBanCo.AutoPlay();
                }
            }));
        }

        /// <summary>
        /// Thêm sự kiện click cho các button Cell
        /// </summary>
        private void SetButtonClick()
        {
            foreach (Control control in board.Children)
            {
                Button cell = (Button)control;
                cell.Click += this.Cell_Click;
            }
        }

        /// <summary>
        /// Xử lý sự kiện Cell click
        /// </summary>
        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            Button cell = (Button)sender;

            if (BanCoViewModel.CanPlayAt(Grid.GetRow(cell), Grid.GetColumn(cell)))
                // Đánh vào ô cờ đang xét
                BanCoViewModel.CurrentBanCo.PlayAt(Grid.GetRow(cell), Grid.GetColumn(cell));
        }

        /// <summary>
        /// Thêm content vào button cell đang xét
        /// </summary>
        private void SetButtonContent(Button cell)
        {
            if (BanCoViewModel.CurrentBanCo.ActivePlayer == Models.OCo.CellValues.Player1)
                cell.Content = CreateEllipse(Colors.Black);
            else
                cell.Content = CreateEllipse(Colors.White);
        }

        /// <summary>
        /// Tạo hình ellipse
        /// </summary>
        private Ellipse CreateEllipse(Color color)
        {
            Ellipse ellipse = new Ellipse();

            ellipse.Width = 30;
            ellipse.Height = 30;
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            ellipse.Fill = new SolidColorBrush(color);

            return ellipse;
        }

        /// <summary>
        /// Reset bàn cờ
        /// </summary>
        private void ResetBoard()
        {
            foreach (Button cell in board.Children)
            {
                cell.Content = null;
            }
        }

        /// <summary>
        /// Xử lý sự kiện nhấn các phím tắt
        /// </summary>
        private void CaroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.Z)
                Undo();
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.Y)
                Redo();
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.N)
                NewGame();
        }

        #region NewGame
        private void NewGame()
        {
            if (BanCoViewModel.CurrentBanCo.CheDoChoi == Models.CheDoChoi.Player)
                PvPmode();
            else
                PvCmode();
        }

        private void menuNewGame_Click(object sender, RoutedEventArgs e) => NewGame();
        #endregion

        /// <summary>
        /// Đóng chương trình
        /// </summary>
        private void menuExit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        #region Set mode
        private void menuPvP_Click(object sender, RoutedEventArgs e) => PvPmode();

        private void PvPmode()
        {
            ResetBoard();
            BanCoViewModel.PvP();
        }

        private void menuPvC_Click(object sender, RoutedEventArgs e) => PvCmode();

        private void PvCmode()
        {
            ResetBoard();
            BanCoViewModel.PvC();
        }

        private void btnPvP_Click(object sender, RoutedEventArgs e) => PvPmode();

        private void btnPvC_Click(object sender, RoutedEventArgs e) => PvCmode();
        #endregion

        #region Undo/Redo
        private void menuUndo_Click(object sender, RoutedEventArgs e) => Undo();

        private void Undo() => BanCoViewModel.CurrentBanCo.Undo();

        private void menuRedo_Click(object sender, RoutedEventArgs e) => Redo();

        private void Redo() => BanCoViewModel.CurrentBanCo.Redo();

        void CurrentBanCo_OnRedo(Models.OCo OCo) =>
            SetButtonContent(board.Children[OCo.Row * Settings.Default.BOARD_SIZE + OCo.Col] as Button);

        void CurrentBanCo_OnUndo(Models.OCo OCo) =>
            (board.Children[OCo.Row * Settings.Default.BOARD_SIZE + OCo.Col] as Button).Content = null;

        private void menuEdit_MouseMove(object sender, MouseEventArgs e)
        {
            menuUndo.IsEnabled = BanCoViewModel.CanUndo(); // (true / false)
            menuRedo.IsEnabled = BanCoViewModel.CanRedo(); // (true / false)
        }
        #endregion
    }
}

