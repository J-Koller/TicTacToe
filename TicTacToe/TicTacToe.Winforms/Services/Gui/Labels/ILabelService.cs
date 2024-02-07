using System.Windows.Forms;

namespace TicTacToe.WinFormsApp.Services.Gui.Labels
{
    public interface ILabelService
    {
        void ClearText(params Label[] labels);

        void ChangeText(Label label, string text);

        void ChangeTextColorToRed(Label label);

        void ChangeTextColorToRed(Label label, string text);
        
        void ChangeTextColorToBlue(Label label);

        void ChangeTextColorToBlue(Label label, string text);
    }
}
