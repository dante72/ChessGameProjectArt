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

    public GameObject figurePrefab;
    public GameObject pawn;
    public GameObject knight;
    public GameObject bishop;
    public GameObject rook;
    public GameObject queen;
    public GameObject king;
    public Material white;
    public Material black;

    private Dictionary<Type, GameObject> dictionary = new Dictionary<Type, GameObject>();

    private float scale = 2;

    public GameObject cellPrefab;
    public Material blackCell;
    public Material whiteCell;

    public List<IChessObserver> Observers { get; set ; }


    // Start is called before the first frame update
    void Start()
    {
        dictionary.Add(typeof(Pawn), pawn);
        dictionary.Add(typeof(Queen), queen);
        dictionary.Add(typeof(King), king);
        dictionary.Add(typeof(Knight), knight);
        dictionary.Add(typeof(Bishop), bishop);
        dictionary.Add(typeof(Rook), rook);

        GenerateBoard();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var boardCell in boardCells)
        {

            if (boardCell.figure != null)
                if (boardCell.cell.Figure != null)
                    if (boardCell.cell.Figure != boardCell.figure)
            {
                boardCell.figure = null;
                boardCell.figureView.transform.position = new Vector3(0, -2.0f, 0);
            }

            if (boardCell.figure == null)
                continue;
                

            if (boardCell.Row == boardCell.figure.Position.Row && boardCell.Column == boardCell.figure.Position.Column)
                continue;



            boardCell.figureView.transform.position = boardCells[boardCell.figure.Position.Row, boardCell.figure.Position.Column].cellView.transform.position;
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


                var cellModel = modelBoard.GetCell(row, column);

                viewCell.GetComponent<Selectable>().OnInit(cellModel);

               
                var figureView = CreateFigure(cellModel.Figure, viewCell);

                boardCells[row, column] = new BoardCell(viewCell, figureView, cellModel, cellModel.Figure);
            }

        }
    }

    public GameObject CreateFigure(Figure figure, GameObject cell)
    {
        if (figure == null)
            return null;

        //GameObject figurePrefab = dictionary[figure.GetType()];
        
        /*switch (figure)
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
        }*/

        var figureInctance = Instantiate(figurePrefab, cell.transform.position, Quaternion.identity);

        figureInctance.GetComponent<FigureManager>().OnInit(figure, cell);

        return figureInctance;
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
