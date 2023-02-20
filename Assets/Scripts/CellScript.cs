using ChessGame;
using ChessGame.Figures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class CellScript : MonoBehaviour
{
    private Material _general;
    private Renderer _renderer;
    
    internal ChessCell chessCell;

    public Material selected;
    public Material marked;
    public Material white;
    public Material black;

    internal int Row => chessCell.Row;
    internal int Column => chessCell.Column;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
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

        _general = _renderer.material;
    }
    public void Select()
    {
        _renderer.material = selected;
    }

    public void Deselect()
    {
        _renderer.material = _general;
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

            else if (_renderer.material != _general)
                _renderer.material = _general;
        }
    }
}
