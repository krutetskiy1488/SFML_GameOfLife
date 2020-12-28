using System;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameLife
{
    class Button : Transformable, Drawable
    {
        protected Text _text;
        protected Clock _timer;
        
        private RectangleShape _bound;

        public bool Active;


        public Button(int x, int y, int height, int width, string text, Font font)
        {
            _text = new Text(text, font, 40);
            _text.Position = new Vector2f(x, y);
            _text.FillColor = Color.Black;

            _bound = new RectangleShape();
            _bound.Size = new Vector2f(width, height);
            _bound.Position = new Vector2f(x, y);
            _bound.FillColor = Color.Green;
            _bound.OutlineThickness = 2;
            _bound.OutlineColor = Color.Black;

            _timer = new Clock();
        }

        virtual public void OnBound(RenderWindow window)
        {
            var bounds = _bound.GetGlobalBounds();
            var pos = Mouse.GetPosition(window);

            if (bounds.Contains(pos.X, pos.Y))
                Active = true;
            else
                Active = false;

            Fill();
        }

        protected void Fill()
        {
            if (Active)
                _text.FillColor = Color.Blue;
            else
                _text.FillColor = Color.Black;
        }

        public bool IsPressed(RenderWindow window)
        {
            var bounds = _bound.GetGlobalBounds();
            var pos = Mouse.GetPosition(window);
            var time = _timer.ElapsedTime.AsSeconds();

            if (bounds.Contains(pos.X, pos.Y) && Mouse.IsButtonPressed(Mouse.Button.Left) && time > 0.1)
            {
                _timer.Restart();
                return true;
            }
            else
                return false;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_bound);
            target.Draw(_text);
        }

        public virtual void Update() { }
    }
}
