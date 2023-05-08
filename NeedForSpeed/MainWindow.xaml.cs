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

namespace NeedForSpeed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random uuu;

        public MainWindow()
        {
            InitializeComponent();

            uuu = new();

            //Test1();
            Test2();
        }

        private void Test2()
        {
            List<Subject> s1 = new List<Subject>() { new("no 1*"),new("no 2*"),new("no 3*"),new("no 4*") };

            tb1.Text = firstCh(s1, 0.3f);
        }

        string firstCh(List<Subject> a, float killChance)
        {
            string logs = string.Empty;

            bool firstKill = IsSuccessful(killChance);
            logs += firstKill.ToString() +'\n';


            if (firstKill)
            {
                int whoDie = uuu.Next(0, a.Count);                
                var temp = a[whoDie];
                logs += whoDie.ToString()+'\n' + temp.Info +'\n';
                a.Remove(temp);
            }

            foreach (Subject s in a)
            {
                if(s.IsAlive) s.Info += "1";
            }

            logs += string.Join(',', a.Select(x=>x.Info));

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
    }
}
