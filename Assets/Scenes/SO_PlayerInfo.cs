using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Data")]
public class SO_PlayerInfo : ScriptableObject
{
    public string characterName;
    public int characterID;
    public float balance;
}
