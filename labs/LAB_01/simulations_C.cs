using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LAB_FINAL_01.HandS;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using GDIDrawer;

namespace LAB_FINAL_01
{
    public class SimulationManager
    {
        public IReadOnlyList<Hand> Hands => _hands.AsReadOnly();


        // Constants for simulation parameters
        public const int CANVAS_WIDTH = 800;
        public const int CANVAS_HEIGHT = 600;
        public const int DEFAULT_INTERVAL = 50;
        public const int MIN_INTERVAL = 5;
        public const int MAX_INTERVAL = 1000;

        List<Hand> _hands;
        public CDrawer _drawer;
        Random _random;
        int _frameCount;

        public bool IsRunning { get; private set; }

        public SimulationManager()
        {
            _hands = new List<Hand>();
            _drawer = new CDrawer(CANVAS_WIDTH, CANVAS_HEIGHT, false);
            _random = new Random();
            _frameCount = 0;
        }

        /// <summary>
        /// Initializes the simulation with the specified number of hands.
        /// Validates minimum hand count constraint (minimum 2 hands).
        /// </summary>
        public void Initialize(int handCount)
        {
            if (handCount < 2)
                throw new ArgumentException("Minimum hand count is 2", nameof(handCount));

            _hands.Clear();
            _frameCount = 0;

            for (int i = 0; i < handCount; i++)
            {
                _hands.Add(Hand.CreateRandom(_random, CANVAS_WIDTH, CANVAS_HEIGHT));
            }

            IsRunning = true;
            Render();
        }

        /// <summary>
        /// Performs one simulation tick: move, resolve collisions, render.
        /// Returns frame count and winner information as tuple per constraint requirement.
        /// </summary>
        public (int frameCount, HandType? winner) Tick()//main core of the cose checking collision , doing rendering 
        {
            if (!IsRunning) return (_frameCount, null);

            _frameCount++;
            MoveHands();
            ResolveCollisions();
            Render();

            HandType? winner = _hands.GetWinner();
            if (winner.HasValue)
            {
                IsRunning = false;
            }

            return (_frameCount, winner);
        }
        private void MoveHands()
        {
            for (int i = 0; i < _hands.Count; i++)
            {
                Hand hand = _hands[i];
                PointF newPos = new PointF(hand.Position.X + hand.Velocity.X,hand.Position.Y + hand.Velocity.Y);

                //just checking the balls are in the boundreies of the gdi drawer
                if (newPos.X < Hand.HAND_SIZE || newPos.X > CANVAS_WIDTH - Hand.HAND_SIZE)
                {
                    hand.Velocity = new PointF(-hand.Velocity.X, hand.Velocity.Y);newPos.X = Math.Clamp(newPos.X, Hand.HAND_SIZE, CANVAS_WIDTH - Hand.HAND_SIZE);
                }

                if (newPos.Y < Hand.HAND_SIZE || newPos.Y > CANVAS_HEIGHT - Hand.HAND_SIZE)
                {
                    hand.Velocity = new PointF(hand.Velocity.X, -hand.Velocity.Y);newPos.Y = Math.Clamp(newPos.Y, Hand.HAND_SIZE, CANVAS_HEIGHT - Hand.HAND_SIZE);
                }

                hand.Position = newPos;
                _hands[i] = hand;
            }
        }
        private void ResolveCollisions()
        {
            for (int i = 0; i < _hands.Count - 1; i++)
            {
                for (int j = i + 1; j < _hands.Count; j++)
                {
                    if (Hand.CheckOverlap(_hands[i], _hands[j]))//if overlapping happens then true
                    {
                        HandType winnerType = Hand.DetermineWinner(_hands[i].Type, _hands[j].Type);

                        if (_hands[i].Type == winnerType)
                        {
                            Hand loser = _hands[j];
                            loser.Type = winnerType;//giving the winner
                            loser.Color = GetColorForType(winnerType);
                            _hands[j] = loser;//the other one is the looser eventually
                        }
                        else
                        {
                            Hand loser = _hands[i];
                            loser.Type = winnerType;
                            loser.Color = GetColorForType(winnerType);
                            _hands[i] = loser;
                        }
                        //if i is the winnder then it gonna go with the first on else the opponent won
                    }
                }
            }
        }

        private void Render()
        {
            _drawer.Clear();
            foreach (Hand hand in _hands)
            {
                _drawer.AddCenteredEllipse((int)hand.Position.X, (int)hand.Position.Y, (int)Hand.HAND_SIZE, (int)Hand.HAND_SIZE, hand.Color);
                _drawer.AddText(GetTextForType(hand.Type),15, (int)hand.Position.X - 5, (int)hand.Position.Y - 5, (int)Hand.HAND_SIZE/2, (int)Hand.HAND_SIZE/2, Color.White) ;
            
            }
           _drawer.Render();
        }
        private static Color GetColorForType(HandType type)
        {
            return type switch
            {
                HandType.Rock => Color.Red,
                HandType.Paper => Color.Green,
                HandType.Scissors => Color.Blue,
                _ => Color.Black
            };
        }
        public static string GetTextForType(HandType type)
        {
            return type switch
            {
                HandType.Rock => "R",
                HandType.Paper => "P",
                HandType.Scissors => "S",
                _ => ""
            };
        }

        public void Close()
        {
            _drawer.Close();
        }
    }
}
