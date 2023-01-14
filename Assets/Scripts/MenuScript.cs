using ChessGameClient.AuthWebAPI;
using ChessGameClient.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menuComponent;
    private bool visible = true;
    public static bool flag = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            visible = !visible;
            menuComponent.SetActive(visible);
        }

        if (flag)
        {
            flag = false;
            StartScene();
        }
    }

    public async void NewGame()
    {
        Debug.Log("Start game");
        await GameClientV2.Client.gameHubService.AddOrRemovePlayer(0);
    }

    public async void StartScene()
    {
        //await GameClientV2.Client.gameHubService.GetBoard();
        SceneManager.LoadScene(1);
    }

}
