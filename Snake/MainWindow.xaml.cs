using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Collections.Generic;

namespace Snake
{
    public partial class MainWindow : Window
    {
        SnakePlayer snake = new SnakePlayer(40, 100);
        Food food = new Food(200, 200);
        public int directionX = 20;
        public int directionY = 0;
        public int prevdirectionX = -20;
        public int prevdirectionY = 0;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            // Start game loop
            timer.Tick += new EventHandler(tick);
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Start();
        }

        /// <summary>
        /// Event that runs when a key press is detected.
        /// </summary>
        public void keypress(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                directionX = 0;
                directionY = -20;
            } else if (e.Key == Key.Down)
            {
                directionX = 0;
                directionY = 20;
            } else if (e.Key == Key.Left)
            {
                directionX = -20;
                directionY = 0;
            } else if (e.Key == Key.Right)
            {
                directionX = 20;
                directionY = 0;
            }
            // Prevent snake from going into itself.
            if (directionX == -prevdirectionX && directionY == -prevdirectionY)
            {
                directionX = prevdirectionX;
                directionY = prevdirectionY;
            }
        }

        /// <summary>
        /// Event that defines what happens on each game tick.
        /// </summary>
        public void tick(Object sender, EventArgs e)
        {
            prevdirectionX = directionX;
            prevdirectionY = directionY;
            int snakeX = snake.getX();
            int snakeY = snake.getY();

            // Check snake collision with borders, food, or itself
            if (snakeX > board.ActualWidth || snakeX < 0 || snakeY < 0 || snakeY > board.ActualHeight)
            {
                // Game over
                timer.Stop();
            }
            if (snakeX == food.getX() && snakeY == food.getY())
            {
                snake.addBody();
                food.newLocation();
            }
            int snakeSize = snake.body.Count;
            for (int i = 0; i < snakeSize; i++)
            {
                int bodyX = snake.body[i].Item1;
                int bodyY = snake.body[i].Item2;
                if (snakeX == bodyX && snakeY == bodyY)
                {
                    // Game over
                    timer.Stop();
                }
            }

            // Update the snake body starting with the tail
            for (int i = snakeSize - 1; i > 0; i--)
            {
                int forwardX = snake.body[i - 1].Item1;
                int forwardY = snake.body[i - 1].Item2;
                snake.body[i] = new Tuple<int, int>(forwardX, forwardY);
            }
            snake.body[0] = new Tuple<int, int>(snakeX, snakeY);

            // Update the snake head
            int newX = snakeX + directionX;
            int newY = snakeY + directionY;
            snake.setX(newX);
            snake.setY(newY);

            render();
        }

        /// <summary>
        /// Renders the snake and food into the canvas.
        /// </summary>
        public void render()
        {
            board.Children.Clear();
            // Render the snake head
            Rectangle rhead = new Rectangle();
            rhead.Width = 20;
            rhead.Height = 20;
            rhead.Fill = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(rhead, snake.getX());
            Canvas.SetTop(rhead, snake.getY());

            board.Children.Add(rhead);

            // Render the snake body
            int snakeSize = snake.body.Count;
            for(int i = 0; i < snakeSize; i++)
            {
                int x = snake.body[i].Item1;
                int y = snake.body[i].Item2;
                Rectangle rbody = new Rectangle();
                rbody.Width = 20;
                rbody.Height = 20;
                rbody.Fill = new SolidColorBrush(Colors.Black);
                Canvas.SetLeft(rbody, x);
                Canvas.SetTop(rbody, y);
                board.Children.Add(rbody);
            }

            // Render the food
            Ellipse foodshape = new Ellipse();
            foodshape.Width = 20;
            foodshape.Height = 20;
            foodshape.Fill = new SolidColorBrush(Colors.Red);
            Canvas.SetLeft(foodshape, food.getX());
            Canvas.SetTop(foodshape, food.getY());
            board.Children.Add(foodshape);
        }
    }


    public class SnakePlayer
    {
        private int x;
        private int y;
        public List<Tuple<int, int>> body = new List<Tuple<int, int>>();
        public SnakePlayer(int x, int y)
        {
            this.x = x;
            this.y = y;
            body.Add(new Tuple<int, int>(x - 20, y));
            body.Add(new Tuple<int, int>(x - 40, y));
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public void addBody()
        {
            int size = body.Count;
            body.Add(body[size - 1]);
        }
    }


    public class Food
    {
        private int x;
        private int y;
        public Food(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public void newLocation()
        {
            Random rnd = new Random();
            int newX = rnd.Next(0, 30);
            int newY = rnd.Next(0, 30);
            this.x = newX * 20;
            this.y = newY * 20;
        }
    }
}
