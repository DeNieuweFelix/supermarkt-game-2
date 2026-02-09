using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    public enum Types
    {
        Residential,
        Productive,
        Offensive,
        Defensive,
    }

    public string name;
    public GameObject model;
    public Sprite icon;
    public Types type;
    public List<MaterialProduce> materialsProduce = new List<MaterialProduce>();
    public List<MaterialCost> materialsCosts = new List<MaterialCost>();
}

[System.Serializable]
public class MaterialProduce
{
    public p_Material material;
    public int amount;
    public int delay;
}
[System.Serializable]
public class MaterialCost
{
    public p_Material material;
    public int cost;
}

