using System;
using System.Collections.Generic;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameLife
{
    class Core
    {
        private Dictionary<string, Button> _buttons;

        private RenderWindow _window;
        private Field _field;
        private Clock _timer;

        private static int _speed = 200;
        private static bool _active = false;

        public Core(RenderWindow window)
        {
            _window = window;
            _field = new Field(21, 20, 100, 800, 800);
            _timer = new Clock();

            Font font = new Font("../../../resources/fonts/times-new-roman.ttf");
            _buttons = new Dictionary<string, Button>();
            _buttons["start"] = new Button (20 , 20 , 50, 100, "Start", font);
            _buttons["clear"] = new Button (130, 20 , 50, 110, "Clear", font);
            _buttons["1x"]    = new Button (250, 20 , 50, 50 , "1x"   , font);
            _buttons["2x"]    = new Button (370, 20 , 50, 50 , "2x"   , font);
            _buttons["3x"]    = new Button (490, 20 , 50, 50 , "3x"   , font);
            _buttons["size"]  = new TextBox(610, 20 , 50, 210, "Size: ", font);
        }

        public void Run()
        {
            _window.Closed += OnClosed;
            _window.TextEntered += EnterText;

            while (_window.IsOpen)
            {
                _window.DispatchEvents();

                Events();
                Update();
                Render();
            }
        }

        private void EnterText(object sender, TextEventArgs e)
        {
            var text = e.Unicode;

            var box = _buttons["size"] as TextBox;
            
            var resized = box.Update(text);
            if (resized)
                _field.Resize(box.ParseToInt());
        }

        private void OnClosed(object sender, EventArgs e)
        {
            var win = (RenderWindow)sender;
            win.Close();
        }

        public void Events()
        {
            _field.OnBound(_window);
            foreach (var key in _buttons.Keys)
                _buttons[key].OnBound(_window);

            if (_buttons["start"].IsPressed(_window))
                _active = !_active;
            if (_buttons["clear"].IsPressed(_window))
            { _field.Clear(); _active = false; }
            if (_buttons["1x"].IsPressed(_window))
                _speed = 200;
            if (_buttons["2x"].IsPressed(_window))
                _speed = 100;
            if (_buttons["3x"].IsPressed(_window))
                _speed = 1;
        }

        public void Update()
        {
            int time = _timer.ElapsedTime.AsMilliseconds();

            if (_active && time > _speed)
            {
                _timer.Restart();
                _field.Update();
            }
        }

        public void Render()
        {
            _window.Clear(Color.White);

            _window.Draw(_field);
            foreach (var key in _buttons.Keys)
                _window.Draw(_buttons[key]);

            _window.Display();
        }
    }
}
