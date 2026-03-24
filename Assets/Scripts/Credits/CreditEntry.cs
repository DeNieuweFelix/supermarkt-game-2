using UnityEngine;

[CreateAssetMenu(fileName = "CreditEntry", menuName = "Scriptable Objects/CreditEntry")]
public class CreditEntry : ScriptableObject
{
    public string name;
    public Texture2D image;

    [TextArea(15,20)]
    public string role;
}
