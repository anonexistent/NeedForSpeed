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
using System.Windows.Shapes;

namespace NeedForSpeed
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //double canvasWidth = ((Canvas)point.Parent).ActualWidth;
            //double pointPosition = Canvas.GetLeft(point);
            //double pointPosition2 = Canvas.GetLeft(point2);


            //if ((pointPosition + point.Width + 20 > canvasWidth) & (pointPosition2 + point.Width + 20 > canvasWidth))
            //{
            //    Canvas.SetLeft(point, 0);
            //    Canvas.SetLeft(point2, 0);

            //}
            //else
            //{
            //    if (pointPosition2 + point.Width + 20 > canvasWidth / 2)
            //    {
            //        MessageBox.Show("OH NO GAME OVER FOR PLAYER 1");
            //    }
            //    Canvas.SetLeft(point, pointPosition + 20);
            //    Canvas.SetLeft(point2, pointPosition + 20);
            //}

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                Ellipse racer = (Ellipse)FindName($"racer{i + 1}");
                double canvasWidth = ((Canvas)racer.Parent).ActualWidth;
                double racerPosition = Canvas.GetLeft(racer);

                if (random.Next(2) == 0)
                {
                    Canvas.SetLeft(racer, racerPosition);
                }
                else
                {
                    Canvas.SetLeft(racer, canvasWidth);
                }
            }
        }

        private Dictionary<int, Ellipse> racers = new Dictionary<int, Ellipse>();
        private Random uuu = new Random();

        private void RaceButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем гонщиков
            CreateRacers();

            // Запускаем гонку
            StartRace();
        }

        private void CreateRacers()
        {
            racers.Clear();

            foreach (var item in RaceTrackCanvas.Children)
            {
                try
                {
                    RaceTrackCanvas.Children.Remove(((Ellipse)item));
                }
                catch (Exception)
                {

                }
            }

            for (int i = 0; i < 5; i++)
            {
                Ellipse racer = new Ellipse();
                racer.Width = 20;
                racer.Height = 20;
                racer.Fill = Brushes.Blue;
                Canvas.SetTop(racer, i * 40);
                Canvas.SetLeft(racer, 0);
                RaceTrackCanvas.Children.Add(racer);
                racers.Add(i, racer);
            }
        }

        private async void StartRace()
        {
            // Создаем массив флагов для отметки гонщиков, которые выбыли из гонки
            bool[] eliminatedRacers = new bool[racers.Count];

            // Проходим по всем этапам гонки
            for (int stage = 1; stage <= 7; stage++)
            {
                // Выбираем случайного гонщика, который выбывает из гонки на этом этапе
                int eliminatedRacerIndex = uuu.Next(racers.Count);
                eliminatedRacers[eliminatedRacerIndex] = true; // отмечаем гонщика, который выбыл из гонки

                // Передвигаем оставшихся гонщиков на этапе
                for (int i = 0; i < racers.Count; i++)
                {
                    Ellipse racer = racers[i];
                    if (!eliminatedRacers[i]) // перемещаем только гонщиков, которые еще в гонке
                    {
                        double currentLeft = Canvas.GetLeft(racer);
                        double nextLeft = currentLeft + RaceTrackCanvas.ActualWidth/7;
                        Canvas.SetLeft(racer, nextLeft);
                    }
                }

                // Ждем 1 секунду перед началом следующего этапа
                await Task.Delay(250);
            }
        }
    }
}
