using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public string name;
    public List<MaterialOwned> MaterialsOwned = new List<MaterialOwned>();
}

[System.Serializable]
public class MaterialOwned
{
    public p_Material material;
    public int amount;
}
