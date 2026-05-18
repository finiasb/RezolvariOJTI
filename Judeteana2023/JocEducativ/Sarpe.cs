using System.Data.SqlClient;

namespace JocEducativ
{
    public partial class SarpeEducativ : Form
    {
        private string constr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={AppDomain.CurrentDomain.BaseDirectory}JocEducativ.mdf;Integrated Security=True;Connect Timeout=30";
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        Random random = new Random();
        private System.Windows.Forms.Timer timerMove;
        private readonly string _email;
        private int GRID_SIZE = 20, CELL_SIZE = 25;
        private int[,] grid = new int[20, 20]; // [height, width]
        private List<Point> snake = new List<Point>();
        private Point food;
        private int xHead, yHead;
        private int scorTotal = 0;
        private int _punctajBonus;
        //snake = 1
        //food = 2 
        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private Direction direction;
        public SarpeEducativ(string email)
        {
            InitializeComponent();
            _email = email;
            direction = Direction.Down;
            DrawGame();
            timerMove = new System.Windows.Forms.Timer();
            timerMove.Interval = 1000;
            timerMove.Tick += timer_tickk;
            stopBtn.Enabled = false;
        }

        private bool IsKeyPressed(Keys key)
        {
            return GetAsyncKeyState((int)key) != 0;
        }

        public void AdaugaPunctajBonus(int punctajBonus)
        {
            _punctajBonus = punctajBonus;
            scorTotal += punctajBonus;
            lblPunctaj.Text = scorTotal.ToString();
        }

        private void timer_tickk(object sender, EventArgs e)
        {
            if (IsKeyPressed(Keys.A) && (direction == Direction.Up || direction == Direction.Down))
                direction = Direction.Left;
            else if (IsKeyPressed(Keys.D) && (direction == Direction.Up || direction == Direction.Down))
                direction = Direction.Right;
            else if (IsKeyPressed(Keys.W) && (direction == Direction.Left || direction == Direction.Right))
                direction = Direction.Up;
            else if (IsKeyPressed(Keys.S) && (direction == Direction.Left || direction == Direction.Right))
                direction = Direction.Down;

            Point head = snake[0];
            Point newHead = head;
            switch (direction)
            {
                case Direction.Up:
                    newHead.Y--; break;
                case Direction.Down:
                    newHead.Y++; break;
                case Direction.Left:
                    newHead.X--; break;
                case Direction.Right:
                    newHead.X++; break;
            }


            if (newHead.X < 0 || newHead.X >= GRID_SIZE || newHead.Y < 0 || newHead.Y >= GRID_SIZE)
            {
                timerMove.Stop();
                MessageBox.Show($"Ai murit! Scorul tău a fost: {scorTotal}");
                this.Close();
                gridClean();
                inserare();
                AlegeJoc alege = new AlegeJoc(_email);
                alege.Show();
                return;
            }

            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[i] == newHead)
                {
                    timerMove.Stop();
                    MessageBox.Show($"Ai murit! Scorul tău a fost: {scorTotal}");
                    inserare();
                    this.Close();
                    AlegeJoc alege = new AlegeJoc(_email);

                    alege.Show();
                    gridClean();
                    return;
                }
            }

            snake.Insert(0, newHead);
            if (newHead == food)
            {
                scorTotal = scorTotal + 10 + _punctajBonus;
                lblPunctaj.Text = "Punctaj: " + scorTotal.ToString();
                GenerateFood();


                timerMove.Stop();
                ShowQuestion();
            }
            else
                snake.RemoveAt(snake.Count - 1);

            gridClean();

            grid[food.X, food.Y] = 2;
            if (snake.Contains(food))
            {
                MessageBox.Show("da");
            }
            foreach (var p in snake)
                grid[p.X, p.Y] = 1;

            DrawGame();
        }
        private void gridClean()
        {
            for (int x = 0; x < GRID_SIZE; x++)
                for (int y = 0; y < GRID_SIZE; y++)
                    grid[x, y] = 0;
        }

        private void inserare()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into rezultate(TipJoc, EmailUtilizator, PunctajJoc) values(@TipJoc, @EmailUtilizator, @PunctajJoc)", con);
            cmd.Parameters.AddWithValue("@TipJoc", "1");
            cmd.Parameters.AddWithValue("@EmailUtilizator", _email);
            cmd.Parameters.AddWithValue("@PunctajJoc", scorTotal);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void ShowQuestion()
        {
            Întrebare formIntrebare = new Întrebare(this);
            formIntrebare.ShowDialog();
            timerMove.Start();
        }
        private void START()
        {
            timerMove.Start();
            gridClean();
            snake.Clear();

            direction = Direction.Down;
            xHead = random.Next(1, GRID_SIZE);
            yHead = random.Next(1, GRID_SIZE / 2);

            Point head = new Point(xHead, yHead);
            snake.Add(head);


            food = new Point(random.Next(GRID_SIZE), random.Next(GRID_SIZE / 2, GRID_SIZE));
            grid[food.X, food.Y] = 2;
            DrawGame();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            AlegeJoc alege = new AlegeJoc(_email);

            alege.Show();
        }
        private void GenerateFood()
        {
            Point newFood;
            do
            {
                newFood = new Point(random.Next(GRID_SIZE), random.Next(GRID_SIZE));
            } while (snake.Contains(newFood));

            food = newFood;
        }
        private void DrawGame()
        {
            Bitmap bmp = new Bitmap(panelGame.Width, panelGame.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);


                for (int x = 0; x < GRID_SIZE; x++)
                {
                    for (int y = 0; y < GRID_SIZE; y++)
                    {
                        Rectangle rect = new Rectangle(x * CELL_SIZE, y * CELL_SIZE, CELL_SIZE, CELL_SIZE);

                        if (grid[x, y] == 1)
                        {
                            if (snake[0].X == x && snake[0].Y == y)
                            {
                                g.FillEllipse(Brushes.White, rect);
                            }
                            else
                            {
                                g.FillEllipse(Brushes.Green, rect);
                            }
                        }

                        if (grid[x, y] == 2)
                            g.FillEllipse(Brushes.Red, rect);

                        g.DrawRectangle(Pens.Gray, rect);
                    }
                }
            }

            panelGame.BackgroundImage = bmp;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            START();
            startBtn.Enabled = false;
            stopBtn.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }
    }
}
