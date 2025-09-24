using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;

namespace LAB_FINAL_01
{
    public enum HandType {Rock,Paper,Scissors}
    public class HandS
    {
        public struct Hand
        {
            public const float HAND_SIZE = 45f;
            public const float MIN_SPEED = 0.5f;
            public const float MAX_SPEED = 1.5f;
            public const int SIZE = 45; 

            public HandType Type { get; set; }
            public PointF Position { get; set; }
            public PointF Velocity { get; set; }
            public Color Color { get; set; }

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

            public static bool CheckOverlap(Hand hand1, Hand hand2)
            {
                if (hand1.Type == hand2.Type) return false;

                float dx = hand1.Position.X - hand2.Position.X;
                float dy = hand1.Position.Y - hand2.Position.Y;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                return distance < HAND_SIZE;
            }
            public static HandType DetermineWinner(HandType type1, HandType type2)
            {
                return (type1, type2) switch
                {
                    (HandType.Rock, HandType.Scissors) => HandType.Rock,(HandType.Scissors, HandType.Rock) => HandType.Rock,
                    (HandType.Paper, HandType.Rock) => HandType.Paper,(HandType.Rock, HandType.Paper) => HandType.Paper,
                    (HandType.Scissors, HandType.Paper) => HandType.Scissors,(HandType.Paper, HandType.Scissors) => HandType.Scissors,_ => type1
                };
            }

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
