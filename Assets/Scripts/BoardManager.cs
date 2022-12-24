using ChessGame;
using ChessGame.Figures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoardManager : MonoBehaviour, IChessObserver, IDisposable
{
    private const int rows = 8, columns = 8;
    private readonly ChessBoard modelBoard = new ChessBoard(true, true);
    internal readonly BoardCell[,] boardCells = new BoardCell[rows, columns];
    //private readonly GameObject[,] figures = new GameObject[rows, columns];
    //private readonly List<FigureManager> figuresManager = new List<FigureManager>();

    public GameObject pawn;
    public GameObject knight;
    public GameObject bishop;
    public GameObject rook;
    public GameObject queen;
    public GameObject king;
    public Material white;
    public Material black;

    private float scale = 2;

    public GameObject figurePrefab;
    public GameObject cellPrefab;
    public Material blackCell;
    public Material whiteCell;

    public List<IChessObserver> Observers { get; set ; }


    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var boardCell in boardCells)
        {

            if (boardCell.figure != null && boardCell.cell.Figure != null && boardCell.cell.Figure != boardCell.figure)
            {
                boardCell.figure = null;
                boardCell.figureView.transform.position = new Vector3(0, -0.7f, 0);
            }

            if (boardCell.figure == null)
                continue;
                

            if (boardCell.Row == boardCell.figure.Position.Row && boardCell.Column == boardCell.figure.Position.Column)
                continue;



            boardCell.figureView.transform.position = boardCells[boardCell.figure.Position.Row, boardCell.figure.Position.Column].cellView.transform.position + new Vector3(0, 0.7f, 0);
            boardCells[boardCell.figure.Position.Row, boardCell.figure.Position.Column].figureView = boardCell.figureView;
            boardCell.figureView = null;
            boardCells[boardCell.figure.Position.Row, boardCell.figure.Position.Column].figure = boardCell.figure;
            boardCell.figure = null;
        }
    }

    private void GenerateBoard()
    {
        for (int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                var viewCell = Instantiate(cellPrefab, new Vector3(column * scale, 0, (rows - 1 - row) * scale), Quaternion.identity);

                if (row % 2 == column % 2)
                    viewCell.GetComponent<Renderer>().material = whiteCell;
                else
                    viewCell.GetComponent<Renderer>().material = blackCell;

                var cellModel = modelBoard.GetCell(row, column);

                viewCell.GetComponent<Selectable>().cell = cellModel;

                var figureView = CreateFigure(cellModel.Figure, viewCell);

                boardCells[row, column] = new BoardCell(viewCell, figureView, cellModel, cellModel.Figure);
            }

        }
    }

    public GameObject CreateFigure(Figure figure, GameObject cell)
    {
        if (figure == null)
            return null;

        GameObject figureView = null;
        switch (figure)
        {
            case Pawn:
                figureView = pawn;
                break;
            case Knight:
                figureView = knight;
                break;
            case Rook:
                figureView = rook;
                break;
            case Bishop:
                figureView = bishop;
                break;
            case Queen:
                figureView = queen;
                break;
            case King:
                figureView = king;
                break;
        }

        figureView = Instantiate(figureView, cell.transform.position + new Vector3(0, 0.7f, 0), figure.Color == FigureColors.White
                                                                                ? Quaternion.identity
                                                                                : Quaternion.Euler(0, 180, 0));

        figureView.GetComponent<Renderer>().material = figure.Color == FigureColors.White ? white : black;

        return figureView;
    }

    public Task UpdateAsync()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public class BoardCell
    {
        public readonly GameObject cellView;
        public readonly ChessCell cell;

        public GameObject figureView;
        public Figure? figure;
        public int Row => cell.Row;
        public int Column => cell.Column;

        public BoardCell(GameObject cellView, GameObject figureView, ChessCell cell, Figure? figure)
        {
            this.cellView = cellView;
            this.figureView = figureView;
            this.figure = figure;
            this.cell = cell;
        }
    }
}
