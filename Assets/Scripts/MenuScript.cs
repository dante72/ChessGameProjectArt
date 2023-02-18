using ChessGameClient.AuthWebAPI;
using ChessGameClient.Services;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject menuComponent;
    private bool visible = true;
    public static bool flag = false;

    public static bool closeMenu = false;
    public TextMeshProUGUI errorMessage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || closeMenu)
        {
            closeMenu = false;

            visible = !visible;
            menuComponent.SetActive(visible);
        }
    }

    public async void NewGame()
    {
        Debug.Log("Start game");

        if (await GameClientV2.Client.authWebApi.SessionExists())
            await GameClientV2.Client.gameHubService.GetBoard();
        else
        {
            var status = await GameClientV2.Client.authWebApi.AddOrRemovePlayer();

            if (!status)
            {
                Debug.Log("Search!");
                errorMessage.text = "Search!";
            }
            else
            {
                Debug.Log("Stop search!");
                errorMessage.text = "";
            }
        }
    }

    public void StartScene()
    {
        //await GameClientV2.Client.gameHubService.GetBoard();
        SceneManager.LoadScene(1);
    }

}
