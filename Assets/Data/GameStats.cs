using UnityEngine;

[CreateAssetMenu(fileName = "GameStats", menuName = "Scriptable Objects/GameStats")]
public class GameStats : ScriptableObject
{
    public int MaxResidents;
    public int CurrentResidents;
}
