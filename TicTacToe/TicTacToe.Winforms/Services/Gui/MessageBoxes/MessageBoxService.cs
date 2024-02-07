using System;
using System.Windows.Forms;

namespace TicTacToe.WinFormsApp.Services.Gui.MessageBoxes
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowErrorBox(string text)
        {
            MessageBox.Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowErrorBox(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public DialogResult ShowQuestionBox(string text)
        {
            return MessageBox.Show(text, string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public DialogResult ShowQuestionBox(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void ShowInformationBox(string text)
        {
            MessageBox.Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowInformationBox(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowExclamatoryBox(string text)
        {
            MessageBox.Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ShowExclamatoryBox(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
