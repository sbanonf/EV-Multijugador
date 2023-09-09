using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GlobalManagers : MonoBehaviour
{
    public static GlobalManagers instance { get; private set; }

    [SerializeField] private GameObject parentDB;
    [field: SerializeField] public NetworkController networkController { get; private set; }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(parentDB);
        }

    }
}
