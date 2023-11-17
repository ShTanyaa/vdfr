using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static shumatbaev_XOGame.Class1;

namespace shumatbaev_XOGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {

        private readonly double rectWidth;
        private readonly double rectHeight;
        private readonly Class1 XoGame = new Class1();

        
        public MainWindow ()
        {
            InitializeComponent();
            XoGame.OnTurn += (row, col, elem) =>
            {
                char symbol = elem == XOElement.Cross ? 'X' : 'O';
                printSymbol(row, col, symbol);
            };
            rectWidth = gameField.Width / 3;
            rectHeight = gameField.Height / 3;

            double y = 0;

            for (int i = 1; i <= 3; i++)
            {
                double x = 0;

                for (int j = 1; j <= 3; j++)
                {
                    var rect = new Rectangle();
                    rect.Stroke = Brushes.Black;
                    rect.StrokeThickness = 1;
                    rect.Height = rectHeight;
                    rect.Width = rectWidth;

                    Canvas.SetLeft(rect, x);
                    Canvas.SetTop(rect, y);

                    gameField.Children.Add(rect);

                    x += rectWidth;
                }
                y += rectHeight;
            }
        }
        private (int, int) calcCell (double x, double y)
        {
            int row = (int)(y / rectHeight);
            int col = (int)(x / rectWidth);

            return (row, col);
        }

        private void printSymbol(int row, int col, char symbol)
        {
            (double cornerX, double cornerY) = (rectHeight * col, rectWidth * row);

            Label text = new Label();
            text.Content = symbol;
            text.FontSize = rectWidth / 1.5;

            Canvas.SetLeft(text, cornerX + rectWidth / 4);
            Canvas.SetTop(text, cornerY);
            gameField.Children.Add(text);

           
        }

        private void makeTurn(object sender, MouseButtonEventArgs e)
        {
            (int row, int col) = calcCell(e.GetPosition(gameField).X, e.GetPosition(gameField).Y);
            XoGame.TryTurn(row, col);
        }

    }
}
