using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloLogic
{
    public class Utilities
    {
        public enum checkNextStepForTheProgram
        {
            ValidStep = 1,
            IllegalStep = 2,
            EndGame = 3,
            InvalidInput = 4,
            ValidInput = 5,
            TwoPlayerCantPlay = 6,
            Quite = 0,
            Run
        };

        public struct Point
        {
            private int m_X;
            private int m_Y;

            public Point(int i_height, int i_width)
            {
                m_X = i_height;
                m_Y = i_width;
            }

            public int X()
            {
                return m_X;
            }

            public int Y()
            {
                return m_Y;
            }
        }

        public struct ValidStep
        {
            private Point m_pointInMatrix;

            private List<Point> m_directionsValidStep;

            public ValidStep(Point m_point)
            {
                m_pointInMatrix = m_point;
                m_directionsValidStep = new List<Point>();
            }

            public void AddDirection(Point i_direction)
            {
                m_directionsValidStep.Add(i_direction);
            }

            public Point GetPoint()
            {
                return m_pointInMatrix;
            }

            public List<Point> GetList()
            {
                return m_directionsValidStep;
            }
        }
    }
}
