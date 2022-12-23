using ChessGame.Figures;
using ChessGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class FigureManager : MonoBehaviour
{
    public GameObject pawn;
    public GameObject knight;
    public GameObject bishop;
    public GameObject rook;
    public GameObject queen;
    public GameObject king;
    public Material white;
    public Material black;

    public GameObject chessBoard;
    private BoardManager boardManager;


    private Figure _figure;
    internal Figure Figure
    {
        get => _figure;
        set
        {
            _figure = value;
            if (_figure.Position!= null)
            {
                var fp = _figure.Position;
                var cell = boardManager.viewCells[fp.Row, fp.Column];
                CreateFigure(_figure, cell.transform);
            }


        }
    }

    private GameObject this_f;

    private Renderer render;
    // Start is called before the first frame update
    void Awake()
    {
        boardManager = chessBoard.GetComponent<BoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateFigure(Figure figure, Transform transform)
    {
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

        var obj = Instantiate(figureView, transform.position + new Vector3(0, 0.7f, 0), figure.Color == FigureColors.White
                                                                                ? Quaternion.identity
                                                                                : Quaternion.Euler(0, 180, 0));

        obj.GetComponent<Renderer>().material = figure.Color == FigureColors.White ? white : black;
        
    }
}
