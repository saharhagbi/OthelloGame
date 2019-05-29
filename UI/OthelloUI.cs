using System;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    public class OthelloUI
    {
        private string[,] m_board;
        private int m_HeightAndWidth;
        Controller m_GameLogic = new Controller();
        private OthelloLogic.Utilities.checkNextStepForTheProgram m_isNeedToStopGame = OthelloLogic.Utilities.checkNextStepForTheProgram.Run;

        public void StartGame()
        {

            m_HeightAndWidth = getHeightAndWidth();
            if (m_isNeedToStopGame != OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
            {
                GetPlayer(true, 'X', 1);
                GetSecondPlayer();
            }

            if (m_isNeedToStopGame != OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
            {
                m_board = m_GameLogic.StartGame(m_HeightAndWidth);
                Ex02.ConsoleUtils.Screen.Clear();
                printNameOfThePlayerThatPlay();
                printBoard();
                ControllerMainGame();
            }
        }

        private void ControllerMainGame()
        {
            OthelloLogic.Utilities.checkNextStepForTheProgram gameState;
            string PlayerStep;

            do
            {
                Console.WriteLine("Enter your Step");
                PlayerStep = Console.ReadLine();
                checkIfNeedStopGame(PlayerStep);
                if (m_isNeedToStopGame == OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
                {
                    break;
                }
                gameState = m_GameLogic.CheckVaildStepAndUpdateBoard(PlayerStep);

                handleSituationAfterTurn(gameState);

            } while ((gameState != OthelloLogic.Utilities.checkNextStepForTheProgram.EndGame) &&

                    (gameState != OthelloLogic.Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay));

            if (m_isNeedToStopGame != OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
            {
                PrintResult();
            }
        }

        private void PrintResult()
        {
            int PlayerOneScore, PlayerTwoScore;
            string PlayerOneName, PlayerTwoName;

            m_GameLogic.GetScore(out PlayerOneName, out PlayerTwoName, out PlayerOneScore, out PlayerTwoScore);

            Console.WriteLine(PlayerOneName + "   " + PlayerOneScore);
            Console.WriteLine(PlayerTwoName + "   " + PlayerTwoScore);
        }

        private void checkIfNeedStopGame(string i_CheckTheTerm)
        {
            if (((i_CheckTheTerm == "Q") || (i_CheckTheTerm == "q")))
            {
                m_isNeedToStopGame = OthelloLogic.Utilities.checkNextStepForTheProgram.Quite;
            }
        }

        private void handleSituationAfterTurn(OthelloLogic.Utilities.checkNextStepForTheProgram i_gameSituationAfterTurn)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            printNameOfThePlayerThatPlay();
            printBoard();

            switch (i_gameSituationAfterTurn)
            {
                case OthelloLogic.Utilities.checkNextStepForTheProgram.EndGame:
                    Console.WriteLine("End Game");
                    break;

                case OthelloLogic.Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay:
                    Console.WriteLine("The Two Players Can't Play");
                    break;
                case OthelloLogic.Utilities.checkNextStepForTheProgram.InvalidInput:
                    Console.WriteLine("Input is invalid. Input need to contain upper letter and a digit. Example: E2");
                    break;
                case OthelloLogic.Utilities.checkNextStepForTheProgram.IllegalStep:
                    Console.WriteLine("IllegalStep. Try other Step");
                    break;

            }
        }

        private void printNameOfThePlayerThatPlay()
        {
            string name = m_GameLogic.GetNameOfThePlayerThatPlay();

            Console.WriteLine("It's " + name + " turn!");
        }

        private void GetPlayer(bool i_isHuman, char i_shape, int i_playerNumber)
        {
            string playerName;
            if (m_isNeedToStopGame != OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
            {
                if (i_isHuman)
                {
                    Console.WriteLine("Write your name");
                    playerName = Console.ReadLine();
                    checkIfNeedStopGame(playerName);
                }
                else
                {
                    playerName = "Computer";
                }

                m_GameLogic.SetPlayerOnController(playerName, i_isHuman, i_shape, i_playerNumber);
            }

        }

        private void GetSecondPlayer()
        {
            string getTypeOfPlayer;

            if (m_isNeedToStopGame != OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
            {
                do
                {
                    Console.WriteLine(@"choose which game you want play
1. Player against player
2. Player against computer");
                    getTypeOfPlayer = Console.ReadLine();
                    checkIfNeedStopGame(getTypeOfPlayer);

                    if (m_isNeedToStopGame == OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
                    {
                        break;
                    }
                } while ((getTypeOfPlayer != "1") && (getTypeOfPlayer != "2"));

                if (m_isNeedToStopGame == OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
                {
                    return;
                }

                bool isHuman = getTypeOfPlayer == "1";
                GetPlayer(isHuman, 'O', 2);
            }

        }

        private int getHeightAndWidth()
        {
            string HeightAndWidth;

            Console.WriteLine("Chose your board size. Type 6 for 6X6 board Or 8 for 8X8 board");
            HeightAndWidth = Console.ReadLine();
            checkIfNeedStopGame(HeightAndWidth);

            while ((HeightAndWidth != "6") && (HeightAndWidth != "8"))
            {
                if (m_isNeedToStopGame != OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
                {
                    Console.WriteLine("Invalid choice! Chose 6 for 6X6 board Or 8 for 8X8 board");
                    HeightAndWidth = Console.ReadLine();
                    checkIfNeedStopGame(HeightAndWidth);
                }


            }
            if (m_isNeedToStopGame != OthelloLogic.Utilities.checkNextStepForTheProgram.Quite)
            {
                return int.Parse(HeightAndWidth);
            }
            else
            {
                return (int)(OthelloLogic.Utilities.checkNextStepForTheProgram.Quite);
            }
        }

        private void printBoard()
        {

            char letter;
            StringBuilder BoardToPrint = new StringBuilder();
            StringBuilder EqualMark = new StringBuilder(m_HeightAndWidth * 4 + 2);
            EqualMark.Append("\n");
            EqualMark.Insert(1, "=", m_HeightAndWidth * 4 + 1);

            for (int i = 0; i < m_HeightAndWidth; ++i)
            {
                letter = (char)(i + 'A');
                BoardToPrint.Append("   " + letter);
            }

            for (int i = 0; i < m_HeightAndWidth; ++i)
            {
                BoardToPrint.Append(@"  " + EqualMark);
                BoardToPrint.Append("\n");
                BoardToPrint.Append((i + 1).ToString());
                for (int j = 0; j < m_HeightAndWidth; ++j)
                {
                    if (m_board[i, j] == null)
                        BoardToPrint.Append(@"|   ");
                    else
                        BoardToPrint.Append(@"| " + m_board[i, j] + @" ");
                }
                BoardToPrint.Append("|");
            }

            BoardToPrint.Append(EqualMark);
            Console.WriteLine(BoardToPrint);
        }
    }
}
