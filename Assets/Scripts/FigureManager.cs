using ChessGame.Figures;
using ChessGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System;

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
    internal GameObject cell;
    private GameObject figureView;

    internal GameObject Cell {
        get => cell;
        set
        {
            cell = value;
            figureView.transform.position = cell.transform.position;
        }
    }


    private Figure _figure;
    internal Figure Figure
    {
        get => _figure;
        set
        {
            _figure = value;
            //if (_figure.Position!= null)
            //{
            //    var fp = _figure.Position;
            //    var cell = boardManager.boardCells[fp.Row, fp.Column].cellView;
            //    CreateFigure(_figure, cell);
            //}


        }
    }
    void Awake()
    {
        boardManager = chessBoard.GetComponent<BoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Init(FigureColors color)
    {
        transform.rotation = color == FigureColors.White
            ? Quaternion.identity
            : Quaternion.Euler(0, 180, 0);

        GetComponentInChildren<Renderer>().material = color == FigureColors.White ? white : black;
    }
}
