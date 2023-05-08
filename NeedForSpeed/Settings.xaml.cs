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
        public int preCircle = 1;
        public int circle = 0;

        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Count_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "btnPlus") preCircle++;
            if (((Button)sender).Name == "btnMinus") preCircle--;
            tbCount.Text = preCircle.ToString();
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
