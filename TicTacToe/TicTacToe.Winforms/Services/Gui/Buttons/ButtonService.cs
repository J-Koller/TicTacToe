using System.Windows.Forms;

namespace TicTacToe.WinFormsApp.Services.Gui.Buttons
{
    public class ButtonService : IButtonService
    {
        public void LockButtons(params Button[] buttons)
        {
            foreach(Button button in buttons)
            {
                button.Invoke(() =>
                {
                    button.Enabled = false;
                });
            }
        }

        public void UnlockButtons(params Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.Invoke(() =>
                {
                    button.Enabled = true;
                });
            }
        }
    }
}
