using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloLogic
{
    class Board
    {
        private string[,] m_board;
        private int m_HeightAndWidth;
        private int m_amountOfBlankCells;

        public int getHeightAndWidth()
        {
            return m_HeightAndWidth;
        }

        public string[,] GetStartingBoard(int i_HeightAndWidth)
        {
            m_HeightAndWidth = i_HeightAndWidth;
            m_amountOfBlankCells = (m_HeightAndWidth * m_HeightAndWidth) - 4;
            int CorrectInitialPlace = i_HeightAndWidth / 2;
            m_board = new string[i_HeightAndWidth, i_HeightAndWidth];
            m_board[CorrectInitialPlace - 1, CorrectInitialPlace - 1] = "O";
            m_board[CorrectInitialPlace, CorrectInitialPlace - 1] = "X";
            m_board[CorrectInitialPlace - 1, CorrectInitialPlace] = "X";
            m_board[CorrectInitialPlace, CorrectInitialPlace] = "O";

            return m_board;
        }

        public List<Utilities.ValidStep> getAllValidSteps(char i_shape)
        {
            Utilities.ValidStep curretnValidStep;
            Utilities.Point currentPoint;
            string stringShape = i_shape.ToString();

            List<Utilities.ValidStep> AllValidSteps = new List<Utilities.ValidStep>();

            for (int j = 0; j < m_HeightAndWidth; ++j)
            {
                for (int i = 0; i < m_HeightAndWidth; ++i)
                {
                    if (isAValidPoint(i, j, stringShape))
                    {
                        currentPoint = new Utilities.Point(i, j);
                        curretnValidStep = new Utilities.ValidStep(currentPoint);

                        checkAllDirectionsOfPoint(curretnValidStep, currentPoint, stringShape);
                        AllValidSteps.Add(curretnValidStep);
                    }
                }
            }

            return AllValidSteps;
        }

        private void checkAllDirectionsOfPoint(Utilities.ValidStep i_validStep, Utilities.Point i_currentPoint, string i_stringShape)
        {

            if (checkContinueOfSequence(i_currentPoint, (int)Direction.LEFT, (int)Direction.NoChange, i_stringShape))
            {
                i_validStep.AddDirection(new Utilities.Point((int)Direction.LEFT, (int)Direction.NoChange));
            }

            if (checkContinueOfSequence(i_currentPoint, (int)Direction.LEFT, (int)Direction.UP, i_stringShape))
            {
                i_validStep.AddDirection(new Utilities.Point((int)Direction.LEFT, (int)Direction.UP));
            }

            if (checkContinueOfSequence(i_currentPoint, (int)Direction.NoChange, (int)Direction.UP, i_stringShape))
            {
                i_validStep.AddDirection(new Utilities.Point((int)Direction.NoChange, (int)Direction.UP));
            }

            if (checkContinueOfSequence(i_currentPoint, (int)Direction.RIGHT, (int)Direction.UP, i_stringShape))
            {
                i_validStep.AddDirection(new Utilities.Point((int)Direction.RIGHT, (int)Direction.UP));
            }

            if (checkContinueOfSequence(i_currentPoint, (int)Direction.RIGHT, (int)Direction.NoChange, i_stringShape))
            {
                i_validStep.AddDirection(new Utilities.Point((int)Direction.RIGHT, (int)Direction.NoChange));
            }

            if (checkContinueOfSequence(i_currentPoint, (int)Direction.RIGHT, (int)Direction.DOWN, i_stringShape))
            {
                i_validStep.AddDirection(new Utilities.Point((int)Direction.RIGHT, (int)Direction.DOWN));
            }

            if (checkContinueOfSequence(i_currentPoint, (int)Direction.NoChange, (int)Direction.DOWN, i_stringShape))
            {
                i_validStep.AddDirection(new Utilities.Point((int)Direction.NoChange, (int)Direction.DOWN));
            }

            if (checkContinueOfSequence(i_currentPoint, (int)Direction.LEFT, (int)Direction.DOWN, i_stringShape))
            {
                i_validStep.AddDirection(new Utilities.Point((int)Direction.LEFT, (int)Direction.DOWN));
            }

        }

        private bool isAValidPoint(int i, int j, string i_shape)
        {
            if ((m_board[i, j] != null))
            {
                return false;
            }

            Utilities.Point currentPoint = new Utilities.Point(i, j);

            return (checkContinueOfSequence(currentPoint, (int)Direction.LEFT, (int)Direction.NoChange, i_shape)) ||

            (checkContinueOfSequence(currentPoint, (int)Direction.LEFT, (int)Direction.UP, i_shape)) ||

            (checkContinueOfSequence(currentPoint, (int)Direction.NoChange, (int)Direction.UP, i_shape)) ||

            (checkContinueOfSequence(currentPoint, (int)Direction.RIGHT, (int)Direction.UP, i_shape)) ||

            (checkContinueOfSequence(currentPoint, (int)Direction.RIGHT, (int)Direction.NoChange, i_shape)) ||

            (checkContinueOfSequence(currentPoint, (int)Direction.RIGHT, (int)Direction.DOWN, i_shape)) ||

            (checkContinueOfSequence(currentPoint, (int)Direction.NoChange, (int)Direction.DOWN, i_shape)) ||

            (checkContinueOfSequence(currentPoint, (int)Direction.LEFT, (int)Direction.DOWN, i_shape));

        }

        private bool checkContinueOfSequence(Utilities.Point i_validStepPoint, int i_xDirection, int i_yDirection, String i_playerShape)
        {
            int currentXpoint = i_validStepPoint.X();
            int currentYpoint = i_validStepPoint.Y();

            currentXpoint += i_xDirection;
            currentYpoint += i_yDirection;

            if ((isPointInBorder(currentXpoint, currentYpoint)))
            {
                if ((m_board[currentXpoint, currentYpoint] != null) && (m_board[currentXpoint, currentYpoint] != i_playerShape))
                {
                    while ((isPointInBorder(currentXpoint, currentYpoint)))
                    {
                        if (m_board[currentXpoint, currentYpoint] == i_playerShape)
                        {
                            return true;
                        }
                        currentXpoint += i_xDirection;
                        currentYpoint += i_yDirection;
                    }
                }

            }
            return false;
        }

        private bool isPointInBorder(int i_xOfPoint, int i_yOfPoint)
        {
            return ((i_xOfPoint >= 0) && (i_xOfPoint < m_HeightAndWidth) && ((i_yOfPoint >= 0) && (i_yOfPoint < m_HeightAndWidth)));
        }

        public void updateBoard(Utilities.ValidStep i_pointToChange, string i_shape)
        {
            updateBoardByDirections(i_pointToChange.GetPoint().X(), i_pointToChange.GetPoint().Y(), i_pointToChange.GetList(), i_shape);
        }

        public void updateBoardByDirections(int i_pointX, int i_pointY, List<Utilities.Point> i_listOfChanges, string i_shape)
        {
            foreach (Utilities.Point CurrentPoint in i_listOfChanges)
            {
                updateBoardByDirection(i_pointX, i_pointY, CurrentPoint.X(), CurrentPoint.Y(), i_shape);
            }
        }

        public void updateBoardByDirection(int i_pointX, int i_pointY, int i_DirectionX, int i_DirectionY, string i_shape)
        {
            do
            {
                m_board[i_pointX, i_pointY] = i_shape;
                i_pointX = i_pointX + i_DirectionX;
                i_pointY = i_pointY + i_DirectionY;
            } while (m_board[i_pointX, i_pointY] != i_shape);


        }

        public bool IsBoardFull()
        {
            for (int y = 0; y < m_HeightAndWidth; ++y)
            {
                for (int x = 0; x < m_HeightAndWidth; ++x)
                {
                    if (m_board[x, y] == null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void GetScoreToPlayer(out int i_PlayerOneScore, char i_PlayerOnrSahpe, out int i_PlayerTwoScore, char i_playerTwoShape)
        {
            i_PlayerOneScore = amountOfShapeInBoard(i_PlayerOnrSahpe.ToString());
            i_PlayerTwoScore = amountOfShapeInBoard(i_playerTwoShape.ToString());
        }

        private int amountOfShapeInBoard(string i_Sahpe)
        {
            int countTheShape = 0;

            foreach (string cellShape in m_board)
            {
                if (i_Sahpe == cellShape)
                {
                    countTheShape++;
                }
            }

            return countTheShape;

        }

        public void ReduceBlankCells()
        {
            m_amountOfBlankCells = m_amountOfBlankCells - 1;
        }

        public bool IsHaveBlankCells()
        {
            return m_amountOfBlankCells != 0;
        }


        private enum Direction
        {
            UP = -1,
            DOWN = 1,
            LEFT = -1,
            RIGHT = 1,
            NoChange = 0

        };
    }
}
