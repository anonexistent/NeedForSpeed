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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public int preCircle = 0;
        public int circle = 0;
        public int preCount = 5;
        public int count = 0;

        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Count_Click(object sender, RoutedEventArgs e)
        {

            switch (((Button)sender).Name)
            {
                case "btnPlus":
                    preCircle++;
                    break;
                case "btnMinus":
                    preCircle--; 
                    break;
                case "btnPlus1":
                    preCount++; 
                    break;
                case "btnMinus1":
                    preCircle--; 
                    break;

                default:
                    break;
            }

            tbCount.Text = preCircle.ToString();
            tbCount1.Text = preCount.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ColorZone_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
