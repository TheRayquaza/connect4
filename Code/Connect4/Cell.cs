using System.Text;

namespace Puissance4;

public class Cell
{
    public int Line;
    public int Column;

    public Type CellType;

    public enum Type
    {
        Red,
        Yellow,
        Empty
    }

    public Cell(int x, int y, Type type)
    {
        Line = x;
        Column = y;
        CellType = type;
    }

    public void Print()
    {
        switch (CellType)
        {
            case Type.Red :
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Type.Yellow :
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
        }
        Console.OutputEncoding = Encoding.Unicode;
        Console.Write(CellType == Type.Empty ? " " : "●", System.Text.Encoding.Unicode);

        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public Cell CellCopy() => new Cell(Line, Column, CellType);
}