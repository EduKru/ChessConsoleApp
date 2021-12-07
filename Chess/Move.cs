﻿using Chess.Pieces;

namespace Chess
{
    public enum MovementType
    {
        Moving, //
        MovingPeaceful,
        Defending, //controlling can mean capturing or potentially move to that field once the ally piece left.
        DoubleStep, //important for en passant implementation
        Capturing,
        CastleShort,
        CastleLong,
        EnPassant
    }

    //TODO remove instance of Piece from board and put it in the move object. Create new instance for board
    
    public class Move
    {
        private string name;
        private Piece P;
        public Field FromField;
        public Field ToField;
        public MovementType MovementType;
        //private Piece m_capturedPiece;


        public Move(string name, Field from, Field to, MovementType mt)
        {
            this.name = name;
            FromField = from;
            ToField = to;
            MovementType = mt;
            //this.m_capturedPiece = mCapturedPiece;
        }


        public override string ToString()
        {
            return (fromField: FromField, toField: ToField, movementType: MovementType).ToString();
        }


    }
}