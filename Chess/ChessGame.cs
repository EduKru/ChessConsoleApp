﻿using System;
using System.Collections.Generic;
using Chess.Pieces;

namespace Chess
{
    public enum GameState
    {
        Running,
        Check,
        Checkmate,
        Draw
    }

    public class ChessGame
    {
        //carries basic information and information for the flow control
        public ChessBoard CurrentChessBoard;
        public List<Move> MovesHistory;
        public Player Player1;
        public Player Player2;
        public Player CurrentPlayer;
        public GameState GameState = GameState.Running;
        public int TurnCounter;

        //new Fields
        public bool isCheck = false;

        
        public ChessGame(string fenNotationString, string player1Name, string player2Name)
        {
            Player1 = new Player(player1Name, Color.White);
            Player2 = new Player(player2Name, Color.Black);


            //Create the Board
            CurrentChessBoard = new ChessBoard();

            //set white as starting Player
            CurrentPlayer = Player1;

            //set Move Counter to Zero
            TurnCounter = 0;

            //gameState
            GameState = GameState.Running;

            //
            MovesHistory = new List<Move>();

            var dict = new Dictionary<string, Piece>();


            int row = 7;
            int col = 0;
            Color color;
            Piece p; //= new Pawn(new Field(0,0), Color.White);
            foreach (var c in fenNotationString)
            {
                if (Char.IsLower(c))
                {
                     color = Color.Black;
                }

                else
                {
                     color = Color.White;
                }

                var lowerOfChar = Char.ToLower(c);
                Field f = new Field(row, col);

                
                if (Char.IsDigit(lowerOfChar))
                {

                    col = col + int.Parse(c.ToString());
                    continue;
                }
                if (Char.IsLetter(lowerOfChar))
                {

                    //switch
                    switch (lowerOfChar)
                    {
                        case 'r':
                            p = new Rook(f, color);
                            break;
                        case 'n':
                            p = new Knight(f, color);
                            break;
                        case 'b':
                            p = new Bishop(f, color);
                            break;
                        case 'q':
                            p = new Queen(f, color);
                            break;
                        case 'k':
                            p = new King(f, color);
                            break;
                        case 'p':
                            p = new Pawn(f, color);
                            break;
                        default:
                            p = new Pawn(f, color);
                            break;
                    }
                    dict.Add(f.ToString(), p);
                    col++;
                }
                else if (lowerOfChar == char.Parse("/"))
                {
                    row--;
                    col = 0;
                }


                

             }
            CurrentChessBoard = new ChessBoard(dict);
        }



        public ChessGame(string player1Name, string player2Name)
        {

            //Create Players
            Player1 = new Player(player1Name, Color.White);
            Player2 = new Player(player2Name, Color.Black);

            //Create the Board
            CurrentChessBoard = new ChessBoard();

            //set white as starting Player
            CurrentPlayer = Player1;

            //set Move Counter to Zero
            TurnCounter = 0;

            //gameState
            GameState = GameState.Running;

            //
            MovesHistory = new List<Move>();
        }

        


        //Implement here because attribute here
        //Check if Color Player == Color Piece
        public bool IsPlayerAndPieceColorSame(Player player, Piece piece)
        {
            if (piece == null)
            {
                return false;
            }
            bool colorSame = player.Color == piece.PieceColor;
            return colorSame;
        }

        public List<Move> FilterMoveWhichExposeCheck(List<Move> moves, Color currentPlayerColor)
        {
            List<Move> legalMoves = new List<Move>();
            //filtered moves list, return list, reverse logic add ok moves
            foreach (var m in moves)
            {
                ChessBoard copyBoard = new ChessBoard(this.CurrentChessBoard);
                copyBoard.MovePiece(m.FromField, m.ToField, MovementType.Moving, 0);
                //TODO is checked doesnt work properly it doenst care for the kings color
                Color enemyColor = Helper.ColorSwapper(currentPlayerColor);
                if (!copyBoard.IsChecked(enemyColor))
                {
                    legalMoves.Add(m);
                }
            }
            return legalMoves;
        }


        //Check Whether a move results in a check
        public List<Move> FilterMove(MovementType type, List<Move> moveList)
        {
            List<Move> filteredList = new List<Move>();
 
            foreach (var m in moveList)
            {
                if (m.MovementType != type)
                {
                    filteredList.Add(m);
                }
            }
            return filteredList;
        }


        public List<Move> FilterKingMoves(List<Move> moveList, King king)
        {
            Color c = king.PieceColor;
            Color oppColor = Helper.ColorSwapper(c);
            var pieces = this.CurrentChessBoard.getAllPiecesOfColor(oppColor);

            foreach (var p in pieces)
            {
                var enemyPmoves = p.getPossibleMoves(this.CurrentChessBoard);
                enemyPmoves = FilterMove(MovementType.DoubleStep, enemyPmoves);
                enemyPmoves = FilterMove(MovementType.MovingPeaceful, enemyPmoves);


                foreach (var mEnemy in enemyPmoves)
                {
                    for (int i = moveList.Count-1; i >= 0; i--)
                    {
                        if (moveList[i].ToField.ToString() == mEnemy.ToField.ToString())
                        {
                            moveList.RemoveAt(i);
                        }
                    }
                }
            }

            return moveList;
        }


        public bool CheckMitigationPossible()
        {
            bool mitigationFound = false;

            var pieces = this.CurrentChessBoard.getAllPiecesOfColor(this.CurrentPlayer.Color);
            foreach (var pp in pieces)
            {
                var moves = pp.getPossibleMoves(this.CurrentChessBoard);
                moves = FilterMove(MovementType.Defending, moves);

                foreach (var m in moves)
                {
                    ChessBoard copyBoard = new ChessBoard(this.CurrentChessBoard);
                    copyBoard.MovePiece(m.FromField, m.ToField, MovementType.Moving, 0);
                    if (!copyBoard.IsChecked(Helper.ColorSwapper(this.CurrentPlayer.Color)))
                    {
                        mitigationFound = true;
                        break;
                    }

                }

            }

            return mitigationFound;
        }

        //TODO make sure available moves are actually available filtering of moves needed...if check for example
        public bool MovesAvailable()
        {
            bool areMovesAvail = false;
            var pieces = this.CurrentChessBoard.getAllPiecesOfColor(this.CurrentPlayer.Color);

            foreach (var pp in pieces)
            {
                var moves = pp.getPossibleMoves(this.CurrentChessBoard);
                moves = FilterMove(MovementType.Defending, moves);
                if (moves.Count > 0)
                {
                    areMovesAvail = true;
                }
            }

            return areMovesAvail;
        }



    }
}