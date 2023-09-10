using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
     public TextMeshProUGUI nombre;
    // public TextMeshProUGUI balance;
    // public TextMeshProUGUI Ganancias;
     public TextMeshProUGUI n;
     public TextMeshProUGUI contador;
    private void SetLocalObjects()
    {

        if (Runner.LocalPlayer == Object.HasInputAuthority)
        {
            var playerName = GlobalManagers.instance.networkController.localplayer;
            var b = GlobalManagers.instance.networkController.localname;
            var c = GlobalManagers.instance.networkController.conta;
            Debug.Log(playerName);
            RpcSetNickName(playerName);
            RpcSetName(b);
            RpcSetConta(c);
        }
    }
    [Networked(OnChanged = nameof(OnContachange))] private NetworkString<_4> contador2 { get; set; }
    private static void OnContachange(Changed<PlayerInfo> changed)
    {
        changed.Behaviour.SetPlayerNickname(changed.Behaviour.playerName);
    }

    [Rpc(sources: RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetConta(NetworkString<_4> conta)
    {
        contador2 = conta;
        SetPlayerConta(playerName);
    }
    private void SetPlayerConta(NetworkString<_16> nickname)
    {
        contador.text = "apostador " + contador2;
    }
    [Networked(OnChanged = nameof(OnNicknamechange))] private NetworkString<_16> playerName { get; set; }
    private static void OnNicknamechange(Changed<PlayerInfo> changed)
    {
        changed.Behaviour.SetPlayerNickname(changed.Behaviour.playerName);
    }

    [Rpc(sources: RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetNickName(NetworkString<_16> nickName)
    {
        playerName = nickName;
        SetPlayerNickname(playerName);
    }
    private void SetPlayerNickname(NetworkString<_16> nickname)
    {
        nombre.text = nickname + " " ;
    }
  /*  [Networked(OnChanged = nameof(OnBalancechange))] private NetworkString<_4> playerBalance { get; set; }
    private static void OnBalancechange(Changed<PlayerInfo> changed)
    {
        changed.Behaviour.SetBalance(changed.Behaviour.playerBalance);
    }

    [Rpc(sources: RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetBalance(NetworkString<_4> balance)
    {
        playerBalance = balance;
        SetBalance(balance);
    }
    private void SetBalance(NetworkString<_4> pbalance)
    {
        balance.text = " "+ pbalance + " ";
    }
    [Networked(OnChanged = nameof(OnGananciaschange))] private NetworkString<_4> ganancia { get; set; }
    private static void OnGananciaschange(Changed<PlayerInfo> changed)
    {
        changed.Behaviour.SetGanancias(changed.Behaviour.ganancia);
    }

    [Rpc(sources: RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetGanancias(NetworkString<_4> ganancias)
    {
        ganancia = ganancias;
        SetGanancias(ganancia);
    }
    private void SetGanancias(NetworkString<_4> ganancia)
    {
        nombre.text = " "+ ganancia + " ";
    } */
    [Networked(OnChanged = nameof(Onnamechange))] private NetworkString<_16>localname { get; set; }
    private static void Onnamechange(Changed<PlayerInfo> changed)
    {
        changed.Behaviour.SetPlayername(changed.Behaviour.localname);
    }

    [Rpc(sources: RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetName(NetworkString<_16> nickName)
    {
        localname = nickName;
        SetPlayername(playerName);
    }
    private void SetPlayername(NetworkString<_16> ni)
    {
        n.text = ni + " ";
    }
    public override void Spawned()
    {   
        base.Spawned();
        SetLocalObjects();
    }


}
