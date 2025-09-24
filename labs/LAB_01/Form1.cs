using GDIDrawer;
using static LAB_FINAL_01.HandS;

////////////////////////////////////////////////////////////
// LAb #01 -Review Lab
// Parmanvir Singh - Sep/2025
// Submission Code : CMPE2800_1232_Lab_01f 
////////////////////////////////////////////////////////////
namespace LAB_FINAL_01
{
    public partial class Form1 : Form
    {

          SimulationManager _simulation;
          System.Windows.Forms.Timer _timer;//creating the timer
          int _animation_Interval = SimulationManager.DEFAULT_INTERVAL;//getting the interval from default val 


          Dictionary<HandType, Color> _colorMap = new() { { HandType.Rock, Color.Red }, { HandType.Paper, Color.Green }, { HandType.Scissors, Color.Blue } };//making the manual dictionary 


        public void Initialt()
        {
           
            //setting the form things like label text
            lblInterval.Text = $"Interval: {_animation_Interval}ms (Use Mouse Wheel)";
            btnStart.Click += (s, e) => StartSimulation((int)numHandCount.Value);
            numHandCount.ValueChanged += (s, e) => lblStatus.Text = "Ready to start";//aslo adding to even args like e
            Text = "Rock-Paper-Scissors Simulation";
        }

        public Form1()
        {
            InitializeComponent();
            Initialt();
            _simulation = new SimulationManager();//setting the class
            _timer = new System.Windows.Forms.Timer();//timer
            _timer.Tick += Timer_Tick;//adding the delegate
            UpdateInterval();//for the interval
            // Set up mouse wheel event for interval control
            MouseWheel += MainForm_MouseWheel;
        }


        private void StartSimulation(int handCount)
        {
            try
            {
                _simulation.Initialize(handCount);//giving the count 
                _timer.Start();
                UpdateStatus($"Running: {handCount} hands");//just the label
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
                //doing the same thing like in the other class render method do draw and text on ball
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
        /// <summary>
        /// Mouse wheel this is doing the interval speed default is 50
        /// from each chanhge it gonna change by +5_-5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseWheel(object? sender, MouseEventArgs e)
        {
            // Mouse wheel control for animation interval
            _animation_Interval += e.Delta > 0 ? -5 : 5;
            _animation_Interval = Math.Clamp(_animation_Interval, SimulationManager.MIN_INTERVAL, SimulationManager.MAX_INTERVAL);//making it in the range 
            UpdateInterval();
        }

        /// <summary>
        /// just changing the text of the borm label making it in fuction
        /// just just it handy
        /// </summary>
        private void UpdateInterval()
        {
            _timer.Interval = _animation_Interval;       
            lblInterval.Text = $"Interval: {_animation_Interval} ms (Use Mouse Wheel)";
        }
        /// <summary>
        /// same for this one 
        /// </summary>
        /// <param name="status"></param>
        private void UpdateStatus(string status)
        {
            
                lblStatus.Text = status;
        }

        /// <summary>
        /// Exiting the program if exit button is click addition just some times
        /// cause problem if not fully close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            _simulation._drawer.Close();
            System.Environment.Exit(0);
        }
    }








}
