using Common;
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

namespace SnakeWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Games.xaml
    /// </summary>
    public partial class Game : Page
    {
        public int StepCadr = 0;
        public Game()
        {
            InitializeComponent();
        }

        public void CreateUI()
        {
            Dispatcher.Invoke(() =>
            {
                if (StepCadr == 0) StepCadr = 1;
                else StepCadr = 0;
                Canvas.Children.Clear();
                for (int iPoint = MainWindow.mainWindow.viewModelGames.SnakesPlayers.Points.Count - 1;
                iPoint >= 0;
                iPoint--)
                {
                    Snakes.Point SnakePoint = MainWindow.mainWindow.viewModelGames.SnakesPlayers.Points[iPoint];
                    if(iPoint != 0)
                    {
                        Snakes.Point NextPoint = MainWindow.mainWindow.viewModelGames.SnakesPlayers.Points[iPoint - 1];
                        if (SnakePoint.X > NextPoint.X || SnakePoint.X < NextPoint.X)
                        {
                            if (iPoint % 2 == 0)
                            {
                                if (StepCadr % 2 == 0) SnakePoint.Y -= 1;
                                else SnakePoint.Y += 1;
                            }
                            else
                            {
                                if(StepCadr % 2 ==0) SnakePoint.Y += 1;
                                else SnakePoint.Y -= 1;
                            }
                        }
                        else if (SnakePoint.Y > NextPoint.Y || SnakePoint.Y > NextPoint.Y)
                        {
                            if (iPoint % 2 == 0)
                            {
                                if (StepCadr % 2 == 0) SnakePoint.X -= 1;
                                else SnakePoint.X += 1;
                            }
                            else
                            {
                                if (StepCadr % 2 == 0) SnakePoint.X += 1;
                                else SnakePoint.X -= 1;
                            }
                        }
                    }
                    Brush Color;
                    if (iPoint == 0)
                        Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 127, 14));
                    else
                        Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 198, 19));
                    Ellipse ellipse = new Ellipse()
                    {
                        Width = 20,
                        Height = 20,
                        Margin = new System.Windows.Thickness(SnakePoint.X - 10, SnakePoint.Y - 10, 0, 0),
                        Fill = Color,
                        Stroke = Brushes.Black
                    };
                    Canvas.Children.Add(ellipse);
                }
                Ellipse apple = new Ellipse()
                {
                    Width = 40,
                    Height = 40,
                    Margin = new System.Windows.Thickness(
                        MainWindow.mainWindow.viewModelGames.Points.X - 20,
                        MainWindow.mainWindow.viewModelGames.Points.Y - 20, 0, 0),
                    Fill = Brushes.Red,
                    Stroke = Brushes.Black
                };
                Canvas.Children.Add(apple);
            });
        }
    }
}
