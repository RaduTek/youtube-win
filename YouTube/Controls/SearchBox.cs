using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YouTube.Controls
{
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();

            ButtonText = "Search";
            textBox.GotFocus += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;
        }

        private string backKey = "SearchTextBox";
        private readonly int buttonTextMargin = 6;
        private bool darkTheme = false;

        public new event EventHandler<SearchBoxEventArgs> TextChanged;

        public event EventHandler<SearchBoxEventArgs> Search;

        public new string Text
        {
            get => textBox.Text;
            set { textBox.Text = value; }
        }

        public string ButtonText
        {
            get => button.Text;
            set
            {
                button.Text = value;
                button.Width = buttonTextMargin * 2 + TextRenderer.MeasureText(value, Font).Width;
            }
        }

        public bool DarkTheme
        {
            get => darkTheme;
            set
            {
                darkTheme = value;
                backKey = panel.BackKey = "SearchTextBox" + (value ? "Dark" : "");
                button.BackKey = "SearchButton" + (value ? "Dark" : "");
                textBox.BackColor = value ? Color.FromArgb(255, 39, 39, 39) : Color.White;
                textBox.ForeColor = button.ForeColor = value ? Color.FromArgb(255, 220, 220, 220) : Color.Black;
            }
        }

        private void RaiseSearchEvent()
        {
            Search?.Invoke(this, new SearchBoxEventArgs() { Text = textBox.Text });
        }

        private void backPanel_Click(object sender, EventArgs e)
        {
            textBox.Focus();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(this, new SearchBoxEventArgs() { Text = textBox.Text });
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                RaiseSearchEvent();
            }
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            panel.BackKey = backKey;
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            panel.BackKey = backKey + "_Focused";
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            RaiseSearchEvent();
        }
    }

    public class SearchBoxEventArgs : EventArgs
    {
        public string Text { get; set; }
    }
}
