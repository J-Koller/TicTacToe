using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.WinFormsApp.Services.Gui.Boxes
{
    public class BoxService : IBoxService
    {
        public void ClearGameBoard(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                button.Invoke(() =>
                {
                    button.Text = "";
                    button.Enabled = false;
                    button.ForeColor = Color.Transparent;
                    button.BackColor = Color.FromArgb(64, 64, 64);
                });
            }
        }

        public void LockGameBoard(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                button.Invoke(() => button.Enabled = false);
            }
        }

        public void UnlockGameBoard(List<Button> buttons)
        {
            IEnumerable<Button> blankButtons = buttons.Where(b => b.Text == "");

            foreach (Button button in blankButtons)
            {
                button.Invoke(() => button.Enabled = true);
            }
        }

        public void UpdateGameBoard(List<Button> buttons, MoveDto moveDto)
        {
            Button button = buttons.Single(b => (Point)b.Tag == moveDto.Coordinates);

            button.Invoke(() => 
            {
                ChangeBox(button, moveDto.Symbol);
            });
        }

        private void ChangeBox(Button button, string Symbol)
        {
            if (Symbol == "X")
            {
                button.ForeColor = Color.FromArgb(128, 255, 255);
            }
            else
            {
                button.ForeColor = Color.FromArgb(255, 128, 128);
            }

            button.Text = Symbol;
        }
    }
}
