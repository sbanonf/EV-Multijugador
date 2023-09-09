using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvasController : MonoBehaviour
{
    [SerializeField] private Button cancelbtn;
    private NetworkController networkcontroller;
    private void Start()
    {
        networkcontroller = GlobalManagers.instance.networkController;
        networkcontroller.OnStartedRunnerConnection += onStartedRunnerConnection;
        networkcontroller.OnplayerJoinedSuccesfully += onplayerJoinedSuccesfully;
        cancelbtn.onClick.AddListener(networkcontroller.ShutdownRunner);
        this.gameObject.SetActive(false);
    }

    private void onplayerJoinedSuccesfully()
    {
        Debug.Log("Me uni");
        
    }

    private void onStartedRunnerConnection()
    {
        this.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        networkcontroller.OnplayerJoinedSuccesfully -= onplayerJoinedSuccesfully;
        networkcontroller.OnStartedRunnerConnection -= onStartedRunnerConnection;
    }
    
}
