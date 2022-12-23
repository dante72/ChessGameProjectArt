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
    private Renderer renderer;

    private void Start()
    {
        if (cell != null)
        {
            ((IChessObservable)cell).Subscribe(this);
        }

        renderer = GetComponent<Renderer>();

        deselected = renderer.material;
        
    }
    public void Select()
    {
        renderer.material = selected;
    }

    public void Deselect()
    {
        renderer.material = deselected;
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
                renderer.material.color = Color.green;

        }
    }

    public Task UpdateAsync()
    {
        //throw new NotImplementedException();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        ((IChessObservable)cell).Remove(this);
    }
}
