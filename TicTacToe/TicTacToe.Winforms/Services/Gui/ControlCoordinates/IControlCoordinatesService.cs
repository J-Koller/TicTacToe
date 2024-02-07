using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe.WinFormsApp.Services.Gui.ControlCoordinates
{
    public interface IControlCoordinatesService
    {
        Point GetCoordinatesByControl(Control control);
    }
}
