using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shumatbaev_XOGame
{
    public class Class1
    {
        public enum XOElement
        {
            None = 0, Cross = 1, Circle = -1
        }
        private const int rowCount = 3, colCount = 3;
        private XOElement[,] rawValues = new XOElement[rowCount, colCount];
        private XOElement lastTurn = XOElement.Circle;
        private XOElement winner = XOElement.None;
        public event Action<int, int, XOElement> OnTurn;
        public event Action<XOElement> OnGameOver;

        private XOElement reverseElement(XOElement element)
        {

            return (XOElement)(-(int)element);
        }


        public bool GameOver
        {
            get
            {
                    return winner != XOElement.None;
            }
        }
        public XOElement Winner
        {
            get
            {
                return winner;
            }

            set
            {
                winner = value;

                if(winner !=XOElement.None)
                {
                    OnGameOver?.Invoke(winner);
                }
            }
        }

        public bool CanTurn(int row, int col)
        {
       
            return rawValues[row, col] == XOElement.None && !GameOver;
        }

        public bool TryTurn(int row, int col)
        {
            if (!CanTurn(row, col))
            {
                return false;
            }

            rawValues[row, col] = reverseElement(lastTurn);
            lastTurn = rawValues[row, col];
            OnTurn?.Invoke(row, col, lastTurn);

            Winner = checkWinner(); 
            return true;
        }

        public char[,] Field
        {
            get
            {
                char[,] result = new char[rowCount, colCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (rawValues[i, j] == XOElement.Cross)
                            result[i, j] = 'X';
                        else if (rawValues[i, j] == XOElement.Circle)
                            result[i, j] = 'O';
                        else
                            result[i, j] = '-';
                    }
                }
                return result;
            }
        }
        private XOElement checkRows ()
        {
            for(int i=0; i<rowCount; i++)
            {
                int sum = 0;
                for(int j=0; j<colCount; j++)
                {
                    sum += (int) rawValues[i, j];
                }
                if(sum == 3)
                {
                    return XOElement.Cross;
                }
                if(sum == -3)
                {
                    return XOElement.Circle;
                }
            }
            return XOElement.None;
        }
        private XOElement checkWinner ()
        {
            var winner = checkRows();
            if(winner != XOElement.None)
            {
                return winner;
            }
            winner = checkCols();
            if(winner != XOElement.None)
            {
                return winner;
            }

            return XOElement.None;
        }
        public XOElement checkCols ()
        {
            for (int i = 0; i < colCount; i++)
            {
                int sum = 0;
                for (int j = 0; j < rowCount; j++)
                {
                    sum += (int) rawValues[j, i];
                }
                if (sum == 3)
                {
                    return XOElement.Cross;
                }
                if (sum == -3)
                {
                    return XOElement.Circle;
                }
            }
            return XOElement.None;
        }

    }
}

