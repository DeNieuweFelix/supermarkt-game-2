using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "Scriptable Objects/Material")]
public class p_Material : ScriptableObject
{
    public string name;
    public int ProductionAmount;
    public float ProductionDelay;
    public Material material;
}
