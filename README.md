# ChessGUI (OOP - Project)
notImplemented Logic features:
Promotion

IDE: Visual Studio ---> Rider

General Project-Structure:
Not Yet Implemented-Requirements:
* Display Player Name
* Game Start Menu
* Visualize Check
* GameOver Screen
________________________________________________________________________________
Current state of the project:\
![Unbenannt](https://user-images.githubusercontent.com/29587190/146266333-0e8c109f-313b-426e-970d-ecce9a1bfeea.PNG)

This is how the Gamelogic looks for the console application:
![image](https://user-images.githubusercontent.com/29587190/149512806-6ea3e822-7552-4bc9-ad5d-2726b8ecb80b.png)


________________________________________________________________________________


<img src="https://user-images.githubusercontent.com/29587190/143298051-5ec44c11-8890-450c-b4a9-20657083fdad.PNG" alt="drawing" width="600"/>



OOP-Project: Implementing Chess as a Concole App.

Currently working on the logic of the pieces.
Especially King interaction like pins ect. aren't implemented yet.

In the follwing chapter I go through the concepts of OOP and give examples related to my chess project.

## Inheritance:
A parent class has some functionality which the child class can inherit from.
All types in .Net inherit from the Object class meaning functions like Equals, ToString, GetType already exist.

## Polymorphism
Implementing methods that must be implemented by any derived classes
For example:
The Class Piece (parent class) has a Method called getPossibleFields(). Classes like pawn, knight etc. inherit from the piece class.
Since every kind of piece moves different the implementation of getPossibleFields() is unique inside the child classes.
The Input for the GetPossibleMoves is the currentChessboardObject (to detect blocking and other illegal moves). The returned list is really different
depending on which child class called the getPossibleFields()-Method.

```
    class Knight : Piece
    {
        //constructor
        public Knight(Field aField, Color aPieceColor, bool aisAlive = true) : base(aField, aPieceColor, aisAlive = true)
        {
            //already Implemented
            base.PrintRepresentation = "KN";
        }
        ...  
    
                public override List<Field> getPossibleFields(ChessBoard cb)
        {
            var rowOfsetcolOfset = new List<(int, int)>
            {
                (-2, -1), //2 hoch 1 links
                (-2, 1), // 2 hoch 1 rechts
                (-1, -2),  // 1 hoch  2 links
                (-1, 2), // 1 hoch 2 rechts
                (1,2), // 1 runter 2 rechts
                (1,-2), // 1 runter 2 links
                (2,1),
                (2,-1)
            };

            return getPossibleFieldsTraversingPieces(cb, rowOfsetcolOfset, false);
        }
    }
 ```
The Piece class is also an abstract class, because there shouldn't be a piece instance and the get getPossibleFields-Method also gets the abstract keyword.

 ```
        //Implement in Piece children
        public abstract List<Field> getPossibleFields(ChessBoard cb);
 ```
