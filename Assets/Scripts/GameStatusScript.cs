using ChessGame;
using ChessGameClient.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameStatusScript : MonoBehaviour
{
    public GameObject board;
    private BoardScript boardScript;
    public TextMeshProUGUI timer1;
    public TextMeshProUGUI timer2;
    public TextMeshProUGUI gameStatus;

    private ChessBoard chessBoard => boardScript._chessBoard;
    // Start is called before the first frame update
    void Start()
    {
        boardScript = board.GetComponent<BoardScript>();
        timer1.text = string.Empty;
        timer2.text = string.Empty;
        gameStatus.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (chessBoard == null || chessBoard.Player == null)
            return;

        gameStatus.text = chessBoard.GameStatusDescription;

        var player1 = chessBoard.Player;
        var player2 = chessBoard.Players.First(p => p != player1);

        var timer1Text = player1?.Timer.Value;
        timer1.text = $"{timer1Text:hh\\:mm\\:ss}";

        var timer2Text = player2?.Timer.Value;
        timer2.text = $"{timer2Text:hh\\:mm\\:ss}";
    }
}
