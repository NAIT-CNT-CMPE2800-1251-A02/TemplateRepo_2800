namespace LAB_FINAL_01
{
    
        // Program.cs
        static class Program
        {
            [STAThread]
            static void Main()
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
        }
    
}