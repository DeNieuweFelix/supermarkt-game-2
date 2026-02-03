using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileInfo info;
    public Material DefaultMaterial;
    public Material SelectedMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject tileBase;
    

    public bool selected = false;

    public void Select()
    {
        selected = true;
        meshRenderer.material = SelectedMaterial;
    }

    public void Init()
    {
        tileBase.GetComponent<MeshRenderer>().material = info.thisType.tileMaterial;

        int TileBaseRot = 0;

        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                TileBaseRot = 0;
                break;
            case 1:
                TileBaseRot = 90;
                break;
            case 2:
                TileBaseRot = 180;
                break;
            case 3:
                TileBaseRot = 270;
                break;
        }

        if(Random.Range(0, 10) > 5)
        {
            TileBaseRot = -TileBaseRot;
        }

        tileBase.transform.rotation = Quaternion.Euler(0f, TileBaseRot, 0f);
    }
}

[System.Serializable]
public class TileInfo
{
    public int x;
    public int y;
    public int c;
    public TileType thisType;
    public void Set(int sx, int sy, int sc, TileType tt)
    {
        x = sx;
        y = sy;
        c = sc;
        thisType = tt;
    }
}
