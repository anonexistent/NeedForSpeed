using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

/// <summary>
/// Description player
/// </summary>

namespace NeedForSpeed
{
    internal class Subject
    {
        public bool IsAlive { get; set; }
        //public bool[] Challenges { get { return Challenges; } set { Challenges = value; 
        //                                                                    //MakeInfo(); 
        //                                                                } }
        public int Id { get; set; }
        public string Info { get; set; }

        public Subject()
        {
            IsAlive = true;
            //Challenges = new bool[4] { false, false, false, false };
            Info = string.Empty;
        }
        public Subject(string info)
        {
            IsAlive = true;
            Info = info;
        }

        //void MakeInfo()
        //{
        //    if (challenges.Contains(false))
        //    {
        //        IsAlive = false;
        //        Info = "die ☺";
        //        for (int i = 0; i < Challenges.Length; i++) if (!Challenges[i]) Info += $" (test № {i+1})";
        //    }

        //}
    }
}
