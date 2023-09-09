using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
     public TextMeshProUGUI nombre;

    private void SetLocalObjects()
    {

        if (Runner.LocalPlayer == Object.HasInputAuthority)
        {
            playerName = GlobalManagers.instance.networkController.localplayer;
            SetPlayerNickname(playerName);
        }
    }

    [Networked(OnChanged = nameof(OnNicknamechange))] private NetworkString<_16> playerName { get; set; }
    private static void OnNicknamechange(Changed<PlayerInfo> changed)
    {
        changed.Behaviour.SetPlayerNickname(changed.Behaviour.playerName);
    }

    private void SetPlayerNickname(NetworkString<_16> nickname)
    {
        nombre.text = nickname + " " + Object.InputAuthority.PlayerId;
    }
    public override void Spawned()
    {
        base.Spawned();
        nombre = GameObject.FindWithTag("Nombre").GetComponent<TextMeshProUGUI>();
        SetLocalObjects();
    }


}
