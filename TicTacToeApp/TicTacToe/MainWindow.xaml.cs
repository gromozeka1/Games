using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<Player, ImageSource> imageSources = new()
        {
            { Player.X, new BitmapImage(new Uri("pack://application:,,,/Assets/X15.png"))},
            { Player.O, new BitmapImage(new Uri("pack://application:,,,/Assets/O15.png"))},
        };

        private readonly Dictionary<Player, ObjectAnimationUsingKeyFrames> animations = new()
        {
            {Player.X, new ObjectAnimationUsingKeyFrames() },
            {Player.O, new ObjectAnimationUsingKeyFrames() }
        };

        private readonly DoubleAnimation fadeOutAnimation = new()
        {
            Duration = TimeSpan.FromSeconds(.5),
            From = 1,
            To = 0,
        };

        private readonly DoubleAnimation fadeInAnimation = new()
        {
            Duration = TimeSpan.FromSeconds(.5),
            From = 0,
            To = 1,
        };

        private readonly Image[,] imageControls = new Image[3, 3];
        private readonly GameState gameState = new();

        public MainWindow()
        {
            InitializeComponent();
            SetupGameGrid();
            SetupAnimations();

            gameState.MoveMade += OnMoveMade;
            gameState.GameEnded += OnGameEnded;
            gameState.GameRestarted += OnGameRestarted;
        }

        private void SetupGameGrid()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    var imageControl = new Image();
                    GameGrid.Children.Add(imageControl);
                    imageControls[r,c] = imageControl;
                }
            }
        }

        private void SetupAnimations()
        {
            TimeSpan duration = TimeSpan.FromSeconds(.25);
            animations[Player.X].Duration = duration;
            animations[Player.O].Duration = duration;

            for (int i = 0; i < 16; i++)
            {
                animations[Player.X].KeyFrames.Add(GetKeyFrame(Player.X, i));
                animations[Player.O].KeyFrames.Add(GetKeyFrame(Player.O, i));
            }
        }

        private static ObjectKeyFrame GetKeyFrame(Player player, int i)
        {
            
            Uri uri = new Uri($"pack://application:,,,/Assets/{player}{i}.png");
            BitmapImage img = new BitmapImage(uri);
            DiscreteObjectKeyFrame keyFrame = new DiscreteObjectKeyFrame(img);
            return keyFrame;
        }

        private async Task FadeOut(UIElement uiElement)
        {
            uiElement.BeginAnimation(OpacityProperty, fadeOutAnimation);
            await Task.Delay(fadeOutAnimation.Duration.TimeSpan);
            uiElement.Visibility = Visibility.Hidden;
        }

        private async Task FadeIn(UIElement uiElement)
        {
            uiElement.Visibility = Visibility.Visible;
            uiElement.BeginAnimation(OpacityProperty, fadeInAnimation);
            await Task.Delay(fadeInAnimation.Duration.TimeSpan);
        }

        private async Task TransitionToEndScreen(string text, ImageSource? winnerImage)
        {
            await Task.WhenAll(FadeOut(TurnPanel), FadeOut(GameCanvas));
            ResultText.Text = text;
            WinnerImage.Source = winnerImage;
            await FadeIn(EndScreen);
        }

        private async Task TransitionToGameScreen()
        {
            await FadeOut(EndScreen);
            Line.Visibility = Visibility.Hidden;
            await Task.WhenAll(FadeIn(TurnPanel), FadeIn(GameCanvas));
        }

        private (Point, Point) FindLinePoints(WinInfo? winInfo)
        {
            winInfo = winInfo ?? throw new ArgumentNullException(nameof(winInfo));
            double squareSize = GameGrid.Width / 3;
            double margin = squareSize / 2;
            double fluentCoordinate = (winInfo.Number * squareSize) + margin;

            return winInfo.Type switch
            {
                WinType.Row => (new Point(0, fluentCoordinate), new Point(GameGrid.Width, fluentCoordinate)),
                WinType.Column => (new Point(fluentCoordinate, 0), new Point(fluentCoordinate, GameGrid.Height)),
                WinType.MainDiagonal => (new Point(0, 0), new Point(GameGrid.Width, GameGrid.Height)),
                WinType.AntiDiagonal => (new Point(GameGrid.Width, 0), new Point(0, GameGrid.Height)),
                _ => throw new NotSupportedException(nameof(winInfo)),
            };
        }

        private async Task ShowLine(WinInfo? winInfo)
        {
            TimeSpan duration = TimeSpan.FromSeconds(0.25);
            (Point start, Point end) = FindLinePoints(winInfo);

            Line.X1 = start.X;
            Line.Y1 = start.Y;

            var x2animation = new DoubleAnimation
            {
                Duration = duration,
                From = start.X,
                To = end.X,
            };

            var y2animation = new DoubleAnimation
            {
                Duration = duration,
                From = start.Y,
                To = end.Y,
            };

            Line.Visibility = Visibility.Visible;
            Line.BeginAnimation(Line.X2Property, x2animation);
            Line.BeginAnimation(Line.Y2Property, y2animation);
            await Task.Delay(duration);
        }

        private void OnMoveMade(Position position)
        {
            Player player = gameState.GameGrid[position.X, position.Y];
            imageControls[position.X, position.Y].BeginAnimation(Image.SourceProperty, animations[player]);
            PlayerImage.Source = imageSources[gameState.CurrentPlayer];
        }

        private async void OnGameEnded(GameResult? gameResult)
        {
            gameResult = gameResult ?? throw new ArgumentNullException(nameof(gameResult));
            await Task.Delay(1000);

            if (gameResult.Winner == Player.None)
            {
                await TransitionToEndScreen("It's a tie!", null);
            }
            else
            {
                await ShowLine(gameResult.WinInfo);
                await Task.Delay(1000);
                await TransitionToEndScreen("Winner: ", imageSources[gameResult.Winner]);
            }
        }

        private async void OnGameRestarted()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    imageControls[r, c].BeginAnimation(Image.SourceProperty, null);
                    imageControls[r, c].Source = null;
                }
            }

            PlayerImage.Source = imageSources[gameState.CurrentPlayer];
            await TransitionToGameScreen();
        }

        private void GameGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double squareSize = GameGrid.Width / 3;
            Point clickPosition = e.GetPosition(GameGrid);
            Position position = new Position
            {
                X = (int)(clickPosition.Y / squareSize),
                Y = (int)(clickPosition.X / squareSize),
            };
            gameState.MakeMove(position);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameState.GameOver)
            {
                gameState.Reset();
            }
        }
    }
}
