using System;
using System.Windows.Forms;
using CSat;
using OpenTK;

namespace CSatExamples
{
    class MainClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Open("log.txt");
            Settings.DataDir = "../data/model/";
            Settings.TextureDir = "../data/texture/";
            Settings.ShaderDir = "../data/shader/";
          
            if (true)
            {
                GameWindow game = new Game5(800, 600); 
                game.WindowState = OpenTK.WindowState.Normal; // .FullScreen;
                game.Run(30.0, 0.0);
                return;
            }
        }
    }
}
