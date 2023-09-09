using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef networkPrefabRef = NetworkPrefabRef.Empty;

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
            Runner.Spawn(networkPrefabRef, Vector3.zero, Quaternion.identity, playerref);
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
