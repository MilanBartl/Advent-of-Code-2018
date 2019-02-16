using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day10
{
    public class Worker
    {
        private List<Star> _stars = new List<Star>();

        public Worker()
        {
            var splits = _input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string split in splits)
            {
                _stars.Add(new Star(split));
            }

            //PrintStars(40);
        }

        public int Work1()
        {
            while (!AreStarsAligned())
                MoveStars();

            PrintStars(200, 50);
            return 1;
        }

        public int Work2()
        {
            int i = 0;
            while (!AreStarsAligned())
            {
                MoveStars();
                i++;
            }
            return i;
        }

        private void PrintStars(int dimX, int dimY)
        {
            var star = _stars[0];

            int radX = dimX / 2;
            int radY = dimY / 2;

            int startX = -radX + star.X;
            int endX = radX + star.X;
            int startY = -radY + star.Y;
            int endY = radY + star.Y;

            for (int i = startY; i < endY; i++)
            {
                for (int j = startX; j < endX; j++)
                {
                    if (_stars.Any(st => st.X == j && st.Y == i))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.Write(Environment.NewLine);
            }
        }

        private void MoveStars()
        {
            foreach (var star in _stars)
            {
                star.X += star.VelX;
                star.Y += star.VelY;
            }
        }

        private bool AreStarsAligned()
        {
            //return _stars.GroupBy(st => st.X).Where(gr => gr.Select(x => x).Count() > 7).Count() > 8;
            var group = _stars.GroupBy(st => st.X).Where(gr => gr.Select(x => x).Count() > 7).FirstOrDefault();
            if (group != null)
            {
                var ordered = group.OrderBy(st => st.Y).Select(st => st.Y).ToList();

                if (Math.Abs(Math.Abs(ordered[0]) - Math.Abs(ordered[6])) <= 7)
                    return true;
                else
                    return false;
            }
            return false;
        }

        string _testInput = @"position=< 9,  1> velocity=< 0,  2>
position=< 7,  0> velocity=<-1,  0>
position=< 3, -2> velocity=<-1,  1>
position=< 6, 10> velocity=<-2, -1>
position=< 2, -4> velocity=< 2,  2>
position=<-6, 10> velocity=< 2, -2>
position=< 1,  8> velocity=< 1, -1>
position=< 1,  7> velocity=< 1,  0>
position=<-3, 11> velocity=< 1, -2>
position=< 7,  6> velocity=<-1, -1>
position=<-2,  3> velocity=< 1,  0>
position=<-4,  3> velocity=< 2,  0>
position=<10, -3> velocity=<-1,  1>
position=< 5, 11> velocity=< 1, -2>
position=< 4,  7> velocity=< 0, -1>
position=< 8, -2> velocity=< 0,  1>
position=<15,  0> velocity=<-2,  0>
position=< 1,  6> velocity=< 1,  0>
position=< 8,  9> velocity=< 0, -1>
position=< 3,  3> velocity=<-1,  1>
position=< 0,  5> velocity=< 0, -1>
position=<-2,  2> velocity=< 2,  0>
position=< 5, -2> velocity=< 1,  2>
position=< 1,  4> velocity=< 2,  1>
position=<-2,  7> velocity=< 2, -2>
position=< 3,  6> velocity=<-1, -1>
position=< 5,  0> velocity=< 1,  0>
position=<-6,  0> velocity=< 2,  0>
position=< 5,  9> velocity=< 1, -2>
position=<14,  7> velocity=<-2,  0>
position=<-3,  6> velocity=< 2, -1>";

        string _input = @"position=< 21518, -21209> velocity=<-2,  2>
position=< 10842,  21423> velocity=<-1, -2>
position=< 32189, -21209> velocity=<-3,  2>
position=<-21158, -21218> velocity=< 2,  2>
position=<-31794, -53194> velocity=< 3,  5>
position=<-42469,  42743> velocity=< 4, -4>
position=<-53120, -31873> velocity=< 5,  3>
position=< 32177, -42536> velocity=<-3,  4>
position=< 53505,  32084> velocity=<-5, -3>
position=<-53091,  10764> velocity=< 5, -1>
position=<-53141, -21211> velocity=< 5,  2>
position=<-42429,  10764> velocity=< 4, -1>
position=<-10492, -31873> velocity=< 1,  3>
position=< 42819, -10550> velocity=<-4,  1>
position=<-53096, -10551> velocity=< 5,  1>
position=<-10492, -42536> velocity=< 1,  4>
position=<-10508,  42739> velocity=< 1, -4>
position=<-10471, -42533> velocity=< 1,  4>
position=< 53446,  32086> velocity=<-5, -3>
position=< 10820, -53195> velocity=<-1,  5>
position=< 10858,  21423> velocity=<-1, -2>
position=<-31810, -10553> velocity=< 3,  1>
position=<-10492,  32077> velocity=< 1, -3>
position=<-31799, -42527> velocity=< 3,  4>
position=<-10484,  42738> velocity=< 1, -4>
position=< 10862,  10767> velocity=<-1, -1>
position=<-10503,  10759> velocity=< 1, -1>
position=<-21130,  32081> velocity=< 2, -3>
position=<-42477, -31877> velocity=< 4,  3>
position=<-10452, -42530> velocity=< 1,  4>
position=<-10452,  53399> velocity=< 1, -5>
position=<-31826,  42744> velocity=< 3, -4>
position=<-42444, -10556> velocity=< 4,  1>
position=< 42824, -31873> velocity=<-4,  3>
position=<-10492,  32082> velocity=< 1, -3>
position=<-53092,  10759> velocity=< 5, -1>
position=< 21522,  42745> velocity=<-2, -4>
position=< 53474, -42532> velocity=<-5,  4>
position=<-31822,  42737> velocity=< 3, -4>
position=< 32131, -42529> velocity=<-3,  4>
position=<-10476, -53193> velocity=< 1,  5>
position=< 21473, -10551> velocity=<-2,  1>
position=<-10497,  21422> velocity=< 1, -2>
position=< 21469,  42741> velocity=<-2, -4>
position=< 21470,  10763> velocity=<-2, -1>
position=<-31807,  32086> velocity=< 3, -3>
position=< 32144,  32078> velocity=<-3, -3>
position=<-53120, -42531> velocity=< 5,  4>
position=<-31773, -31871> velocity=< 3,  3>
position=<-31800, -21218> velocity=< 3,  2>
position=< 53478, -21213> velocity=<-5,  2>
position=< 21498,  21424> velocity=<-2, -2>
position=< 42798,  32081> velocity=<-4, -3>
position=< 42790,  10761> velocity=<-4, -1>
position=<-31778, -10558> velocity=< 3,  1>
position=<-21106,  21418> velocity=< 2, -2>
position=<-31785,  53401> velocity=< 3, -5>
position=< 53497, -42536> velocity=<-5,  4>
position=< 53491, -42535> velocity=<-5,  4>
position=<-21115, -21218> velocity=< 2,  2>
position=< 53478,  53401> velocity=<-5, -5>
position=< 10835,  32077> velocity=<-1, -3>
position=<-10500,  21421> velocity=< 1, -2>
position=<-53142, -21212> velocity=< 5,  2>
position=<-10490, -31868> velocity=< 1,  3>
position=< 53506,  21419> velocity=<-5, -2>
position=< 21517,  10764> velocity=<-2, -1>
position=< 32136,  32082> velocity=<-3, -3>
position=<-42485, -42536> velocity=< 4,  4>
position=< 10821, -53195> velocity=<-1,  5>
position=<-42445, -10551> velocity=< 4,  1>
position=<-10473, -21214> velocity=< 1,  2>
position=< 42806, -53186> velocity=<-4,  5>
position=< 42791,  10767> velocity=<-4, -1>
position=<-53144,  32083> velocity=< 5, -3>
position=<-53096,  53402> velocity=< 5, -5>
position=<-21119,  42738> velocity=< 2, -4>
position=< 42824,  53404> velocity=<-4, -5>
position=< 21495,  32081> velocity=<-2, -3>
position=<-31782,  21421> velocity=< 3, -2>
position=<-21157, -31873> velocity=< 2,  3>
position=< 53478,  32083> velocity=<-5, -3>
position=< 53470,  53398> velocity=<-5, -5>
position=< 42797,  21422> velocity=<-4, -2>
position=<-21143, -31877> velocity=< 2,  3>
position=< 32144, -21216> velocity=<-3,  2>
position=<-10482,  32086> velocity=< 1, -3>
position=< 42831, -21216> velocity=<-4,  2>
position=<-42448,  53397> velocity=< 4, -5>
position=<-21135, -21213> velocity=< 2,  2>
position=< 42806,  32086> velocity=<-4, -3>
position=< 42805,  53404> velocity=<-4, -5>
position=<-53096,  32080> velocity=< 5, -3>
position=< 32155,  32077> velocity=<-3, -3>
position=< 53463, -10550> velocity=<-5,  1>
position=< 53475,  42737> velocity=<-5, -4>
position=< 21470, -31873> velocity=<-2,  3>
position=<-10452, -10550> velocity=< 1,  1>
position=< 10850,  21427> velocity=<-1, -2>
position=< 10854,  53398> velocity=<-1, -5>
position=<-10500,  53395> velocity=< 1, -5>
position=< 21497,  10759> velocity=<-2, -1>
position=< 32155, -42527> velocity=<-3,  4>
position=< 53505, -42534> velocity=<-5,  4>
position=<-21142,  53404> velocity=< 2, -5>
position=< 32176, -53193> velocity=<-3,  5>
position=<-42437, -53193> velocity=< 4,  5>
position=<-21163,  53396> velocity=< 2, -5>
position=< 32128, -42531> velocity=<-3,  4>
position=< 42798, -53195> velocity=<-4,  5>
position=< 42829,  32081> velocity=<-4, -3>
position=< 21510, -53192> velocity=<-2,  5>
position=<-53108, -21214> velocity=< 5,  2>
position=<-21124,  53399> velocity=< 2, -5>
position=<-21108, -31875> velocity=< 2,  3>
position=<-31794,  10768> velocity=< 3, -1>
position=< 10842,  21426> velocity=<-1, -2>
position=<-53142,  21424> velocity=< 5, -2>
position=<-42476,  53399> velocity=< 4, -5>
position=< 42816, -10554> velocity=<-4,  1>
position=< 32179,  42741> velocity=<-3, -4>
position=< 32171,  32081> velocity=<-3, -3>
position=<-10503,  32077> velocity=< 1, -3>
position=< 42813,  42745> velocity=<-4, -4>
position=< 21485,  42736> velocity=<-2, -4>
position=<-42444,  10762> velocity=< 4, -1>
position=<-31789, -53187> velocity=< 3,  5>
position=<-10480,  32077> velocity=< 1, -3>
position=< 32128, -10556> velocity=<-3,  1>
position=< 10858, -31869> velocity=<-1,  3>
position=<-53144, -42532> velocity=< 5,  4>
position=<-42461, -21215> velocity=< 4,  2>
position=<-42457, -10550> velocity=< 4,  1>
position=< 32186,  32083> velocity=<-3, -3>
position=<-42440,  53403> velocity=< 4, -5>
position=<-21130,  21419> velocity=< 2, -2>
position=< 42819, -21216> velocity=<-4,  2>
position=<-31810, -31874> velocity=< 3,  3>
position=<-53120,  32080> velocity=< 5, -3>
position=< 32178, -42536> velocity=<-3,  4>
position=< 21509, -31876> velocity=<-2,  3>
position=<-42436, -31868> velocity=< 4,  3>
position=<-42432, -21211> velocity=< 4,  2>
position=<-21155,  21422> velocity=< 2, -2>
position=<-53111, -42532> velocity=< 5,  4>
position=< 42819, -53188> velocity=<-4,  5>
position=<-21147, -21209> velocity=< 2,  2>
position=< 42840,  42741> velocity=<-4, -4>
position=<-31782,  32079> velocity=< 3, -3>
position=<-42448, -31871> velocity=< 4,  3>
position=< 21501,  53398> velocity=<-2, -5>
position=<-31797,  21426> velocity=< 3, -2>
position=<-42448,  53402> velocity=< 4, -5>
position=<-53110, -21214> velocity=< 5,  2>
position=< 32179,  32086> velocity=<-3, -3>
position=<-21138,  32080> velocity=< 2, -3>
position=< 10871,  32086> velocity=<-1, -3>
position=< 53470,  32082> velocity=<-5, -3>
position=<-53141, -53193> velocity=< 5,  5>
position=< 53462, -31868> velocity=<-5,  3>
position=<-21116, -42531> velocity=< 2,  4>
position=<-31782, -21212> velocity=< 3,  2>
position=<-42477, -31874> velocity=< 4,  3>
position=< 42836, -21218> velocity=<-4,  2>
position=<-31826, -53193> velocity=< 3,  5>
position=< 10818,  42740> velocity=<-1, -4>
position=< 53503,  10763> velocity=<-5, -1>
position=<-21109, -21215> velocity=< 2,  2>
position=<-21143,  53403> velocity=< 2, -5>
position=<-53120,  32078> velocity=< 5, -3>
position=<-31794, -10550> velocity=< 3,  1>
position=<-42473,  42740> velocity=< 4, -4>
position=<-31782,  53398> velocity=< 3, -5>
position=<-53120, -21218> velocity=< 5,  2>
position=<-53119,  53399> velocity=< 5, -5>
position=<-10500,  53404> velocity=< 1, -5>
position=< 53503,  53399> velocity=<-5, -5>
position=< 21498,  53396> velocity=<-2, -5>
position=<-31766,  42737> velocity=< 3, -4>
position=< 32138, -42532> velocity=<-3,  4>
position=< 32133, -10559> velocity=<-3,  1>
position=< 53505,  42738> velocity=<-5, -4>
position=< 21518,  21418> velocity=<-2, -2>
position=<-53144,  53399> velocity=< 5, -5>
position=<-31810, -42535> velocity=< 3,  4>
position=<-53099, -53187> velocity=< 5,  5>
position=<-31805, -42527> velocity=< 3,  4>
position=< 21478,  42740> velocity=<-2, -4>
position=< 53506,  53403> velocity=<-5, -5>
position=<-21165, -42533> velocity=< 2,  4>
position=< 10850, -21217> velocity=<-1,  2>
position=<-21143, -42528> velocity=< 2,  4>
position=<-31769,  42740> velocity=< 3, -4>
position=<-53088, -21211> velocity=< 5,  2>
position=<-53119, -31868> velocity=< 5,  3>
position=<-42448,  32084> velocity=< 4, -3>
position=<-53096, -31872> velocity=< 5,  3>
position=<-53091, -53186> velocity=< 5,  5>
position=< 42787,  10760> velocity=<-4, -1>
position=< 10861,  21427> velocity=<-1, -2>
position=< 53446,  53402> velocity=<-5, -5>
position=< 42832,  10759> velocity=<-4, -1>
position=< 21485, -42535> velocity=<-2,  4>
position=<-10482, -42532> velocity=< 1,  4>
position=< 53483, -53194> velocity=<-5,  5>
position=< 10835, -31868> velocity=<-1,  3>
position=< 53507, -10550> velocity=<-5,  1>
position=< 42815,  32077> velocity=<-4, -3>
position=< 10847, -42533> velocity=<-1,  4>
position=<-42448,  32084> velocity=< 4, -3>
position=<-21117,  32086> velocity=< 2, -3>
position=<-21167,  42742> velocity=< 2, -4>
position=<-31826,  21427> velocity=< 3, -2>
position=< 32181,  42745> velocity=<-3, -4>
position=< 32136,  10766> velocity=<-3, -1>
position=<-21138,  32085> velocity=< 2, -3>
position=< 21501, -21218> velocity=<-2,  2>
position=< 21510, -10557> velocity=<-2,  1>
position=<-31777,  21427> velocity=< 3, -2>
position=<-53084,  21426> velocity=< 5, -2>
position=<-31810,  21426> velocity=< 3, -2>
position=<-31797, -10556> velocity=< 3,  1>
position=<-31794,  42744> velocity=< 3, -4>
position=<-31773, -42529> velocity=< 3,  4>
position=< 10818,  32084> velocity=<-1, -3>
position=<-21149, -21209> velocity=< 2,  2>
position=< 10855,  32078> velocity=<-1, -3>
position=< 32186, -21215> velocity=<-3,  2>
position=< 53454,  42743> velocity=<-5, -4>
position=< 21530,  10759> velocity=<-2, -1>
position=< 42795,  53404> velocity=<-4, -5>
position=< 21522,  32078> velocity=<-2, -3>
position=<-21111, -42535> velocity=< 2,  4>
position=<-31826,  32079> velocity=< 3, -3>
position=<-21133,  10763> velocity=< 2, -1>
position=<-10499,  32077> velocity=< 1, -3>
position=< 32170, -21213> velocity=<-3,  2>
position=<-53096,  10765> velocity=< 5, -1>
position=< 42839, -10554> velocity=<-4,  1>
position=<-53101,  53400> velocity=< 5, -5>
position=<-31782, -21212> velocity=< 3,  2>
position=<-10508, -10556> velocity=< 1,  1>
position=<-53142, -31874> velocity=< 5,  3>
position=< 53479,  53399> velocity=<-5, -5>
position=<-31818, -31871> velocity=< 3,  3>
position=<-31789, -21217> velocity=< 3,  2>
position=< 32186,  10765> velocity=<-3, -1>
position=<-42427, -21215> velocity=< 4,  2>
position=<-21157,  10759> velocity=< 2, -1>
position=<-10448,  42737> velocity=< 1, -4>
position=< 21493, -10550> velocity=<-2,  1>
position=< 42819, -10557> velocity=<-4,  1>
position=< 42830,  10764> velocity=<-4, -1>
position=<-31794, -21211> velocity=< 3,  2>
position=<-21139, -10555> velocity=< 2,  1>
position=<-42441,  10766> velocity=< 4, -1>
position=< 32176,  42740> velocity=<-3, -4>
position=< 32136,  53401> velocity=<-3, -5>
position=<-10484,  42737> velocity=< 1, -4>
position=<-21138,  32079> velocity=< 2, -3>
position=<-10508,  10759> velocity=< 1, -1>
position=< 21522,  53396> velocity=<-2, -5>
position=<-31818,  21419> velocity=< 3, -2>
position=< 10867, -21213> velocity=<-1,  2>
position=< 21469,  21424> velocity=<-2, -2>
position=< 32139, -53191> velocity=<-3,  5>
position=< 42819, -21214> velocity=<-4,  2>
position=< 42803, -42534> velocity=<-4,  4>
position=< 10847,  53398> velocity=<-1, -5>
position=<-42437, -42528> velocity=< 4,  4>
position=< 21521, -42531> velocity=<-2,  4>
position=< 42835,  32080> velocity=<-4, -3>
position=<-10503,  21427> velocity=< 1, -2>
position=<-21167, -42527> velocity=< 2,  4>
position=< 21521,  32077> velocity=<-2, -3>
position=<-10484, -10553> velocity=< 1,  1>
position=<-10452, -42529> velocity=< 1,  4>
position=< 10847, -21213> velocity=<-1,  2>
position=< 53470,  21419> velocity=<-5, -2>
position=< 10862, -10551> velocity=<-1,  1>
position=<-42469, -31868> velocity=< 4,  3>
position=< 10838, -10550> velocity=<-1,  1>
position=< 32152, -42527> velocity=<-3,  4>
position=< 32176,  53401> velocity=<-3, -5>
position=<-31825, -10554> velocity=< 3,  1>
position=<-21167, -42528> velocity=< 2,  4>
position=<-21115, -31872> velocity=< 2,  3>
position=< 10866, -21217> velocity=<-1,  2>
position=< 21506, -10555> velocity=<-2,  1>
position=<-21143,  53399> velocity=< 2, -5>
position=<-10471, -31877> velocity=< 1,  3>
position=<-10508,  32085> velocity=< 1, -3>
position=< 10866, -10550> velocity=<-1,  1>
position=<-53088, -31877> velocity=< 5,  3>
position=<-10495,  10759> velocity=< 1, -1>
position=< 10847,  21418> velocity=<-1, -2>
position=<-10484,  42743> velocity=< 1, -4>
position=< 10818,  10764> velocity=<-1, -1>
position=< 32155,  42740> velocity=<-3, -4>
position=< 42787,  21423> velocity=<-4, -2>
position=< 53490,  21425> velocity=<-5, -2>
position=<-42448,  10764> velocity=< 4, -1>
position=<-21119,  53396> velocity=< 2, -5>
position=< 21528, -21211> velocity=<-2,  2>
position=< 53502,  10762> velocity=<-5, -1>
position=< 42820, -31873> velocity=<-4,  3>
position=< 21481,  53395> velocity=<-2, -5>
position=<-10468, -42527> velocity=< 1,  4>
position=< 32130, -21212> velocity=<-3,  2>
position=< 32181, -10554> velocity=<-3,  1>
position=< 32184,  53395> velocity=<-3, -5>
position=< 10851, -53188> velocity=<-1,  5>
position=< 21477,  42736> velocity=<-2, -4>
position=<-21159,  21426> velocity=< 2, -2>
position=<-31773,  10767> velocity=< 3, -1>
position=< 42827,  32077> velocity=<-4, -3>
position=<-31818, -21210> velocity=< 3,  2>
position=< 53458, -21218> velocity=<-5,  2>
position=<-10452,  42740> velocity=< 1, -4>
position=<-53142, -21215> velocity=< 5,  2>
position=< 10826,  53398> velocity=<-1, -5>
position=<-21166,  42741> velocity=< 2, -4>
position=<-42469, -53187> velocity=< 4,  5>
position=< 32173,  10768> velocity=<-3, -1>
position=< 53497,  10759> velocity=<-5, -1>
position=< 53449, -10557> velocity=<-5,  1>
position=< 32170, -53190> velocity=<-3,  5>
position=< 42843, -53187> velocity=<-4,  5>
position=< 10852,  32081> velocity=<-1, -3>
position=<-10499,  53395> velocity=< 1, -5>
position=< 21496, -53195> velocity=<-2,  5>
position=< 32157,  32084> velocity=<-3, -3>
position=< 42831,  10765> velocity=<-4, -1>
position=< 21498, -10556> velocity=<-2,  1>
position=< 42843, -53193> velocity=<-4,  5>
position=< 42811, -53189> velocity=<-4,  5>
position=<-42477,  53397> velocity=< 4, -5>";
    }
}