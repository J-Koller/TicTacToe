using System.Collections.Generic;
using System.Windows.Forms;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.WinFormsApp.Services.Gui.Boxes
{
    public interface IBoxService
    {
        void ClearGameBoard(List<Button> buttons);

        void LockGameBoard(List<Button> buttons);

        void UnlockGameBoard(List<Button> buttons);

        void UpdateGameBoard(List<Button> buttons, MoveDto moveDto);
    }
}
