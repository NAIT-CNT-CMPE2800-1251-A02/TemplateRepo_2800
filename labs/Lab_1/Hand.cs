using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public class HandS
    {
        public enum HandType { Rock, Paper, Scissors }

        public struct Hand
        {
            public const int SIZE = 20;
            public const float MIN_SPEED = 0.5f;
            public const float MAX_SPEED = 2f;

            public PointF Position;
            public PointF Velocity;
            public HandType Type;

            // Collision detection
            public static bool Intersects(in Hand a, in Hand b)
            {
                float dx = a.Position.X - b.Position.X;
                float dy = a.Position.Y - b.Position.Y;
                return MathF.Sqrt(dx * dx + dy * dy) < SIZE;
            }

            // Rock-Paper-Scissors outcome
            public static HandType Resolve(HandType a, HandType b) => (a, b) switch
            {
                _ when a == b => a,
                (HandType.Rock, HandType.Scissors) => a,
                (HandType.Paper, HandType.Rock) => a,
                (HandType.Scissors, HandType.Paper) => a,
                _ => b
            };
        }




    }
}
