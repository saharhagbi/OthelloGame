using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloLogic
{
    public class Player
    {
        private string m_name;
        private bool m_isHuman;
        private char m_shape;

        public Player(string i_name, bool i_isHuman, char i_shape)
        {
            m_name = i_name;
            m_isHuman = i_isHuman;
            m_shape = i_shape;
        }

        public char getShape()
        {
            return m_shape;
        }

        public string getName()
        {
            return m_name;
        }


        public bool isAHuman()
        {
            return m_isHuman;
        }
    }
}
