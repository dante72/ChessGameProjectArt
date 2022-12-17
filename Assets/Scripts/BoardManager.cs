using ChessGame;
using ChessGame.Figures;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private readonly int rows = 8, columns = 8;
    private readonly ChessBoard board = new ChessBoard(true);
    public GameObject cellPrefab;
    private float scale = 2;
    public Material blackCell;
    public Material whiteCell;
    public GameObject pawn;
    public GameObject knight;
    public GameObject bishop;
    public GameObject rook;
    public GameObject queen;
    public GameObject king;
    public Material white;
    public Material black;

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
                var cell = Instantiate(cellPrefab, new Vector3(column * scale, 0, (rows - 1 - row) * scale), Quaternion.identity);
                if (row % 2 == column % 2)
                    cell.GetComponent<Renderer>().material = whiteCell;
                else
                    cell.GetComponent<Renderer>().material = blackCell;

                GameObject figureView = null;
                Figure figureModel = board[row, column];
                switch (figureModel)
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

                if (figureView != null)
                {
                    var f = Instantiate(figureView, new Vector3(column * scale, 0.7f, (rows - 1 - row) * scale), figureModel.Color == FigureColors.White ? Quaternion.identity : Quaternion.Euler(0, 180, 0));
                    f.GetComponent<Renderer>().material = figureModel.Color == FigureColors.White ? white : black;

                }
            }

        }
    }
}
