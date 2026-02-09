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
    public Types type;
    public List<p_Material> materialsProduce = new List<p_Material>();
    public List<p_Material> materialsCost = new List<p_Material>();
}

