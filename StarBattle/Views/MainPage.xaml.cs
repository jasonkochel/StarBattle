using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using StarBattle.Core.Models;
using StarBattle.Helpers;

namespace StarBattle.Views
{
    public partial class MainPage : Page
    {
        private Shape _currentShape;

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;

            for (var r = 0; r < 9; r++)
            {
                for (var c = 0; c < 9; c++)
                {
                    var cell = BuildCell(c, r);
                    cell.MouseLeftButtonUp += CellOnMouseLeftButtonUp;
                    Grid.SetColumn(cell, c);
                    Grid.SetRow(cell, r);
                    Grid.Children.Add(cell);
                }
            }
        }

        private void CellOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border clickedCell)
            {
                if (_currentShape == null) _currentShape = new Shape();

                var xy = clickedCell.Name.Substring(1).Split('C');
                var clickedRow = int.Parse(xy[0]);
                var clickedCol = int.Parse(xy[1]);
                _currentShape.ToggleCell(clickedRow, clickedCol);

                clickedCell.Background = new SolidColorBrush(Hues.White);
                clickedCell.BorderThickness = new Thickness(1, 1, clickedCol == 8 ? 1 : 0, clickedRow == 8 ? 1 : 0);

                foreach (var c in _currentShape.Cells)
                {
                    var uiCell = (Border)Grid.Children[(c.Row * 9) + c.Col];
                    var bt = c.BorderTop ? 3 : 1;
                    var bb = (c.Row == 8 ? 1 : 0) + (c.BorderBottom ? 2 : 0);
                    var bl = c.BorderLeft ? 3 : 1;
                    var br = (c.Col == 8 ? 1 : 0) + (c.BorderRight ? 2 : 0);

                    uiCell.BorderThickness = new Thickness(bl, bt, br, bb);
                    uiCell.Background = new SolidColorBrush(Hues.LtGray);
                }
            }
        }

        private static Border BuildCell(int c, int r)
        {
            return new Border
            {
                Name = $"R{r}C{c}",
                BorderBrush = new SolidColorBrush(Hues.Black),
                BorderThickness = new Thickness(1, 1, c == 8 ? 1 : 0, r == 8 ? 1 : 0),
                Background = new SolidColorBrush(Hues.White),
                Height = 64,
                Width = 64,
            };
        }
    }
}
