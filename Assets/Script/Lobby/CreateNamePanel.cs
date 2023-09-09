using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CreateNamePanel : LobbyPanelBase
{
    [Header("Variables de Crear Nombre")]
    [SerializeField] private TMP_InputField nombre;
    [SerializeField] private TMP_InputField apellido;
    [SerializeField] private Button Siguiente;
    private const int MAX_CHARACTER_NICKNAME = 30;
    // Start is called before the first frame update
    public override void InitPanel(LobbyUIManager uIManager)
    {
        base.InitPanel(uIManager);
        Siguiente.interactable = false;
        Siguiente.onClick.AddListener(onClickSiguiente);
        nombre.onValueChanged.AddListener(Oninputvaluechanged);
        apellido.onValueChanged.AddListener(Oninputvaluechanged);

    }

    private void Oninputvaluechanged(string arg0)
    {
        Siguiente.interactable = arg0.Length <= MAX_CHARACTER_NICKNAME;
    }

    private void onClickSiguiente()
    {
        var nickname = nombre.text + apellido.text;
        if (nickname.Length <= MAX_CHARACTER_NICKNAME && nombre.text.Length > 0 && apellido.text.Length > 0) {
            base.ClosePanel();
            lobbyUIManager.ShowPanel(LobbyPanelType.CREATEROOM);
            GlobalManagers.instance.networkController.SetPlayerNickname(nickname);
            
        }
    }


}
