using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using Fusion.Sockets;
using System;

public class NetworkController : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner networkRunnerPrefab;
    private NetworkRunner networkRunnerinstance;

    public event Action OnStartedRunnerConnection;
    public event Action OnplayerJoinedSuccesfully;
    public string localplayer { get; private set; }
    public async void StartGame(GameMode mode, string roomName) 
    {
        OnStartedRunnerConnection?.Invoke();
        if (networkRunnerinstance == null) {
            networkRunnerinstance = Instantiate(networkRunnerPrefab);
        }
        networkRunnerinstance.AddCallbacks(this);
        networkRunnerinstance.ProvideInput = true;
        var startgameArgs = new StartGameArgs() {
            GameMode = mode,
            SessionName = roomName,
            PlayerCount = 20,
            SceneManager = networkRunnerinstance.GetComponent<INetworkSceneManager>()
        };
        var result = await networkRunnerinstance.StartGame(startgameArgs);
        if (result.Ok)
        {
            const string SCENE_NAME = "MainGame";
            networkRunnerinstance.SetActiveScene(SCENE_NAME);
        }
        else {
            Debug.LogError($"FailedtoStart: {result.ShutdownReason}");
        }
    }
    
    public void ShutdownRunner() {
        networkRunnerinstance.Shutdown();
    }

    public void SetPlayerNickname(string nickname) {
        localplayer = nickname;
    }
    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined");
        OnplayerJoinedSuccesfully?.Invoke();

    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log("OnConnectedToServer");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        const String SCENE = "SampleScene";
        SceneManager.LoadScene(SCENE);
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("OnConnectedToServer");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
