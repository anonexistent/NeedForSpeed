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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NeedForSpeed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random uuu;
        Settings settings;
        List<Subject> s1;

        Dictionary<Subject, Ellipse> rececs = new();

        void Test3()
        {
            for (int i = 0; i < 5; i++)
            {
                Ellipse racer = new Ellipse();
                racer.Width = 20;
                racer.Height = 20;
                var rndColor = new SolidColorBrush(Color.FromRgb((byte)uuu.Next(1, 255), (byte)uuu.Next(1, 255), (byte)uuu.Next(1, 233))); ;
                racer.Fill = rndColor;
                Canvas.SetTop(racer, i * 25);
                Canvas.SetLeft(racer, 5);
                RaceTrackCanvas.Children.Add(racer);

                rececs.Add(new() { Info =$"{i}*" }, racer);
            }

            for (int i = 0; i < settings.preCircle; i++)
            {
                tb1.Text += ch2(0.5f);
            }
        }

        string ch2(float killC)
        {
            settings.circle++;

            string logs = $"—\t{settings.circle} challenge\t—\n";

            bool oting = IsSuccessful(killC);
            logs += oting.ToString() + '\n';

            Subject temp = new() { Id = -1 };
            try
            {
                if(oting)
                {
                    do
                    {
                        var temp2 = rececs.Keys.Where(x => x.IsAlive == true).ToList();
                        temp = temp2[uuu.Next(0, temp2.Count-1)];
                    } while (!temp.IsAlive);
                    rececs.Remove(temp);
                }

            }
            catch (Exception)
            {
                throw;
            }

            foreach (var item in rececs)
            {
                if(item.Key.IsAlive)
                {
                    double currentLeft = Canvas.GetLeft(item.Value);
                    double nextLeft = currentLeft + RaceTrackCanvas.ActualWidth / settings.preCircle - 5;
                    Canvas.SetLeft(item.Value, nextLeft);
                    if (item.Key.IsAlive) item.Key.Info += settings.circle;
                }
            }

            logs += string.Join(',', rececs.Keys.Select(x=>x.Info).ToList())+ '\n';
            return logs;
        }

        public MainWindow()
        {
            InitializeComponent();
            settings = new();

            uuu = new();

            //Test1();
            //Test2();

            brd.Height = 0;

            btnStart.Click += BtnStart_Click2;
        }

        private void BtnStart_Click2(object sender, RoutedEventArgs e)
        {
            if(settings.circle>0)
            {
                MainWindow w = new();
                w.Show();
                this.Close();
            }

            Test3();
        }

        private bool IsToggle;

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            if (!IsToggle)
            {
                da.To = 90;
                da.Duration = TimeSpan.FromSeconds(1);
                brd.BeginAnimation(Border.HeightProperty, da);
                IsToggle = true;
            }
            else
            {
                da.To = 0;
                da.Duration = TimeSpan.FromSeconds(1);
                brd.BeginAnimation(Border.HeightProperty, da);
                IsToggle = false;
            }
        }

        private Dictionary<Subject, Ellipse> racers = new Dictionary<Subject, Ellipse>();

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            //// Создаем гонщиков
            CreateRacers();

            //// Запускаем гонку
            //StartRace();

            tb1.Text = "\t\tLogs\n\n";
            Test2();
            ClearGame();
            
        }

        #region dontWorking

        private void CreateRacers()
        {
            racers.Clear();

            for (int i = 0; i < 5; i++)
            {
                Ellipse racer = new Ellipse();
                racer.Width = 20;
                racer.Height = 20;
                var rndColor = new SolidColorBrush(Color.FromRgb((byte)uuu.Next(1, 255), (byte)uuu.Next(1, 255), (byte)uuu.Next(1, 233))); ;
                racer.Fill = rndColor;
                Canvas.SetTop(racer, i * 40);
                Canvas.SetLeft(racer, 0);
                RaceTrackCanvas.Children.Add(racer);
                racers.Add(new() { Id = i }, racer);
            }
        }

        private async void StartRace()
        {
            // Создаем массив флагов для отметки гонщиков, которые выбыли из гонки
            bool[] eliminatedRacers = racers.Keys.Select(x=>x.IsAlive).ToArray();

            // Проходим по всем этапам гонки
            for (int stage = 1; stage <= 5; stage++)
            {
                // Выбираем случайного гонщика, который выбывает из гонки на этом этапе
                int eliminatedRacerIndex = uuu.Next(racers.Count);
                eliminatedRacers[eliminatedRacerIndex] = false; // отмечаем гонщика, который выбыл из гонки

                // Передвигаем оставшихся гонщиков на этапе
                for (int i = 0; i < racers.Count; i++)
                {
                    Ellipse racer = racers[racers.Keys.Where(x=>x.Id==i).First()];
                    if (!eliminatedRacers[i]) // перемещаем только гонщиков, которые еще в гонке
                    {
                        double currentLeft = Canvas.GetLeft(racer);
                        double nextLeft = currentLeft + RaceTrackCanvas.ActualWidth / 5;
                        Canvas.SetLeft(racer, nextLeft);
                    }
                }

                // Ждем 0.25 секунду перед началом следующего этапа
                await Task.Delay(250);
            }
        }

        private void ClearGame()
        {
            settings.preCircle = 0;
            settings.tbCount.Text = 0.ToString();
            settings.circle = 0;

            settings.preCount = 0;
            settings.tbCount1.Text = "0";
            settings.count = 0;
        }

        #endregion

        private void Test2()
        {
            s1 = new List<Subject>();
            for (int i = 0; i < settings.preCount; i++) s1.Add(new($"no {i + 1}*"));

            Subject[] sClone = new Subject[settings.preCount];
            s1.CopyTo(sClone);

            for (int i = 0; i < settings.preCircle; i++)
            {                
                tb1.Text += ch(s1, 0.1f);
            }
            
        }

        //  нужно брать последний символ в строке саб.Инфо инт парс() +1 это будет круг на котором игрок выбыл
        //  далее будет несколько прямых на которых будет отображаться на сколько далеко проехал игрок то есть линия делится на
        //  сколько кругов столько и частей линии

        void TestLine()
        {
            var temp = s1.Count>1 ? s1[s1.Count - 1] : s1[0];
            //MessageBox.Show(temp.Info[temp.Info.Length - 1].ToString());
        }

        string ch(List<Subject> a, float killChance)
        {
            settings.circle++;

            string logs = $"—\t{settings.circle} challenge\t—\n";

            bool killed = IsSuccessful(killChance);
            logs += killed.ToString() +'\n';

            if (killed)
            {
                int whoDie = uuu.Next(0, a.Count);
                var temp = a[whoDie];
                temp.IsAlive = false;
                logs += temp.Info +'\n';
                a.Remove(temp);

            }

            foreach (Subject s in a)
            {
                double currentLeft = Canvas.GetLeft(racers[s]);
                double nextLeft = currentLeft + RaceTrackCanvas.ActualWidth / 5;
                Canvas.SetLeft(racers[s], nextLeft);
                if (s.IsAlive) s.Info += settings.circle;
            }

            logs += string.Join(',', a.Select(x => x.Info)) + '\n';
            return logs;
        }

        bool IsSuccessful(float a) => a<=((float)uuu.Next(0,100)/100);

        private void Test1()
        {
            //Subject s1 = new Subject();
            //MessageBox.Show(s1.Info);
            //s1.Challenges[0] = false;
            //MessageBox.Show(s1.Info);

            List<Subject> ps = new() { new(), new(), new(), new() };

            //var a = new List<bool>();
            //int b = 0;


            //for (int i = 0; i < 100; i++)
            //{
            //    a.Add(IsSuccessful(0.5f));
            //    if (a[i]) b++;
            //}
            //MessageBox.Show(string.Join('-', a) + b);

            int a = 0;
            var b = new List<bool>();
            for (int i = 0; i < 100_000; i++)
            {
                b.Add(IsSuccessful(0.3f));
                if (b[i]) a++;
            }
            tb1.Text = a.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = true;
            infoD.Visibility = Visibility.Collapsed;
            settings.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            settings.Close();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tb1.Visibility = tb1.Visibility==Visibility.Visible?Visibility.Collapsed:Visibility.Visible;
        }
    }
}
