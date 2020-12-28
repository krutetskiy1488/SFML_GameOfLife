using System.Linq;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameLife
{
    class TextBox : Button
    {
        private string _name;
        private string _value;

        public TextBox(int x, int y, int height, int width, string text, Font font) 
            : base(x, y, height, width, text, font)
        {
            _value = string.Empty;
            _name = text;
            Active = false;
        }

        override public void OnBound(RenderWindow window)
        {
            var bounds = _text.GetGlobalBounds();
            var pos = Mouse.GetPosition(window);
            var time = _timer.ElapsedTime.AsSeconds();

            if (bounds.Contains(pos.X, pos.Y) && Mouse.IsButtonPressed(Mouse.Button.Left) && time > 0.1)
                Active = !Active;

            Fill();
        }

        public bool Update(string value)
        {
            var digit = value.All(c => char.IsDigit(c) || c == '\b' || c == '\r');


            if (Active && digit)
            {
                if (value == "\r")
                    return true;

                if (value == "\b" && _value.Length > 0)
                    _value = _value.Substring(0, _value.Length - 1);
                else if (value != "\b" && _value.Length < 5)
                    _value += value;
                
                _text.DisplayedString = _name + _value;
            }

            return false;
        }

        public int ParseToInt()
        {
            return int.Parse(_value);
        }


    }
}
