using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileInfo info;
    public Material DefaultMaterial;
    public Material SelectedMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    public bool selected = false;

    public void Select()
    {
        selected = true;
        meshRenderer.material = SelectedMaterial;
    }
}

[System.Serializable]
public class TileInfo
{
    public int x;
    public int y;
    public int c;

    public void Set(int sx, int sy, int sc)
    {
        x = sx;
        y = sy;
        c = sc;
    }
}
