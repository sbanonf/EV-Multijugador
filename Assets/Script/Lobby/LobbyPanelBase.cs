using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanelBase : MonoBehaviour
{
    [field: SerializeField,Header("Variables de Lobby")]
    [SerializeField] public LobbyPanelType PanelType { get; private set; }
    protected LobbyUIManager lobbyUIManager;
    public enum LobbyPanelType { 
        NONE,
        CREATENAME,
        CREATEROOM
    }

    public virtual void InitPanel(LobbyUIManager UIManager) {
        lobbyUIManager = UIManager;
    }
    public void ShowPanel() {
        this.gameObject.SetActive(true);
    }
    public void ClosePanel() {
        this.gameObject.SetActive(false);
    }
}
