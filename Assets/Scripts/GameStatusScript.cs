using ChessGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer1Script : MonoBehaviour
{
    public GameObject board;
    private BoardScript boardScript;
    public TextMeshProUGUI timer1;
    // Start is called before the first frame update
    void Start()
    {
        boardScript = board.GetComponent<BoardScript>();
    }

    // Update is called once per frame
    void Update()
    {
        var timer1Text = boardScript._chessBoard.Player?.Timer.Value;
        timer1.text = $"{timer1Text:hh\\:mm\\:ss}";
    }
}
