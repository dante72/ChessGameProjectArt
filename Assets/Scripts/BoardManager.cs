using ChessGame;
using ChessGame.Figures;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private const int rows = 8, columns = 8;
    private readonly ChessBoard modelBoard = new ChessBoard(true);
    internal readonly GameObject[,] viewCells = new GameObject[rows, columns];
    private readonly List<GameObject> figures = new List<GameObject>();

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
        GenerateFigures();
    }

    // Update is called once per frame
    void Update()
    {
        
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

                viewCell.GetComponent<Selectable>().cell = modelBoard.GetCell(row, column);

                viewCells[row, column] = viewCell;
            }

        }
    }

    private void GenerateFigures()
    {
        foreach (var f in modelBoard.Figures)
        {
            var figure = Instantiate(figurePrefab);
            var manager = figure.GetComponent<FigureManager>();

            manager.Figure = f;

        }
    }
}
