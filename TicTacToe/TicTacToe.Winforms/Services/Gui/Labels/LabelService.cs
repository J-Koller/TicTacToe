using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToe.WinFormsApp.Services.Gui.Labels
{
    public class LabelService : ILabelService
    {
        public void ClearText(params Label[] labels)
        {
            foreach(Label label in labels)
            {
                label.Invoke(() =>
                {
                    label.Text = string.Empty;
                });
            }
        }

        public void ChangeText(Label label, string text)
        {
            label.Invoke(() =>
            {
                label.Text = text;
            });
        }

        public void ChangeTextColorToBlue(Label label)
        {
            label.Invoke(() =>
            {
                label.ForeColor = Color.FromArgb(128, 255, 255);
            });
        }

        public void ChangeTextColorToBlue(Label label, string text)
        {
            label.Invoke(() =>
            {
                label.ForeColor = Color.FromArgb(128, 255, 255);
                label.Text = text;
            });
        }

        public void ChangeTextColorToRed(Label label)
        {
            label.Invoke(() =>
            {
                label.ForeColor = Color.FromArgb(255, 128, 128);
            });
        }

        public void ChangeTextColorToRed(Label label, string text)
        {
            label.Invoke(() =>
            {
                label.ForeColor = Color.FromArgb(255, 128, 128);
                label.Text = text;
            });
        }
    }
}
