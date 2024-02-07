using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe.WinFormsApp.Services.Gui.ControlCoordinates
{
    public class ControlTagCoordinatesService : IControlCoordinatesService
    {
        public Point GetCoordinatesByControl(Control control)
        {
            Point coordinates = (Point)control.Tag;

            return coordinates;
        }
    }
}
