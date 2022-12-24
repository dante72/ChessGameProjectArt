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
    private Material general;
    public Material selected;
    public Material marked;
    internal ChessCell chessCell;
    private Renderer _renderer;
    public Material white;
    public Material black;

    internal int Row => chessCell.Row;
    internal int Column => chessCell.Column;

    private bool isRendered = true;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    private void Start()
    {
        if (chessCell != null)
        {
            ((IChessObservable)chessCell).Subscribe(this);
        }
    }

    public void OnInit(ChessCell chessCell)
    {
        this.chessCell = chessCell;
        if (Row % 2 == Column % 2)
        {
            _renderer.material = white;
        }
        else
        {
            _renderer.material = black;
        }

        general = _renderer.material;
    }
    public void Select()
    {
        _renderer.material = selected;
    }

    public void Deselect()
    {
        _renderer.material = general;
    }

    public async void Click()
    {
        await chessCell.Click();
    }

    void Update()
    {
        UpdateCell();

    }

    private void UpdateCell()
    {
        if (chessCell != null)
        {
            if (chessCell.IsMarked && _renderer.material != marked)
                    _renderer.material = marked;

            else if (_renderer.material != general)
                _renderer.material = general;
        }
    }

    public Task UpdateAsync()
    {
        //UpdateCell();

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        ((IChessObservable)chessCell).Remove(this);
    }
}
