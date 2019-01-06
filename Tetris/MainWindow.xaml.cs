using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Game Loop
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        public void tick(Object sender, EventArgs e)
        {
            LineBlock b = new LineBlock(60, 0);
            b.renderblock(board);
        }
    }

    public class LineBlock
    {
        private int x;
        private int y;
        private int width;
        private int height;

        public LineBlock(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.width = 30 * 4;
            this.height = 40;
        }

        // Renders the position of the block into the canvas
        public void renderblock(Canvas canvas)
        {
            Rectangle r = new Rectangle();
            r.Fill = Brushes.Black;
            r.Width = this.width;
            r.Height = this.height;
            Canvas.SetTop(r, this.y);
            Canvas.SetLeft(r, this.x);
            canvas.Children.Add(r);
        }
    }
}
