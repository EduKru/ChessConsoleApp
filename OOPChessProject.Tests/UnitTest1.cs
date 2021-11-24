﻿
using NUnit.Framework;
using System;

namespace OOPChessProject.Tests
{
    //decorator = special attribute
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            ChessGame ChessGame = new ChessGame("Eduard", "Lukas");
            string board = ChessGame.currentChessBoard.ToString();
            Console.Write(board.Length);
            Assert.AreEqual(board.Length, 494);
        }

        [Test]
        public void TestMethod2()
        {
            Field f = new Field(2, 3);
            Assert.AreEqual(4, 5);
        }

        [Test]
        public void TestBoardCopy()
        {
            ChessBoard chessBoard = new ChessBoard();
            ChessBoard copyBoard = new ChessBoard(chessBoard);
            copyBoard.MovePiece(new Field(1,4), new Field(3, 4), MovementType.moving, 0);

            Console.WriteLine(chessBoard);
            Console.WriteLine(copyBoard);

            Assert.AreNotEqual(copyBoard.ToString(),chessBoard.ToString());
        }

        [Test]
        public void TestLongCastleChecker()
        {

            ChessBoard cb = new ChessBoard();
            cb.MovePiece(new Field(0,1), new Field(5,1), MovementType.moving, 0);
            cb.MovePiece(new Field(0, 2), new Field(5, 2), MovementType.moving, 0);
            cb.MovePiece(new Field(0, 3), new Field(5, 3), MovementType.moving, 0);
            //First test should be true
            Console.WriteLine(cb);
            Console.WriteLine(cb.CastleLong(Color.White));

            cb.MovePiece(new Field(0, 4), new Field(3, 4), MovementType.moving, 0);
            cb.MovePiece(new Field(3, 4), new Field(0, 4), MovementType.moving, 0);
            //second test shouldnt because piece was moved back and forth.
            Console.WriteLine(cb);
            Console.WriteLine(cb.CastleLong(Color.White));


        }

        [Test]
        public void cGame()
        {
            ChessGame cG = new ChessGame("A","B");
            cG.currentChessBoard.MovePiece(new Field(1,3), new Field(3,3), MovementType.moving, 0);
            cG.currentChessBoard.MovePiece(new Field(7, 2), new Field(3, 1), MovementType.moving, 0);
            //Console.WriteLine(cG.currentChessBoard);
            //Console.WriteLine(cG.currentChessBoard.isChecked(Color.Black));
            bool shouldBeCheck = cG.currentChessBoard.isChecked(Color.Black);

            cG.currentChessBoard.MovePiece(new Field(1, 2), new Field(2, 2), MovementType.moving, 0);
            //Console.WriteLine(cG.currentChessBoard);
            //Console.WriteLine(cG.currentChessBoard.isChecked(Color.Black));
            bool shouldntBeCheck = cG.currentChessBoard.isChecked(Color.Black);

            bool checkAndnoCheckAfterBlock = (shouldBeCheck == true) && (shouldntBeCheck == false);
            Assert.IsTrue(checkAndnoCheckAfterBlock);
        }
    }
}
