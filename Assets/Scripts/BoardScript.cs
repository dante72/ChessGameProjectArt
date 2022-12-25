using ChessGame;
using ChessGame.Figures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    private const int rows = 8, columns = 8;
    private float scale = 2;

    internal readonly ChessBoard chessBoard = new ChessBoard(true, true);
    internal readonly GameObject[,] cells = new GameObject[rows, columns];

    public GameObject figureObject;
    public GameObject cellPrefab;
    public Material black;
    public Material white;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
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

                var cellModel = chessBoard.GetCell(row, column);

                viewCell.GetComponent<CellScript>().OnInit(cellModel);

                CreateFigure(cellModel.Figure, viewCell);

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
