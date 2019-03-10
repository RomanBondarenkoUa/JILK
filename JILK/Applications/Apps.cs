using JILK.Controls;
using JILK.Delegates;
using JILK.enums;
using JILK.Primitives;
using System;
using System.Text;

namespace JILK.Applications
{
    public class Apps : Window
    {
        #region buttons charmaps
        private static readonly string NotepadIconBitmap =
  @"                " +
  @" ,───────────.  " +
  @"(_\           \ " +
  @"   │     ~     │" +
  @"   │  readonly │" +
  @"   │  Notepad  │" +
  @"   │     ~     │" +
  @"  _│           │" +
  @" (_/_____(*)__/ " +
  @"          {{    " +
  @"           }}   ";

        private static readonly string FileExplorerBitmap =
  @"                " +
  @"                " +
  @" ______         " +
  @"│______\_______ " +
  @"|              |" +
  @"|     File     |" +
  @"|   explorer   |" +
  @"|______________|" +
  @"                " +
  @" (unavailable)  " +
  @"                ";


        private static readonly string TaskLauncherBitmap =
  @"            A   " +
  @"           /|\  " +
  @" __________| |  " +
  @"│    ~     |o|  " +
  @"│  Task    |o|  " +
  @"│ launcher |o|  " +
  @"│    ~     | │  " +
  @"│_________/   \ " +
  @" (will   / /┃\ \" +
  @"   be    | |┃| |" +
  @"  soon)  |/ ┃ \|";

        private static readonly string TutorialLauncherBitmap =

  @"      FAQ       " +
  @"      o         " +
  @"       o        " +
  @"     ___        " +
  @"     | |        " +
  @"     | |        " +
  @"     |o|        " +
  @"    .' '.       " +
  @"   /  o  \      " +
  @"  :____o__:     " +
  @"  '._____.'     " +
  @"                ";
        #endregion

        private readonly Surface _relatedSurface;

        private readonly Button TextReaderLaunchButton = new Button(NotepadIconBitmap, 13, 18, new Point(5, 5, 0), BorderStyle.None);
        private readonly Button FileExplorerLaunchButton = new Button(FileExplorerBitmap, 13, 18, new Point(26, 5, 0), BorderStyle.None);
        private readonly Button TaskLauncherLaunchButton = new Button(TaskLauncherBitmap, 13, 18, new Point(50, 5, 0), BorderStyle.None);
        private readonly Button TutorialLaunchButton = new Button(TutorialLauncherBitmap, 13, 18, new Point(77, 5, 0), BorderStyle.None);

        public Apps(int height, int width, string title, Point position, Surface relatedSurface) : base(height, width, title, position)
        {
            Redraw(true);
            _relatedSurface = relatedSurface;

            TextReaderLaunchButton.DefaultBorderStyle = BorderStyle.None;
            TextReaderLaunchButton.CursorClick_EventHandler += StartReader;
            base.AddChild(TextReaderLaunchButton);

            FileExplorerLaunchButton.DefaultBorderStyle = BorderStyle.None;
            base.AddChild(FileExplorerLaunchButton);

            TaskLauncherLaunchButton.DefaultBorderStyle = BorderStyle.None;
            base.AddChild(TaskLauncherLaunchButton);

            TutorialLaunchButton.DefaultBorderStyle = BorderStyle.None;
            TutorialLaunchButton.CursorClick_EventHandler += (object sender, CursorEventArgs e)
                => {_relatedSurface.ShowTutorial(); };
            base.AddChild(TutorialLaunchButton);
        }

        /// <summary>
        /// Starts notepad demo
        /// </summary>
        private void StartReader(object sender, EventArgs e)
        {
            Random rand = new Random();
            StringBuilder sampleText = new StringBuilder();
            // Just some random text generation
            for (int i = 0; i < 1500; i++)
            {
                sampleText.Append(((char)rand.Next(65,123)).ToString());
                if (rand.Next() % 7 == 0)
                    sampleText.Append(' ');
            }
            _relatedSurface.AddWindow(new Notepad(25, 80, "JILKpad V0.3", sampleText.ToString(), new Point(0, 0, 0)));
        }
    }
}
