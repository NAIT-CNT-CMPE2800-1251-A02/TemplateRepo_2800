using GDIDrawer;
using static LAB_FINAL_01.HandS;

namespace LAB_FINAL_01
{
    public partial class Form1 : Form
    {

          SimulationManager _simulation;
          System.Windows.Forms.Timer _timer;
          int _animation_Interval = SimulationManager.DEFAULT_INTERVAL;


          Dictionary<HandType, Color> _colorMap = new() { { HandType.Rock, Color.Red }, { HandType.Paper, Color.Green }, { HandType.Scissors, Color.Blue } };


        public void Initialt()
        {
           

            lblInterval.Text = $"Interval: {_animation_Interval}ms (Use Mouse Wheel)";
            Controls.AddRange(new Control[] { lblHandCount, numHandCount, btnStart, lblStatus, lblInterval });
            btnStart.Click += (s, e) => StartSimulation((int)numHandCount.Value);
            numHandCount.ValueChanged += (s, e) => lblStatus.Text = "Ready to start";
            Text = "Rock-Paper-Scissors Simulation";
        }

        public Form1()
        {
            InitializeComponent();
            Initialt();
            _simulation = new SimulationManager();
            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += Timer_Tick;
            UpdateInterval();

            // Set up mouse wheel event for interval control
            MouseWheel += MainForm_MouseWheel;
        }


        private void StartSimulation(int handCount)
        {
            try
            {
                _simulation.Initialize(handCount);
                _timer.Start();
                UpdateStatus($"Running: {handCount} hands");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            // Ask simulation to move & resolve
            var (frame, winner) = _simulation.Tick();
            
            // Clear the window each frame
            _simulation._drawer.Clear();

            // Draw all hands
            foreach (HandS.Hand hand in _simulation.Hands)
            {
                Color color = _colorMap[hand.Type];
                _simulation._drawer.AddCenteredEllipse((int)hand.Position.X,(int)hand.Position.Y,Hand.SIZE,Hand.SIZE,color);
                _simulation._drawer.AddText(SimulationManager.GetTextForType(hand.Type), 15, (int)hand.Position.X - 5, (int)hand.Position.Y - 5, (int)Hand.HAND_SIZE / 2, (int)Hand.HAND_SIZE / 2, Color.White);

            }

            lblStatus.Text = $"Frame: {frame}";

            if (winner != null)
            {
                lblStatus.Text += $" | Winner: {winner}";
                _timer.Stop();
                
            }
        }

        private void MainForm_MouseWheel(object? sender, MouseEventArgs e)
        {
            // Mouse wheel control for animation interval
            _animation_Interval += e.Delta > 0 ? -5 : 5;
            _animation_Interval = Math.Clamp(_animation_Interval, SimulationManager.MIN_INTERVAL, SimulationManager.MAX_INTERVAL);
            UpdateInterval();
        }


        private void UpdateInterval()
        {
            _timer.Interval = _animation_Interval;
            if (Controls.Count > 0 )
                lblInterval.Text = $"Interval: {_animation_Interval}ms (Use Mouse Wheel)";
        }

        private void UpdateStatus(string status)
        {
            if (Controls.Count > 2 && Controls[3] is Label lbl)
                lbl.Text = status;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            _simulation.Close();
            System.Environment.Exit(0);
        }
    }








}
