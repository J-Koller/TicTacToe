using System.Windows.Forms;

namespace TicTacToe.WinFormsApp.Services.Gui.Buttons
{
    public interface IButtonService
    {
        void LockButtons(params Button[] buttons);

        void UnlockButtons(params Button[] buttons);
    }
}
