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
        Debug.Log(Runner.SessionInfo);
        if (Runner.IsServer) {
            Debug.Log(Runner.SessionInfo);
            var original = GameObject.FindWithTag("CanvasPrincipal");
            var texto = Runner.Spawn(networkPrefabRef, original.gameObject.transform.position, Quaternion.identity, playerref);
            texto.transform.SetParent(original.transform);

            // Verifica si el objeto se generó correctamente y tiene la jerarquía y posición correctas
            if (texto != null)
            {
               
                // Obtén el RectTransform del objeto generado
                RectTransform rectTransform = texto.GetComponent<RectTransform>();

                // Establece el objeto como el último hijo del Canvas
                rectTransform.SetAsLastSibling();

                // Asegúrate de que la posición local del RectTransform esté en (0, 0)
                rectTransform.localPosition = Vector3.zero;

                // Asegúrate de que la escala del RectTransform sea la correcta (1, 1, 1)
                rectTransform.localScale = Vector3.one;

                rectTransform.pivot = new Vector2(0, 1f);
                rectTransform.anchoredPosition = new Vector2(95, -268.5f);
                // Establece el objeto generado como el objeto del jugador
                Runner.SetPlayerObject(playerref, texto);

            }
        }
    }

    
    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log(player.PlayerId);
        SpawnPlayer(player);
        var original = GameObject.FindWithTag("CanvasPrincipal");
        var objeto = Runner.TryGetPlayerObject(player, out NetworkObject obj2to);
        obj2to.transform.SetParent(original.transform);
        // Verifica si el objeto se generó correctamente y tiene la jerarquía y posición correctas
        if (obj2to != null)
        {

            // Obtén el RectTransform del objeto generado
            RectTransform rectTransform = obj2to.GetComponent<RectTransform>();

            // Establece el objeto como el último hijo del Canvas
            rectTransform.SetAsLastSibling();

            // Asegúrate de que la posición local del RectTransform esté en (0, 0)
            rectTransform.localPosition = Vector3.zero;

            // Asegúrate de que la escala del RectTransform sea la correcta (1, 1, 1)
            rectTransform.localScale = Vector3.one;

            rectTransform.pivot = new Vector2(0, 1f);
            rectTransform.anchoredPosition = new Vector2(95, -268.5f);
            // Establece el objeto generado como el objeto del jugador
        }
        SetearFalse(player);
    }

    private void SetearFalse(PlayerRef player)
    {
        
        foreach (var p in Runner.ActivePlayers)
        {
            //1 == 0
            if (p.PlayerId != player.PlayerId)
            {
                var o = Runner.TryGetPlayerObject(player, out NetworkObject obj2to);
                obj2to.gameObject.SetActive(false);
                if (obj2to.HasInputAuthority) {
                    obj2to.gameObject.SetActive(true);
                } 
            }
        }
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
