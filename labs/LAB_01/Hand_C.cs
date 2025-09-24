using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;

namespace LAB_FINAL_01
{
    public enum HandType {Rock,Paper,Scissors}//maing the enum of game 
    public class HandS
    {
        public struct Hand//ssetting the constanst so you can change  values after just from here
        {
            public const float HAND_SIZE = 45f;
            public const float MIN_SPEED = 0.5f;
            public const float MAX_SPEED = 1.5f;
            public const int SIZE = 45; //size of the circle

            public HandType Type { get; set; }//automic properties for getting and setting value during game
            public PointF Position { get; set; }
            public PointF Velocity { get; set; }
            public Color Color { get; set; }

            /// <summary>
            /// Creating the random position elements like sissor
            /// paper rock in the form of ewnum hand 
            /// </summary>
            /// <param name="random"></param>
            /// <param name="areaWidth"></param>
            /// <param name="areaHeight"></param>
            /// <returns></returns>
            public static Hand CreateRandom(Random random, int areaWidth, int areaHeight)
            {
                HandType type = (HandType)random.Next(0, 3);

                return new Hand
                {
                    Type = type,
                    Position = new PointF(random.Next((int)HAND_SIZE, areaWidth - (int)HAND_SIZE),random.Next((int)HAND_SIZE, areaHeight - (int)HAND_SIZE)),
                    Velocity = new PointF((float)(random.NextDouble() * (MAX_SPEED - MIN_SPEED) + MIN_SPEED) * (random.Next(0, 2) == 0 ? 1 : -1),(float)(random.NextDouble() * (MAX_SPEED - MIN_SPEED) + MIN_SPEED) * (random.Next(0, 2) == 0 ? 1 : -1)),
                    Color = GetColorForType(type)
                };
            }

            /// <summary>
            /// TO CHECKING THE OVERLAP LIKE ON PAPER TO PAPER SISOR TO PAPER 
            /// PAPER RORCK TO ROCK
            /// </summary>
            /// <param name="hand1"></param>
            /// <param name="hand2"></param>
            /// <returns></returns>
            public static bool CheckOverlap(Hand hand1, Hand hand2)
            {
                if (hand1.Type == hand2.Type) return false;

                float dx = hand1.Position.X - hand2.Position.X;
                float dy = hand1.Position.Y - hand2.Position.Y;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                return distance < HAND_SIZE;
            }
            /// <summary>
            /// DetermineWinner CHECKING THE WINNER AFTER THE COLISSION ON ROCK WINS OVER SISSORS
            /// </summary>
            /// <param name="type1"></param>
            /// <param name="type2"></param>
            /// <returns></returns>
            public static HandType DetermineWinner(HandType type1, HandType type2)
            {
                return (type1, type2) switch
                {
                    (HandType.Rock, HandType.Scissors) => HandType.Rock,(HandType.Scissors, HandType.Rock) => HandType.Rock,
                    (HandType.Paper, HandType.Rock) => HandType.Paper,(HandType.Rock, HandType.Paper) => HandType.Paper,
                    (HandType.Scissors, HandType.Paper) => HandType.Scissors,(HandType.Paper, HandType.Scissors) => HandType.Scissors,_ => type1
                };
            }
            /// <summary>
            /// NOTHING JUST AFTER COLLIOION WHO WINS ALSO CHANGE THE COLOR
            /// LIKE ROCK FOR RED PAPER GREEN 
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public static Color GetColorForType(HandType type)
            {
                return type switch
                {
                    HandType.Rock => Color.Red,HandType.Paper => Color.Green,HandType.Scissors => Color.Blue,_ => Color.Black
                };
            }
        }
    }
}
