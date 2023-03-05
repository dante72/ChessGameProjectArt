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
        await GetBoard();
    }

    public override void OnReceiveBoardAction()
    {
        BoardScript.Mode = 1;
        BoardScript.Reload = true;
        MenuScript.HasActiveSession= true;

        if (_board.Player != null)
        {
            if (_board.Player.Color == FigureColor.White)
                CameraPosition.Mode = CameraMode.White;
            else
                CameraPosition.Mode = CameraMode.Black;
        }
    }
}
