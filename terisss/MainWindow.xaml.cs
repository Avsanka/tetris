using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace terisss
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int speed = 400;

        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/BlockEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Blue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Green.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/LightBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Orange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Purple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Red.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Yellow.png", UriKind.Relative))
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/BlockEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Z.png", UriKind.Relative))
        };

        private readonly Image[,] imageControls;
        private readonly int delayDecrement = 25;

        private int maxDelay = 1000;
        private int minDelay = 50;

        private int md;

        private GameState gameState = new GameState();
    
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.PlayGrid);
        }

        private Image[,] SetupGameCanvas(PlayGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }

            return imageControls;
        }

        private void DrawGrid(PlayGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach(Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void DrawGhostBlock(Block block)
        {
            int dropDistance = gameState.BlockDropDistance();
            foreach (Position p in block.TilePositions())
            {
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.3;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.PlayGrid);
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            ScoreText.Text = $"Score: {gameState.Score}";
        }

        private async Task GameLoop()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score / 100 * delayDecrement));
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }

            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";

        }


        //Асинхронная, потому что нам нужно ожидать без блокирования интерфейса
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.Up:
                    gameState.RotateBlockCW();
                    break;
                case Key.Z:
                    gameState.RotateBlockCCW();
                    break;
                case Key.Space:
                    gameState.HardDropBlock();
                    break;
                default:
                    return;
            }
            Draw(gameState);
        }

        private void DrawNextBlock (BlockQueue blockQueue)
        {
            Block next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }
        //асинхронная, т.к. юзаем геймлуп, который асинхронен

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private async void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Hidden;
            minDelay = 50;
            maxDelay = md;
            await GameLoop();
        }

        private async void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Visible;
            minDelay = -1;
            md = maxDelay;
            maxDelay = -1;
            await GameLoop();
        }
    }
}
