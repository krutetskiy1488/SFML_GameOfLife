using System;
using System.Collections.Generic;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameLife
{
    class Field : Transformable, Drawable
    {
        private int _width;
        private int _height;
        private int _left;
        private int _top;

        private List<List<Cell>> _field;

        public Field(int size, int x, int y, int width, int height)
        {
            _width = width;
            _height = height;
            _left = x;
            _top = y;

            _field = new List<List<Cell>>();

            var cellSize = width / size;

            for (int i = 0; i < size; i++)
            {
                _field.Add(new List<Cell>());
                for (int j = 0; j < size; j++)
                {

                    _field[i].Add(new Cell(x + i * cellSize, y + j * cellSize, cellSize));
                }
            }
        }

        public void OnBound(RenderWindow window)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                var pos = Mouse.GetPosition(window);
                for (int i = 0; i < _field.Count; i++)
                    for (int j = 0; j < _field[i].Count; j++)
                        _field[i][j].OnBound(pos);
            }
        }

        public void Update()
        {
            var upd = new List<List<Cell>>();
            for (int i = 0; i < _field.Count; i++)
            {
                upd.Add(new List<Cell>());
                for (int j = 0; j < _field[i].Count; j++)
                {
                    upd[i].Add(new Cell(_field[i][j]));
                }
            }

            for (int j = 0; j < _field.Count; j++)
            {
                for (int i = 0; i < _field[j].Count; i++)
                {
                    var cell = new Cell(_field[i][j]);
                    var alive = CountOfMour(_field, i, j, Cell.Type.Alive);
                    var dead = CountOfMour(_field, i, j, Cell.Type.Dead);

                    if (alive == 3 && cell.State == Cell.Type.Dead)
                        cell.State = Cell.Type.Alive;
                    else if((alive < 2 || alive > 3) && cell.State == Cell.Type.Alive)
                        cell.State = Cell.Type.Dead;

                    upd[i][j] = cell;
                    upd[i][j].Update();
                }
            }

            _field = upd;
        }

        public void Clear()
        {
            for (int i = 0; i < _field.Count; i++)
            {
                for (int j = 0; j < _field[i].Count; j++)
                {
                    _field[i][j].State = Cell.Type.Dead;
                    _field[i][j].Update();
                }
            }
        }

        public int CountOfMour(List<List<Cell>> place, int i, int j, Cell.Type type)
        {
            int size = place.Count;
            int count = 0;

            if (i - 1 >= 0   && j - 1 >= 0   && place[i - 1][j - 1].State == type) count++;
            if (j - 1 >= 0                   && place[i    ][j - 1].State == type) count++;
            if (i + 1 < size && j - 1 >= 0   && place[i + 1][j - 1].State == type) count++;
            if (i + 1 < size                 && place[i + 1][j    ].State == type) count++;
            if (i + 1 < size && j + 1 < size && place[i + 1][j + 1].State == type) count++;
            if (j + 1 < size                 && place[i    ][j + 1].State == type) count++;
            if (i - 1 >= 0   && j + 1 < size && place[i - 1][j + 1].State == type) count++;
            if (i - 1 >= 0                   && place[i - 1][j    ].State == type) count++;

            return count;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < _field.Count; i++)
                for (int j = 0; j < _field[i].Count; j++)
                    target.Draw(_field[i][j]);
        }

        public void Resize(int size)
        {
            _field = new List<List<Cell>>();

            var cellSize = _width / size;

            for (int i = 0; i < size; i++)
            {
                _field.Add(new List<Cell>());
                for (int j = 0; j < size; j++)
                {

                    _field[i].Add(new Cell(_left + i * cellSize, _top + j * cellSize, cellSize));
                }
            }
        }

        public void Print()
        {
            Console.WriteLine(CountOfMour(_field, 1, 0, Cell.Type.Alive));
        }
    }
}
