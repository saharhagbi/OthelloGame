using System;
using System.Collections.Generic;
using System.Text;
using OthelloLogic;

namespace UI
{
    public class Controller
    {
        private OthelloLogic.gameManager m_gameLogic = new OthelloLogic.gameManager();

        public string[,] StartGame(int i_HeightAndWidth)
        {
            return m_gameLogic.StartGame(i_HeightAndWidth);
        }

        public void SetPlayerOnController(string i_playerName, bool i_isHuman, char i_shape, int i_playerNumber)
        {
            m_gameLogic.SetPlayerOnGameManager(i_playerName, i_isHuman, i_shape, i_playerNumber);
        }

        public Utilities.checkNextStepForTheProgram CheckVaildStepAndUpdateBoard(string i_PlayerStep)
        {
            Utilities.checkNextStepForTheProgram ProgramState = Utilities.checkNextStepForTheProgram.ValidStep;

            if (checkValidInput(i_PlayerStep) == Utilities.checkNextStepForTheProgram.ValidInput)
            {
                Utilities.Point parseStep;
                ParseStringToNumber(i_PlayerStep, out parseStep);
                ProgramState = m_gameLogic.CheckVaildStepAndUpdateBoard(parseStep);

                if (ProgramState == Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay)
                {
                    return m_gameLogic.CheckfBoardFullOrNoMoreMovesForPlayers();
                }
            }
            else
            {
                return Utilities.checkNextStepForTheProgram.InvalidInput;
            }

            ProgramState = isTheGameGoOn();

            return ProgramState;
        }

        private Utilities.checkNextStepForTheProgram isTheGameGoOn()
        {
            return m_gameLogic.IsTheGameContinue();
        }

        private Utilities.checkNextStepForTheProgram checkValidInput(string i_PlayerStep)
        {


            if (i_PlayerStep.Length != 2)
            {
                return Utilities.checkNextStepForTheProgram.InvalidInput;
            }

            char[] AllLetters = new char[2];
            i_PlayerStep.CopyTo(0, AllLetters, 0, 2);

            if ((char.IsUpper(AllLetters[0])) && (char.IsDigit(AllLetters[1])))
            {
                return Utilities.checkNextStepForTheProgram.ValidInput;
            }

            return Utilities.checkNextStepForTheProgram.InvalidInput;
        }

        private void ParseStringToNumber(string i_PlayerStep, out OthelloLogic.Utilities.Point r_parseStep)
        {
            int x, y;
            y = (int)(char.Parse(i_PlayerStep.Remove(1, 1)) - 'A');
            x = (int)(char.Parse(i_PlayerStep.Remove(0, 1)) - '0') - 1;

            r_parseStep = new OthelloLogic.Utilities.Point(x, y);
        }

        public string GetNameOfThePlayerThatPlay()
        {
            return m_gameLogic.GetNameOfThePlayerTurn();
        }

        public void GetScore(out string i_PlayerOneName, out string i_PlayerTwoName, out int i_PlayerOneScore, out int i_PlayerTwoScore)
        {
            i_PlayerOneName = m_gameLogic.GetNamePlayerOne();
            i_PlayerTwoName = m_gameLogic.GetNamePlayerTwo();
            m_gameLogic.GetScore(out i_PlayerOneScore, out i_PlayerTwoScore);
        }
    }
}
