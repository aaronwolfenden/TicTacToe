new TicTacToe().RunGame();


public class TicTacToe
{
    public void RunGame()
    {
        Board board = new Board();
        Player playerOne = new Player(Cell.X);
        Player playerTwo = new Player(Cell.O);
        Player currentPlayer = playerOne;
        int roundNumber = 0;
        while (roundNumber < 9)
        {
            Console.WriteLine($"\nIt is currently {currentPlayer.Symbol}'s Turn");
            board.DisplayBoard();
            Square square = currentPlayer.MakeMove(board);
            board.FillCell(square.Row, square.Column, currentPlayer.Symbol);
            if (CheckForWinner(board, currentPlayer.Symbol))
            {
                Console.WriteLine($"\nPlayer {currentPlayer.Symbol} has won!");
                board.DisplayBoard();
                break;
            }

            if (currentPlayer.Symbol == Cell.X)
            {
                currentPlayer = playerTwo;
            }
            else
            {
                currentPlayer = playerOne;
            }

            roundNumber++;
        }
        Console.WriteLine("\nIt's a draw!");
    }

    public bool CheckForWinner(Board board, Cell cell)
    {
        // Row Wins
        if (board.GetContents(0, 0) == cell && board.GetContents(0, 1) == cell && board.GetContents(0, 2) == cell) return true;
        if (board.GetContents(1, 0) == cell && board.GetContents(1, 1) == cell && board.GetContents(1, 2) == cell) return true;
        if (board.GetContents(2, 0) == cell && board.GetContents(2, 1) == cell && board.GetContents(2, 2) == cell) return true;

        // Column Wins
        if (board.GetContents(0, 0) == cell && board.GetContents(1, 0) == cell && board.GetContents(2, 0) == cell) return true;
        if (board.GetContents(0, 1) == cell && board.GetContents(1, 1) == cell && board.GetContents(2, 1) == cell) return true;
        if (board.GetContents(0, 2) == cell && board.GetContents(1, 2) == cell && board.GetContents(2, 2) == cell) return true;

        // Diagonal Wins
        if (board.GetContents(0, 0) == cell && board.GetContents(1, 1) == cell && board.GetContents(2, 2) == cell) return true;
        if (board.GetContents(0, 2) == cell && board.GetContents(1, 1) == cell && board.GetContents(2, 0) == cell) return true;

        return false;
    }
}


public class Player
{
    public Cell Symbol { get; }

    public Player(Cell symbol)
    {
        Symbol = symbol;
    }

    public Square MakeMove(Board board)
    {
        while (true)
        {

            Console.Write("Where would you like to make your move? ");
            ConsoleKey key = Console.ReadKey().Key;

            Square choice = key switch
            {
                ConsoleKey.NumPad7 => new Square(0, 0),
                ConsoleKey.NumPad8 => new Square(0, 1),
                ConsoleKey.NumPad9 => new Square(0, 2),
                ConsoleKey.NumPad4 => new Square(1, 0),
                ConsoleKey.NumPad5 => new Square(1, 1),
                ConsoleKey.NumPad6 => new Square(1, 2),
                ConsoleKey.NumPad1 => new Square(2, 0),
                ConsoleKey.NumPad2 => new Square(2, 1),
                ConsoleKey.NumPad3 => new Square(2, 2)
            };

            if (board.IsEmpty(choice.Row, choice.Column)) return choice;
            else Console.WriteLine("That square is taken, make a new choice.");
        }
    }
}


public class Board
{
    private readonly Cell[,] _cells = new Cell[3, 3];

    public void DisplayBoard()
    {
        // Displays the current state of the board
        Console.WriteLine();
        Console.WriteLine("======= Current Board State ======= \n");
        Console.WriteLine($"          {GetSymbol(_cells[0, 0])}  |  {GetSymbol(_cells[0, 1])}  |  {GetSymbol(_cells[0, 2])}");
        Console.WriteLine("       |-----+-----+-----|");
        Console.WriteLine($"          {GetSymbol(_cells[1, 0])}  |  {GetSymbol(_cells[1, 1])}  |  {GetSymbol(_cells[1, 2])}");
        Console.WriteLine("       |-----+-----+-----|");
        Console.WriteLine($"          {GetSymbol(_cells[2, 0])}  |  {GetSymbol(_cells[2, 1])}  |  {GetSymbol(_cells[2, 2])}");
        Console.WriteLine();
    }

    public char GetSymbol(Cell cell)
    {
        // The value of the cell is changed to a char to represent empty cells better

        char symbol = cell switch
        {
            Cell.Empty => ' ',
            Cell.X => 'X',
            Cell.O => 'O',
            _ => '_'
        };

        return symbol;
    }


    public bool IsEmpty(int row, int column) => _cells[row, column] == Cell.Empty;
    public void FillCell(int row, int column, Cell symbol) => _cells[row, column] = symbol;
    public Cell GetContents(int row, int column) => _cells[row, column];
}

public class Square
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Square(int row, int column)
    {
        Row = row;
        Column = column;
    }
}


public enum Cell { Empty, O, X }