﻿namespace Forum.App
{
	using System;
	using System.Linq;

	using Contracts;

	public class ForumViewEngine : IForumViewEngine
	{
		private ConsoleColor backgroundColor;
		private ConsoleColor highlightColor;

        private ConsoleColor hardCodedBgColor = ConsoleColor.Gray;
        private ConsoleColor hardCodedHlColor = ConsoleColor.Green;
        private ConsoleColor hardCodedFontColor = ConsoleColor.Black;

        public ForumViewEngine()
		{
			this.InitializeConsole();
		}

        private ConsoleColor BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }

            set
            {
                this.backgroundColor = value;
            }
        }

        private ConsoleColor HighlightColor
        {
            get
            {
                return this.highlightColor;
            }

            set
            {
                ConsoleColor maxColor = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().Last();

                if (value > maxColor)
                {
                    value = (ConsoleColor)((int)value % (int)maxColor);
                }

                this.highlightColor = value;
            }
        }

        public void ResetBuffer()
		{
			this.Clear();
			Console.BufferHeight = 30;
		}

		public void SetBufferHeight(int rows)
		{
			Console.BufferHeight = rows;
		}

		public void RenderMenu(IMenu menu)
		{
			this.Clear();

			foreach (var label in menu.Labels.Where(l => !l.IsHidden))
			{
				this.DisplayLabel(label);
			}

			foreach (var button in menu.Buttons.Where(b => !b.IsHidden))
			{
				this.DisplayLabel(button);
			}

			if (menu is ITextAreaMenu textAreaMenu)
			{
				textAreaMenu.TextArea.Render();
			}
		}

        public void Mark(ILabel label, bool highlighted = true)
        {
            this.SetCursorPosition(label.Position.Left, label.Position.Top);

            //Highlight Option
            if (highlighted)
            {
                Console.BackgroundColor = this.HighlightColor;
            }

            Console.Write(label.Text);

            //Reset background color to prevent using 
            //the wrong background color for other actions
            Console.BackgroundColor = this.BackgroundColor;
        }

        private void InitializeConsole()
        {
            this.BackgroundColor = this.hardCodedBgColor;
            this.HighlightColor = this.hardCodedHlColor;

            Console.BackgroundColor = this.BackgroundColor;
            Console.ForegroundColor = this.hardCodedFontColor;

            Console.CursorVisible = false;
            Console.SetWindowSize(60, 30);
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            this.Clear();
        }

        private void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        private void DisplayLabel(ILabel label)
		{
			this.SetCursorPosition(label.Position.Left, label.Position.Top);
			Console.WriteLine(label.Text);
		}

		private void Clear()
		{
			Console.Clear();
			Console.CursorVisible = false;
		}
	}
}
