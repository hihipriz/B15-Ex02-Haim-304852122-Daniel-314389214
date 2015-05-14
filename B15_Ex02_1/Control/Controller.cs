﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Controller.cs" company="">
//   
// </copyright>
// <summary>
//   The e board size.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace B15_Ex02_1.Control
{
    using System;

    using B15_Ex02_1.Logic;
    using B15_Ex02_1.UI;

    /*
         * Board size
         */

    /// <summary>
    /// The e board size.
    /// </summary>
    public enum eBoardSize
    {
        /// <summary>
        /// The six.
        /// </summary>
        Six = 1, 

        /// <summary>
        /// The eight.
        /// </summary>
        Eight = 2
    }

    /// <summary>
    /// The controller.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// The m_ board.
        /// </summary>
        private static Board m_Board;

        /// <summary>
        /// The m_ player 1.
        /// </summary>
        private static Player m_Player1;

        /// <summary>
        /// The m_ player 2.
        /// </summary>
        private static Player m_Player2;

        /// <summary>
        /// The m_ player move.
        /// </summary>
        private static string m_PlayerMove;

        /// <summary>
        /// The m_ player turn.
        /// </summary>
        private static eTurn m_PlayerTurn;

        /// <summary>
        /// The m_ game.
        /// </summary>
        private Game m_Game;

        /*
         * 2nd Player type PC/Player 2
         */

        /// <summary>
        /// The e player.
        /// </summary>
        public enum ePlayer
        {
            /// <summary>
            /// The player.
            /// </summary>
            Player = 1, 

            /// <summary>
            /// The pc.
            /// </summary>
            PC = 2, 

            /// <summary>
            /// The player 2.
            /// </summary>
            Player2 = 3
        }

        /*
         * Initial start up of values at game beginning.
         */

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            initPlayers();
            initBoard();
            m_Game = new Game(m_Player1, m_Player2, m_Board);
            m_Board.drawBoard(m_Board);
            System.Console.WriteLine();
            play();
        }

        /*
         * scans and validates correct player name using UI
         */

        /// <summary>
        /// The get player name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string getPlayerName()
        {
            // Read player name and check input
            string playerName = View.ScanPlayerName();

            while (!InputValidation.ValidatePlayerName(playerName))
            {
                Console.WriteLine();
                View.PrintInvalidInput("Sorry, that's an invalid player name. Please re-enter:");
                playerName = View.ScanPlayerName();
            }

            return playerName;
        }

        /*
         * Ask for Player 2 or PC from user and validate
         */

        /// <summary>
        /// The get player 2 type.
        /// </summary>
        /// <returns>
        /// The <see cref="ePlayer"/>.
        /// </returns>
        private static ePlayer getPlayer2Type()
        {
            int playerTypeNum;
            string playerType = View.AskPlayerType();
            int.TryParse(playerType, out playerTypeNum);

            while (!Enum.IsDefined(typeof(ePlayer), playerTypeNum) && (playerTypeNum != (int)ePlayer.Player2))
            {
                Console.WriteLine();
                View.PrintInvalidInput("Sorry, that's an invalid player type. Please re-enter:");
                int.TryParse(View.AskPlayerType(), out playerTypeNum);
            }

            return (ePlayer)playerTypeNum;
        }

        /*
         * Starts and handles the game.
         */

        /// <summary>
        /// The play.
        /// </summary>
        private void play()
        {
            m_PlayerTurn = m_Game.GetTurn();
            
            while ((m_Game.GetTurn() != eTurn.GameOver) && (m_PlayerMove != "q"))
            {
                // Get player turn
                m_PlayerTurn = m_Game.GetTurn();

                // Get player move, validate and pass it on to game move, which updates the board.
                switch (m_PlayerTurn)
                {
                    case eTurn.Player1:
                        
                        m_PlayerMove = getPlayerMove(m_Player1.PlayerName, eTurn.Player1);

                        // SetBoard with playermove
                        m_Game.Move(m_PlayerTurn, m_PlayerMove);
                        break;
                    case eTurn.Player2:

                        // Player2
                        if (m_Player2.Type == ePlayer.Player2)
                        {
                            m_PlayerMove = getPlayerMove(m_Player2.PlayerName, eTurn.Player2);

                            // SetBoard with playermove
                            m_Game.Move(m_PlayerTurn, m_PlayerMove);
                        }

                        // PC
                        else if (m_Player2.Type == ePlayer.PC)
                        {
                        }

                        break;
                    case eTurn.GameOver:
                        Console.WriteLine("lala");

                        // TODO
                        // View.PrintGameOver(); 
                        break;
                }
            }

            Environment.Exit(0);
        }

        /*
         * Gets player move from user
         */

        /// <summary>
        /// The get player move.
        /// </summary>
        /// <param name="playerName">
        /// The player name.
        /// </param>
        /// <param name="playerTurn">
        /// The player turn.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string getPlayerMove(string playerName, eTurn playerTurn)
        {
            // Read player name and check input
            string playerMove = View.ScanPlayerMove(playerName);

            if (playerMove != "q")
            {
                while (!Game.ValidMove(playerMove, playerTurn))
                {
                    Console.WriteLine();
                    View.PrintInvalidInput("Sorry, that's an invalid move. Please re-enter:");
                    playerMove = View.ScanPlayerMove(playerName);
                }
            }

            return playerMove;
        }

        /*
         * Initializes the board by scanning a size and creating.
         */

        /// <summary>
        /// The init board.
        /// </summary>
        private static void initBoard()
        {
            eBoardSize boardSize = getBoardSize();

            switch (boardSize)
            {
                case eBoardSize.Six:
                    m_Board = new Board(6);
                        
                            m_Board.setCell('O', '3', 'C');
                            m_Board.setCell('X', '3', 'D');
                            m_Board.setCell('X', '4', 'C');
                            m_Board.setCell('O', '4', 'D');

                    break;

                case eBoardSize.Eight:
                    m_Board = new Board(8);
                    
                        m_Board.setCell('O', '4', 'D');
                        m_Board.setCell('X', '4', 'E');
                        m_Board.setCell('X', '5', 'D');
                        m_Board.setCell('O', '5', 'E');
   
                    break;
            }
        }

        /// <summary>
        /// The get board size.
        /// </summary>
        /// *
        /// <returns>
        /// The <see cref="eBoardSize"/>.
        /// </returns>
        private static eBoardSize getBoardSize()
        {
            int boardSizeNum;
            string boardSize = View.AskBoardSize();
            int.TryParse(boardSize, out boardSizeNum);

            while (!Enum.IsDefined(typeof(eBoardSize), boardSizeNum))
            {
                Console.WriteLine();
                View.PrintInvalidInput("Sorry, that's an invalid board size. Please re-enter:");
                int.TryParse(View.AskPlayerType(), out boardSizeNum);
            }

            return (eBoardSize)boardSizeNum;
        }

        /*
         * Scan player names and create player instances
         */

        /// <summary>
        /// The init players.
        /// </summary>
        private static void initPlayers()
        {
            string player1Name = getPlayerName();
            m_Player1 = new Player(player1Name, ePlayer.Player);

            // Determine player2 type and act accordingly
            ePlayer ePlayerOrPc = getPlayer2Type();

            switch (ePlayerOrPc)
            {
                case ePlayer.Player:
                    string player2Name = getPlayerName();
                    m_Player2 = new Player(player2Name, ePlayer.Player2);
                    break;

                case ePlayer.PC:
                    m_Player2 = new Player("*PC*", ePlayer.PC);
                    break;
            }
        }
    }
}