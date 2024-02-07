using System.Windows.Forms;

namespace TicTacToe.WinFormsApp.Services.Gui.MessageBoxes
{
    public interface IMessageBoxService
    {
        void ShowErrorBox(string text);
        
        void ShowErrorBox(string text, string caption);

        DialogResult ShowQuestionBox(string text);

        DialogResult ShowQuestionBox(string text, string caption);

        void ShowInformationBox(string text);

        void ShowInformationBox(string text, string caption);

        void ShowExclamatoryBox(string text);

        void ShowExclamatoryBox(string text, string caption);
    }
}
