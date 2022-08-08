using System;

namespace Puissance4
{

public class Board
{
    public const int NbColumns = 7;
    public const int NbLines = 6;

    public Cell[,] BoardGame;

    // CTOR
    public Board()
    {
        BoardGame = new Cell[NbLines, NbColumns];
        for (int i = 0; i < NbLines; i++)
            for (int j = 0; j < NbColumns; j++)
                BoardGame[i, j] = new Cell(i, j, Cell.Type.Empty);
    }

    // Copy
    public Board BoardCopy()
    {
        Board boardCopy = new Board();
        for (int i = 0; i < NbLines; i++)
            for (int j = 0; j < NbColumns; j++)
                boardCopy.BoardGame[i, j] = BoardGame[i, j].CellCopy();

        return boardCopy;
    }

    // Verify if the board is full
    public bool IsFull()
    {
        for (int i = 0; i < NbLines; i++)
            for (int j = 0; j < NbColumns; j++)
                if (BoardGame[i, j].CellType == Cell.Type.Empty) return false;

        return true;
    }

    // Place a piece in the board
    public bool PlacePiece(int column, Cell.Type type)
    {
        if (column < 0 || column >= NbColumns) return false;
        
        int line = 0;
        while (line < NbLines && BoardGame[line, column].CellType == Cell.Type.Empty) line++;

        if (line == 0) return false;
        BoardGame[line-1, column].CellType = type;
        return true;
    }

    public bool CheckMove(int column, Cell.Type type)
    {
        // Line detection
        int line = 0;
        while (BoardGame[line, column].CellType == Cell.Type.Empty) line++;

        int count = 0;
        // Horizontal
        int lineHorizontal = line;
        while (lineHorizontal >= 0 && BoardGame[lineHorizontal, column].CellType == type) lineHorizontal--;
        lineHorizontal++;
        while (lineHorizontal < Board.NbLines && BoardGame[lineHorizontal, column].CellType == type)
        {
            lineHorizontal++;
            count++;
        }

        if (count == 4) return true;
        count = 0;
        
        // Vertical
        int columnVertical = column;
        while (columnVertical >= 0 && BoardGame[line, columnVertical].CellType == type) columnVertical--;
        columnVertical++;
        while (columnVertical < Board.NbColumns && BoardGame[line, columnVertical].CellType == type)
        {
            columnVertical++;
            count++;
        }

        if (count == 4) return true;
        count = 0;
        
        // Diagonal TopRight
        int columnTopRight = column;
        int lineTopRight = line;
        while (columnTopRight >= 0 && lineTopRight >= 0 && BoardGame[lineTopRight, columnTopRight].CellType == type) {columnTopRight--; lineTopRight--;}
        columnTopRight++;
        lineTopRight++;
        while (columnTopRight < Board.NbColumns && lineTopRight < Board.NbLines && BoardGame[lineTopRight, columnTopRight].CellType == type)
        {
            columnTopRight++;
            lineTopRight++;
            count++;
        }

        if (count == 4) return true;
        count = 0;
        
        
        // Diagonal BottomLeft
        int columnBottomLeft = column;
        int lineBottomLeft = line;
        while (columnBottomLeft >= 0 && lineBottomLeft < Board.NbLines && BoardGame[lineBottomLeft, columnBottomLeft].CellType == type) {lineBottomLeft++; columnBottomLeft--;}
        columnBottomLeft++;
        lineBottomLeft--;
        while (columnBottomLeft < Board.NbColumns && lineBottomLeft >= 0 && BoardGame[lineBottomLeft, columnBottomLeft].CellType == type)
        {
            columnBottomLeft++;
            lineBottomLeft--;
            count++;
        }

        if (count == 4) return true;
        count = 0;

        return false;

    }
    
    #region Print 
    
    const int HorizontalMargin = 2;

    // Pretty Print
    public void Print()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        for (int i = 0; i < NbLines; i++)
        {
            if (i == 0) PrintLine('┌', '┬', '┐');
            else PrintLine('├', '┼', '┤');
                
            for (int j = 0; j < NbColumns; j++)
            {
                Console.Write('│');
                PrintWithMargins(i, j);
            }

            Console.WriteLine('│'); 
        }

        PrintLine('└', '┴', '┘');
    }

    // Print Line
    public void PrintLine(char start, char middle, char end)
    {
        int caseSize = (2 + HorizontalMargin * 2);
            
        Console.Write(start);
        for (int i = 0; i < caseSize * NbColumns - 1; i++)
        {
            Console.Write((i + 1) % caseSize == 0 ? middle : '─');
        }
        Console.WriteLine(end);
    }

    // Print With Margins
    public void PrintWithMargins(int x, int y)
    {
        for (int i = 0; i < HorizontalMargin * 2 + 1; i++)
        {
            if (i != HorizontalMargin) Console.Write(' ');
            else BoardGame[x,y].Print();
        }
    }

    #endregion
}
}
