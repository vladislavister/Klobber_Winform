using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clobber__alpha_beta_pruning
{
    public partial class Game : Form
    {
        const int mapWidth = 5;
        const int mapHeight = 6;
        const int cellSize = 75;
        char[,] map = new char[mapHeight, mapWidth];
        Button prevButton;
        Button[,] buttons;
        Image blackFigure, whiteFigure;
        public  (int X, int Y)[] fieldsToMove = new (int X, int Y)[]
        {
             (-1, 0), (0, 1), (1, 0), (0, -1)
        };
        public Game()
        {
            InitializeComponent();

            blackFigure = new Bitmap(new Bitmap(@"C:\Users\Влад\Desktop\Klobber\b.png"),
                new Size(cellSize - 15, cellSize - 15));
            whiteFigure = new Bitmap(new Bitmap(@"C:\Users\Влад\Desktop\Klobber\w.png"),
               new Size(cellSize - 15, cellSize - 15));
            Text = "Clobber";
            Init();
        }
        public void Init()
        {
            prevButton = null;

            map = new char[,]
            {
                { 'P','C','P','C','P'},
                { 'C','P','C','P','C'},
                { 'P','C','P','C','P'},
                { 'C','P','C','P','C'},
                { 'P','C','P','C','P'},
                { 'C','P','C','P','C'}
            };
            CreateMap();
        }
        public void CreateMap()
        {
            Width = (cellSize * mapWidth) + 16;
            Height = (cellSize * mapHeight) + 39;

            buttons = new Button[mapHeight, mapWidth];

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    Button button = new Button();
                    buttons[i, j] = button;
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.Click += new EventHandler(PlayerTurnToMove);

                    if (map[i, j] == 'C')
                        button.Image = blackFigure;
                    else if (map[i, j] == 'P')
                        button.Image = whiteFigure;

                    button.BackColor = Color.White;
                    if (i % 2 != 0)
                        if (j % 2 != 0)
                            button.BackColor = Color.Gray;
                    if (i % 2 == 0)
                        if (j % 2 == 0)
                            button.BackColor = Color.Gray;
                    Controls.Add(button);
                }
            }
        }
        public void PlayerTurnToMove(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            if (prevButton == null)
            {
                if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 'P')
                {
                    prevButton = pressedButton;
                    pressedButton.BackColor = Color.Red;
                }
            }
            else
            {
                prevButton.BackColor = GetPrevButtonColor();
                if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 'C' 
                    && MoveLimitation((prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize),
                    (pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize)))
                {
                    char buff = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                    map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] =
                        map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize];
                    map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize] = '0';
                    pressedButton.Image = prevButton.Image;

                    prevButton.Image = null;
                    Update();
                    if (!GameOver('P'))
                    {
                        ComputerTurnToMove();
                        GameOver('C');
                    }
                        
                }
                prevButton = null;
            }
        }
        public void ComputerTurnToMove()
        {
            map = AI.MiniMax(map, 5, -999999999999999, 999999999999999, true).Item1;
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i, j] == 'C')
                        buttons[i, j].Image = blackFigure;
                    else if (map[i, j] == 'P')
                        buttons[i, j].Image = whiteFigure;
                    else
                        buttons[i, j].Image = null;
                }
            }
            Update();
        }
        public bool MoveLimitation((int, int) from, (int, int) to)
        {
            for (int i = 0; i < fieldsToMove.Length; i++)
                if (from.Item1 - to.Item1 == fieldsToMove[i].X && from.Item2 - to.Item2 == fieldsToMove[i].Y)
                    return true;
            return false;
        }
        public Color GetPrevButtonColor()
        {
            if ((prevButton.Location.Y / cellSize) % 2 != 0)
                if ((prevButton.Location.X / cellSize) % 2 != 0)
                    return Color.Gray;

            if ((prevButton.Location.Y / cellSize) % 2 == 0)
                if ((prevButton.Location.X / cellSize) % 2 == 0)
                    return Color.Gray;

            return Color.White;
        }
        public bool GameOver(char symbol)
        {
            bool result = AI.CheckWinner(map, symbol);

            if (result)
            {
                for (int i = 0; i < mapHeight; i++)
                    for (int j = 0; j < mapWidth; j++)
                        buttons[i, j].Enabled = false;

                /*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                 * 
                if (symbol == 'P')
                    Сообщить о победе
                if (symbol == 'C')
                    Сообщить о поражении
                *
                !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/
            }
            return result;
        }
    }
}
