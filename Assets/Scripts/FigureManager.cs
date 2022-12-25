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
    private BoardManager chessBoard;
    private GameObject cell;
    private Cell Selectable;
    private GameObject figureInstance;

    public GameObject pawn;
    public GameObject knight;
    public GameObject bishop;
    public GameObject rook;
    public GameObject queen;
    public GameObject king;
    public Material white;
    public Material black;
    public GameObject chessBoardObject;



    private bool hasInitialized = false;

    internal GameObject Cell {
        get => cell;
        set
        {
            cell = value;
            if (cell)
            {
                Selectable = cell.GetComponent<Cell>();
                transform.position = cell.transform.position;
            }
            else
            {
                Selectable = null;
                transform.position = new Vector3(-2, 0, -2);
            }
        }
    }


    private Figure _figure;
    internal Figure Figure
    {
        get => _figure;
        set
        {
            _figure = value;
        }
    }
    void Awake()
    {
        chessBoard = chessBoardObject.GetComponent<BoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasInitialized)
            return;

        if (Cell == null)
            return;

        if (Figure.Position.Figure != Figure)
        {
            RemoveFigure();
            return;
        }

        if (Figure.Position.Row == Selectable.Row && Figure.Position.Column == Selectable.Column)
            return;
        
        Cell = chessBoard.cells[Figure.Position.Row, Figure.Position.Column];
    }

    internal void OnInit(Figure figure, GameObject cell)
    {
        Figure = figure;
        Cell = cell;

        CreateFigure(figure, cell);

        hasInitialized = true;
    }

    private void RemoveFigure()
    {
        Cell = null;
    }

    private GameObject CreateFigure(Figure figure, GameObject cell)
    {
        if (figure == null)
            return null;

        GameObject figurePrefab = null;
        switch (figure)
        {
            case Pawn:
                figurePrefab = pawn;
                break;
            case Knight:
                figurePrefab = knight;
                break;
            case Rook:
                figurePrefab = rook;
                break;
            case Bishop:
                figurePrefab = bishop;
                break;
            case Queen:
                figurePrefab = queen;
                break;
            case King:
                figurePrefab = king;
                break;
        }

        figureInstance = Instantiate(figurePrefab, cell.transform.position, Quaternion.identity);

        figureInstance.transform.SetParent(transform);

        figureInstance.transform.position += CorrectTransform(figure);

        transform.rotation = figure.Color == FigureColor.White
                        ? Quaternion.identity
                        : Quaternion.Euler(0, 180, 0);

        GetComponentInChildren<Renderer>().material = figure.Color == FigureColor.White ? white : black;

        return figureInstance;
    }

    private Vector3 CorrectTransform(Figure figure)
    {
        return figure is Pawn ? new Vector3(0, 0.5f) : new Vector3(0, 0.7f);
    }
}
