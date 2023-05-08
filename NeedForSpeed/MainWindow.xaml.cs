﻿using System;
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

namespace NeedForSpeed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random uuu;
        int preCircle = 1;
        int circle = 0;

        public MainWindow()
        {
            InitializeComponent();

            uuu = new();

            //Test1();
            //Test2();

            btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = "\t\tLogs\n\n";
            Test2();
            ClearGame();
        }

        private void ClearGame()
        {
            preCircle = 1;
            circle = 0;
        }

        private void Test2()
        {
            List<Subject> s1 = new List<Subject>() { new("no 1*"),new("no 2*"),new("no 3*"),new("no 4*") };
            Subject[] sClone = new Subject[4];
            s1.CopyTo(sClone);

            for (int i = 0; i < preCircle; i++)
            {
                tb1.Text += ch(s1, 0.1f);
            }
        }

        string ch(List<Subject> a, float killChance)
        {
            circle++;

            string logs = $"—\t{circle} challenge\t—\n";

            bool killed = IsSuccessful(killChance);
            logs += killed.ToString() +'\n';

            if (killed)
            {
                int whoDie = uuu.Next(0, a.Count);
                var temp = a[whoDie];
                logs += temp.Info +'\n';
                a.Remove(temp);
            }

            foreach (Subject s in a)
            {
                if (s.IsAlive) s.Info += circle;
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

        private void Button_Count_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "btnPlus") preCircle++;
            if (((Button)sender).Name == "btnMinus") preCircle--;        
            tbCount.Text = preCircle.ToString();
        }
    }
}
