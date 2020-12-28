using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameLife
{
    class Cell : Transformable, Drawable
    {
        public enum Type {
            Alive,
            Dead
        }

        public Type State;
        private RectangleShape _cell;

        public Cell(int x, int y, int size)
        {
            State = Type.Dead;

            _cell = new RectangleShape(new Vector2f(size, size));
            _cell.Position = new Vector2f(x, y);
            _cell.FillColor = Color.White;
            _cell.OutlineColor = Color.Black;
            _cell.OutlineThickness = 2;
        }

        public Cell(Cell cell)
        {
            State = cell.State;
            _cell = cell._cell;
        }

        public void Update()
        {
            if(State == Type.Alive)
                _cell.FillColor = Color.Black;
            else if(State == Type.Dead)
                _cell.FillColor = Color.White;
        }

        public void OnBound(Vector2i pos)
        {
            var bounds = _cell.GetGlobalBounds();

            if (bounds.Contains(pos.X, pos.Y))
            {
                State = Type.Alive;
                Update();
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_cell);
        }
    }
}
