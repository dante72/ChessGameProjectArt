using ChessGame;
using ChessGame.Figures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class Selectable : MonoBehaviour, IChessObserver, IDisposable
{
    private Material deselected;
    public Material selected;
    public Material marked;
    internal ChessCell cell;
    public GameObject pawn;
    public GameObject knight;
    public GameObject bishop;
    public GameObject rook;
    public GameObject queen;
    public GameObject king;
    public Material white;
    public Material black;
    //private GameObject figure;
    private GameObject view;

    private void Start()
    {
        if (cell != null)
        {
            ((IChessObservable)cell).Subscribe(this);
            CreateFigure();
        }


        deselected = GetComponent<Renderer>().material;
        view = GetComponent<GameObject>();
        
    }
    public void Select()
    {
        GetComponent<Renderer>().material = selected;
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material = deselected;
    }

    public void Click()
    {
        cell.Click();
    }

    void Update()
    {
        if (cell != null)
        {
            if (cell.IsMarked)
                GetComponent<Renderer>().material.color = Color.green;

        }
    }

    void CreateFigure()
    {
        GameObject figureView = null;
        Figure figureModel = cell.Figure;
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
            var f = Instantiate(figureView, GetComponent<Renderer>().transform.position, figureModel.Color == FigureColors.White
                                                                                ? Quaternion.identity
                                                                                : Quaternion.Euler(0, 180, 0));

            f.GetComponent<Renderer>().material = figureModel.Color == FigureColors.White ? white : black;

        }
    }

    public Task UpdateAsync()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        ((IChessObservable)cell).Remove(this);
    }
}
