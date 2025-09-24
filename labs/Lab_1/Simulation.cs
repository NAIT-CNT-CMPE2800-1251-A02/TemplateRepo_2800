using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_1.HandS;
using System.Diagnostics;
using GDIDrawer;

namespace Lab_1
{
    public class Simulation<T> where T : struct
    {

            private readonly List<T> _items = new();
            private readonly Random _rnd = new();
        private readonly CDrawer _drawer;
            private int _frameCount = 0;

            public Simulation(int handCount, int width = 800, int height = 600)
            {
               _drawer = new CDrawer();
                if (typeof(T) == typeof(Hand))
                    InitializeHands(handCount, width, height);
            }

            private void InitializeHands(int count, int width, int height)
            {
                for (int i = 0; i < count; i++)
                {
                    var pos = new PointF(_rnd.Next(Hand.SIZE, width - Hand.SIZE),_rnd.Next(Hand.SIZE, height - Hand.SIZE));

                    var vel = new PointF((float)(_rnd.NextDouble() * (Hand.MAX_SPEED - Hand.MIN_SPEED) + Hand.MIN_SPEED),(float)(_rnd.NextDouble() * (Hand.MAX_SPEED - Hand.MIN_SPEED) + Hand.MIN_SPEED));

                    if (_rnd.Next(2) == 0) vel.X = -vel.X;
                    if (_rnd.Next(2) == 0) vel.Y = -vel.Y;

                    var type = (HandType)_rnd.Next(3);

                    // boxed generic add
                    _items.Add((T)(object)new Hand { Position = pos, Velocity = vel, Type = type });
                }
            }

            // Yield return collision pairs
            private IEnumerable<(Hand, Hand)> GetCollisions()
            {
                for (int i = 0; i < _items.Count; i++)
                    for (int j = i + 1; j < _items.Count; j++)
                    {
                        var hi = (Hand)(object)_items[i];
                        var hj = (Hand)(object)_items[j];
                        if (Hand.Intersects(hi, hj) && hi.Type != hj.Type)
                            yield return (hi, hj);
                    }
            }

            public (int frame, HandType? winner) Tick()
            {
                _frameCount++;
                _drawer.Clear();

                // Move and bounce (using yield update pattern)
                for (int i = 0; i < _items.Count; i++)
                {
                    var h = (Hand)(object)_items[i];
                    h.Position = new PointF(h.Position.X + h.Velocity.X, h.Position.Y + h.Velocity.Y);

                    if (h.Position.X < Hand.SIZE || h.Position.X > _drawer.ScaledWidth - Hand.SIZE)
                        h.Velocity = new PointF(-h.Velocity.X, h.Velocity.Y);

                    if (h.Position.Y < Hand.SIZE || h.Position.Y > _drawer.ScaledHeight - Hand.SIZE)
                        h.Velocity = new PointF(h.Velocity.X, -h.Velocity.Y);

                    _items[i] = (T)(object)h;
                }

                // Resolve collisions
                foreach (var (a, b) in GetCollisions())
                {
                    var winner = Hand.Resolve(a.Type, b.Type);
                    int idxA = _items.IndexOf((T)(object)a);
                    int idxB = _items.IndexOf((T)(object)b);
                    var hA = (Hand)(object)_items[idxA];
                    var hB = (Hand)(object)_items[idxB];
                    hA.Type = hB.Type = winner;
                    _items[idxA] = (T)(object)hA;
                    _items[idxB] = (T)(object)hB;
                }

                // Draw
                foreach (var t in _items)
                {
                    var h = (Hand)(object)t;
                    _drawer.AddCenteredEllipse((int)h.Position.X, (int)h.Position.Y, Hand.SIZE, Hand.SIZE,
                        h.Type switch
                        {
                            HandType.Rock => Color.Gray,
                            HandType.Paper => Color.White,
                            HandType.Scissors => Color.Red,
                            _ => Color.Black
                        });
                }

                // Win condition via extension
                var hands = _items.Cast<Hand>().ToList();
                HandType? winnerType = hands.IsWinCondition();
                return (_frameCount, winnerType);
            }
        }




    }

