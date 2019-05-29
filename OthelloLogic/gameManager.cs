using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloLogic
{

    public class gameManager
    {
        private Board m_board;
        private Player m_playerOne;
        private Player m_playerTwo;
        private int m_whichPlayerPlay = 1;
        private amoutOfPlayerWithNoTurn CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.ZeroPlayer;

        public string[,] StartGame(int i_HeightAndWidth)
        {
            m_board = new Board();
            return m_board.GetStartingBoard(i_HeightAndWidth);
        }

        public void SetPlayerOnGameManager(string i_playerName, bool i_isHuman, char i_shape, int i_playerNumber)
        {
            if (i_playerNumber == 1)
            {
                m_playerOne = new Player(i_playerName, i_isHuman, i_shape);
            }
            else
            {
                m_playerTwo = new Player(i_playerName, i_isHuman, i_shape);
            }
        }

        public Utilities.checkNextStepForTheProgram CheckVaildStepAndUpdateBoard(Utilities.Point i_pointToCheck)
        {
            if (m_whichPlayerPlay == 1)
            {
                if (!tryDoHumanTurn(i_pointToCheck, m_playerOne))
                {
                    if (CounterAmountOfPlayerWithNoTurn == amoutOfPlayerWithNoTurn.TwoPlayer)
                    {
                        return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
                    }

                    return Utilities.checkNextStepForTheProgram.IllegalStep;
                }

                if (!m_playerTwo.isAHuman())
                {
                    if (CounterAmountOfPlayerWithNoTurn == amoutOfPlayerWithNoTurn.TwoPlayer)
                    {
                        return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
                    }

                    tryDoComputerTurn();
                    m_whichPlayerPlay = 1;
                }
                else
                {
                    m_whichPlayerPlay = 2;
                }

                return Utilities.checkNextStepForTheProgram.ValidStep;
            }
            else
            {
                if (!tryDoHumanTurn(i_pointToCheck, m_playerTwo))
                {
                    if (CounterAmountOfPlayerWithNoTurn == amoutOfPlayerWithNoTurn.TwoPlayer)
                    {
                        return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
                    }
                    return Utilities.checkNextStepForTheProgram.IllegalStep;
                }
                else
                {
                    m_whichPlayerPlay = 1;
                }
                return Utilities.checkNextStepForTheProgram.ValidStep;
            }
        }

        private bool isStepIsValid(List<Utilities.ValidStep> i_validSteps, Utilities.Point i_pointToCheck)
        {

            int sizeOfList = i_validSteps.Count;
            Utilities.ValidStep[] CloneValidSteps = new Utilities.ValidStep[sizeOfList];
            i_validSteps.CopyTo((CloneValidSteps));

            foreach (Utilities.ValidStep currentStepToCheck in CloneValidSteps)
            {
                if (!(currentStepToCheck.GetPoint().X() == i_pointToCheck.X() && currentStepToCheck.GetPoint().Y() == i_pointToCheck.Y()))
                {
                    i_validSteps.Remove(currentStepToCheck);
                }
            }

            return i_validSteps.Count > 0;
        }

        private bool isListEmpty(List<Utilities.ValidStep> i_validSteps)
        {
            return i_validSteps.Count == 0;
        }

        private void tryDoComputerTurn()
        {
            List<Utilities.ValidStep> validSteps;
            if (isTurnPossible(out validSteps, m_playerTwo))
            {
                m_board.ReduceBlankCells();
                CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.ZeroPlayer;
                int amountOfValidSteps = validSteps.Count;

                Random randomPlay = new Random();
                int randomStepIndex = randomPlay.Next(0, amountOfValidSteps);

                m_board.updateBoard(validSteps[randomStepIndex], m_playerTwo.getShape().ToString());
            }
            else
            {
                updateAmountOfPlayerThatDontPlay();
            }



        }

        private bool tryDoHumanTurn(Utilities.Point i_pointToCheck, Player i_PlayerToPlay)
        {
            List<Utilities.ValidStep> validSteps;

            if ((isTurnPossible(out validSteps, i_PlayerToPlay)))
            {
                m_board.ReduceBlankCells();
                CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.ZeroPlayer;

                if ((isStepIsValid(validSteps, i_pointToCheck)))
                {
                    m_board.updateBoard(validSteps[0], i_PlayerToPlay.getShape().ToString());
                    return true;
                }
            }
            else
            {
                updateAmountOfPlayerThatDontPlay();
            }


            return false;
        }

        private void updateAmountOfPlayerThatDontPlay()
        {
            if (CounterAmountOfPlayerWithNoTurn == amoutOfPlayerWithNoTurn.ZeroPlayer)
            {
                CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.OnePlyer;
            }
            else
            {
                CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.TwoPlayer;
            }
        }

        private bool isTurnPossible(out List<Utilities.ValidStep> i_validSteps, Player i_PlayerToPlay)
        {
            i_validSteps = m_board.getAllValidSteps(i_PlayerToPlay.getShape());
            int amountOfValidSteps = i_validSteps.Count;

            return amountOfValidSteps != 0;
        }

        private bool CheckIfGameIsOver()
        {
            List<Utilities.ValidStep> validStepsPlayerOne;
            List<Utilities.ValidStep> validStepsPlayerTwo;
            if (!isTurnPossible(out validStepsPlayerOne, m_playerOne))
            {
                m_whichPlayerPlay = 2;
                if (!m_playerTwo.isAHuman())
                {
                    CheckVaildStepAndUpdateBoard(new Utilities.Point(0, 0));
                }
            }

            return (!isTurnPossible(out validStepsPlayerOne, m_playerOne) && (!isTurnPossible(out validStepsPlayerTwo, m_playerTwo)));
        }

        public Utilities.checkNextStepForTheProgram CheckfBoardFullOrNoMoreMovesForPlayers()
        {
            if (m_board.IsBoardFull())
            {
                return Utilities.checkNextStepForTheProgram.EndGame;
            }

            return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
        }

        public string GetNameOfThePlayerTurn()
        {
            if (m_whichPlayerPlay == 1)
            {
                return GetNamePlayerOne();
            }
            else
            {
                return GetNamePlayerTwo();
            }
        }

        public string GetNamePlayerOne()
        {
            return m_playerOne.getName();
        }

        public string GetNamePlayerTwo()
        {
            return m_playerTwo.getName();
        }

        public void GetScore(out int i_PlayerOneScore, out int i_PlayerTwoScore)
        {
            m_board.GetScoreToPlayer(out i_PlayerOneScore, m_playerOne.getShape(), out i_PlayerTwoScore, m_playerTwo.getShape());
        }

        public Utilities.checkNextStepForTheProgram IsTheGameContinue()
        {
            if (!m_board.IsHaveBlankCells())
            {
                return Utilities.checkNextStepForTheProgram.EndGame;
            }

            if (CheckIfGameIsOver())
            {
                return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
            }

            return Utilities.checkNextStepForTheProgram.Run;
        }

        private enum amoutOfPlayerWithNoTurn
        {
            TwoPlayer,
            OnePlyer,
            ZeroPlayer
        };
    }


}
