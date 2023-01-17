using ChessGame;
using ChessGame.Figures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class BoardScript : MonoBehaviour
{
    private const int rows = 8, columns = 8;
    private float scale = 2;

    private ChessBoard _chessBoard;

    internal readonly GameObject[,] cells = new GameObject[rows, columns];
    internal readonly List<GameObject> figures = new List<GameObject>();

    public GameObject figureObject;
    public GameObject cellPrefab;
    public Material black;
    public Material white;
    public static int Mode = 0;

    public static bool Reload = false;

    // Start is called before the first frame update
    void Start()
    {

        GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Reload)
        {
            Reload = false;
            RegenerateBoard();
        }
    }

    public void RegenerateBoard()
    {
        foreach (var figure in figures)
            Destroy(figure);

        foreach(var cell in cells)
            Destroy(cell);

        figures.Clear();

        GenerateBoard();
    }

    public void GenerateBoard()
    {
        switch (Mode)
        {
            case 0:
                _chessBoard = new ChessBoard(true, true);
                break;
            case 1:
                _chessBoard = GameClientV2.Client.chessBoard;
                break;
        }

        for (int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                var viewCell = Instantiate(cellPrefab, new Vector3(column * scale, 0, (rows - 1 - row) * scale), Quaternion.identity);

                var cellModel = _chessBoard.GetCell(row, column);

                viewCell.GetComponent<CellScript>().OnInit(cellModel);

                figures.Add(CreateFigure(cellModel.Figure, viewCell));

                cells[row, column] = viewCell;
            }
        }
    }

    public GameObject CreateFigure(Figure figure, GameObject cell)
    {
        if (figure == null)
            return null;

        var figureInctance = Instantiate(figureObject, cell.transform.position, Quaternion.identity);

        figureInctance.GetComponent<FigureScript>().OnInit(figure, cell);

        return figureInctance;
    }
}
