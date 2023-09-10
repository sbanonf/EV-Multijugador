using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef networkPrefabRef = NetworkPrefabRef.Empty;
    [SerializeField] private Canvas canvas;
    public override void Spawned()
    {
        if (Runner.IsServer) {
            foreach (var player in Runner.ActivePlayers) {
                SpawnPlayer(player);
            }
        }
    }

    private void SpawnPlayer(PlayerRef playerref) {

        if (Runner.IsServer) {
            var texto  = Runner.Spawn(networkPrefabRef, transform.position, Quaternion.identity, playerref);
            var original = GameObject.FindWithTag("CanvasPrincipal");
            texto.transform.SetParent(original.transform);
            var rectTransform = texto.GetComponent<RectTransform>();
            rectTransform.SetAsLastSibling();
            texto.gameObject.SetActive(Object.HasStateAuthority);
        }
    }
    public void PlayerJoined(PlayerRef player)
    {
        SpawnPlayer(player);
    }

    public void PlayerLeft(PlayerRef player)
    {
        Despawnplayer(player);
    }

    private void Despawnplayer(PlayerRef player)
    {
        if (Runner.IsServer) {
            if (Runner.TryGetPlayerObject(player, out var networkObject)) {
                Runner.Despawn(networkObject);
            }
            Runner.SetPlayerObject(player, null);
        }
    }
}
