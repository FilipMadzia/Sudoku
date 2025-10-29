using Sudoku.Core;
using Sudoku.Core.Enums;

var sudokuBoard = SudokuBoardGenerator.GenerateEasy();

Console.WriteLine(sudokuBoard);

var row = int.Parse(Console.ReadLine());
var col = int.Parse(Console.ReadLine());
var value = int.Parse(Console.ReadLine());

sudokuBoard.MakeMove(new SudokuMove(row, col, value));

Console.WriteLine(sudokuBoard);