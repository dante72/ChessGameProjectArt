using ChessGame.Figures;
using ChessGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    internal Figure figure;
    // Start is called before the first frame update
    void Start()
    {
        CreateFigure();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateFigure()
    {
        if (figure == null || figure.Position == null)
            return;

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

        if (figureView != null)
        {
            var f = Instantiate(figureView, GetComponent<Renderer>().transform.position, figure.Color == FigureColors.White
                                                                                ? Quaternion.identity
                                                                                : Quaternion.Euler(0, 180, 0));

            f.GetComponent<Renderer>().material = figure.Color == FigureColors.White ? white : black;

        }
    }
}
