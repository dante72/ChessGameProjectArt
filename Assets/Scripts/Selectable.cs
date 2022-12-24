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
    internal ChessCell cell;
    private Renderer _renderer;

    internal int Row => cell.Row;
    internal int Column => cell.Column;

    private bool isRendered = true;

    private void Start()
    {
        if (cell != null)
        {
            ((IChessObservable)cell).Subscribe(this);
        }

        _renderer = GetComponent<Renderer>();

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
        await cell.Click();
    }

    void Update()
    {
        UpdateCell();

    }

    private void UpdateCell()
    {
        if (cell != null)
        {
            if (cell.IsMarked)
                _renderer.material = marked;
            else
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
        ((IChessObservable)cell).Remove(this);
    }
}
