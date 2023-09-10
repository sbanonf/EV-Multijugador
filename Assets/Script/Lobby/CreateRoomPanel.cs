using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;
using UnityEngine.Events;
using System;

public class CreateRoomPanel : LobbyPanelBase
{
    [Header("Room variables")]
    public Button joinRoombtn;
    public Button createRoombtn;
    public Button randomRoombtn;
    
    public TMP_InputField joinRoomField;
    public TMP_InputField createRoomField;
    private NetworkController networkcontroller;

    public override void InitPanel(LobbyUIManager UIManager)
    {

        base.InitPanel(UIManager);
        networkcontroller = GlobalManagers.instance.networkController;
        joinRoombtn.onClick.AddListener(() => CreateRoom(GameMode.Client, joinRoomField.text));
        createRoombtn.onClick.AddListener(() => CreateRoom(GameMode.Host, createRoomField.text));
        randomRoombtn.onClick.AddListener(joinRandom);

    }
    
    private void joinRandom()
    {

        networkcontroller.StartGame(GameMode.AutoHostOrClient, string.Empty);
    }


    private void CreateRoom(GameMode mode, string field)
    {
        if (createRoomField.text.Length >= 2)
        {
            Debug.Log($"-----{mode}-----");
            networkcontroller.StartGame(mode, field);
        }
    }

}
