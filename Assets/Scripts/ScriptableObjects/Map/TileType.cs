using UnityEngine;

[CreateAssetMenu(fileName = "TileType", menuName = "Scriptable Objects/TileType")]
public class TileType : ScriptableObject
{
    public string name;
    public Material tileMaterial;
    public int maxSize = 10;
}
