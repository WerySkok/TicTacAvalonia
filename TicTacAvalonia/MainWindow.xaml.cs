using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.X11;
using System;
using System.Reflection;

namespace TicTacAvalonia
{
    public class MainWindow : Window
    {
        int[,] gameArea = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        bool whoPlays = true; // true = first player, false = second player
        bool gameEnded = false;

        private TextBlock StatusBar;
        private Button Button00;
        private Button Button01;
        private Button Button02;

        private Button Button10;
        private Button Button11;
        private Button Button12;

        private Button Button20;
        private Button Button21;
        private Button Button22;

        private MenuItem NewGameButton;
        private MenuItem ExitButton;
        private MenuItem AboutButton;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            StatusBar = this.FindControl<TextBlock>("StatusBar");

            Button00 = this.FindControl<Button>("Button00");
            Button01 = this.FindControl<Button>("Button01");
            Button02 = this.FindControl<Button>("Button02");

            Button10 = this.FindControl<Button>("Button10");
            Button11 = this.FindControl<Button>("Button11");
            Button12 = this.FindControl<Button>("Button12");

            Button20 = this.FindControl<Button>("Button20");
            Button21 = this.FindControl<Button>("Button21");
            Button22 = this.FindControl<Button>("Button22");

            NewGameButton = this.FindControl<MenuItem>("NewGameButton");
            ExitButton = this.FindControl<MenuItem>("ExitButton");
            AboutButton = this.FindControl<MenuItem>("AboutButton");
        }

        void SetMark(int x, int y)
        {
            if(gameArea[x, y] == 0 && gameEnded == false)
            {
                if(whoPlays)
                {
                    gameArea[x, y] = 1;
                    Button clickedButton = this.FindControl<Button>("Button" + x + y);
                    clickedButton.Content = new Image { Source = new Bitmap(AvaloniaLocator.Current.GetService<IAssetLoader>().Open(new Uri($" avares://TicTacAvalonia/Assets/x.bmp"))) };
                    SetStatusBarText("O player's turn.");
                    whoPlays = false;
                }
                else
                {
                    gameArea[x, y] = 2;
                    Button clickedButton = this.FindControl<Button>("Button" + x + y);
                    clickedButton.Content = new Image { Source = new Bitmap(AvaloniaLocator.Current.GetService<IAssetLoader>().Open(new Uri($" avares://TicTacAvalonia/Assets/o.bmp"))) };
                    SetStatusBarText("X player's turn.");
                    whoPlays = true;
                }
                CheckForWinner();
            }
        }

        void CheckForWinner()
        {
            for(int i = 1; i <= 2; i++)
            {
                if(gameArea[0, 0] == i && gameArea[0, 1] == i && gameArea[0, 2] == i ||
                    gameArea[1, 0] == i && gameArea[1, 1] == i && gameArea[1, 2] == i ||
                    gameArea[2, 0] == i && gameArea[2, 1] == i && gameArea[2, 2] == i ||
                    gameArea[0, 0] == i && gameArea[1, 1] == i && gameArea[2, 2] == i ||
                    gameArea[0, 2] == i && gameArea[1, 1] == i && gameArea[2, 0] == i ||
                    gameArea[0, 0] == i && gameArea[1, 0] == i && gameArea[2, 0] == i ||
                    gameArea[0, 1] == i && gameArea[1, 1] == i && gameArea[2, 1] == i ||
                    gameArea[0, 2] == i && gameArea[1, 2] == i && gameArea[2, 2] == i)
                {
                    string winner;
                    if(i == 1)
                    {
                        winner = "X is the winner!";
                    }
                    else
                    {
                        winner = "O is the winner!";
                    }
                    SetStatusBarText("Start a new game.");
                    var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Tic-Tac-Toe", winner);
                    messageBoxStandardWindow.Show();
                    gameEnded = true;
                }
            }
        }

        void SetStatusBarText(string text)
        {
            StatusBar.Text = text;
        }

        void ClearGameArea()
        {
            gameArea = new[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            gameEnded = false;
            for(int i = 0; i <= 2; i++)
            {
                for(int a = 0; a <= 2; a++)
                {
                    Button ChosenButton = this.FindControl<Button>("Button" + a + i);
                    ChosenButton.Content = null;
                }
            }
            if(whoPlays)
                SetStatusBarText("X player's turn.");
            else
                SetStatusBarText("O player's turn.");
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            ClearGameArea();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog(this);
        }

        private void Button00_Click(object sender, RoutedEventArgs e)
        {
            SetMark(0, 0);
        }

        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            SetMark(0, 1);
        }

        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            SetMark(0, 2);
        }

        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            SetMark(1, 0);
        }

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            SetMark(1, 1);
        }

        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            SetMark(1, 2);
        }

        private void Button20_Click(object sender, RoutedEventArgs e)
        {
            SetMark(2, 0);
        }

        private void Button21_Click(object sender, RoutedEventArgs e)
        {
            SetMark(2, 1);
        }

        private void Button22_Click(object sender, RoutedEventArgs e)
        {
            SetMark(2, 2);
        }
    }
}
