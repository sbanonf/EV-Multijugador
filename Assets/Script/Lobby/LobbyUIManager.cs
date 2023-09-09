using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LobbyPanelBase[] lobbyPanel;
    [SerializeField] public LoadingCanvasController LoadingCanvasControllerprefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var lobby in lobbyPanel) {
            lobby.InitPanel(this);
        }

        Instantiate(LoadingCanvasControllerprefab);
    }

    public void ShowPanel(LobbyPanelBase.LobbyPanelType type) {
        foreach (var lobby in lobbyPanel) {
            if (lobby.PanelType == type) {
                lobby.ShowPanel();
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
