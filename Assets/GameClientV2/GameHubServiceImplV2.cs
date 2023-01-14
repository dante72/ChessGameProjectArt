using ChessGame;
using ChessGameClient.AuthWebAPI;
using ChessGameClient.Models;
using ChessGameClient.Services;
using ChessGameClient.Services.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHubServiceImplV2 : GameHubServiceImpl
{
    public GameHubServiceImplV2(
                                ChessBoard board,
                                GameHttpClient httpClient,
                                SiteUserInfo siteUserInfo)
                        : base(
                               board,
                               httpClient,
                               siteUserInfo)
    {
    }

    public override void GetInviteAction()
    {

    }

    public override void CloseInviteAction()
    {

    }

    public async override void GameStartAction()
    {
        //MenuScript.flag = true;
        await GetBoard();
        //SceneManager.LoadScene(1);
        //BoardScript.chessBoard = _board;
        //MenuScript.menuComponent.SetActive(false);
    }

    public override void OnReceiveBoardAction()
    {
        MenuScript.flag = true;
    }
}
