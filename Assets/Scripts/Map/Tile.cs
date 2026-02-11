using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileInfo info;
    public Material DefaultMaterial;
    public Material SelectedMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject tileBase;
    private bool eroded = false;
    private static Collider[] overlapResults = new Collider[64];
    public bool wasCheckedByPathGenerator = false;

    public bool hasBeenBuiltOn = false;
    

    public bool selected = false;

    public void Select()
    {
        selected = true;
        meshRenderer.material = SelectedMaterial;
    }

    public void Deselect()
    {
        selected = false;
        meshRenderer.material = DefaultMaterial;
    }

    public void BuildOn()
    {
        meshRenderer.enabled = false;
        hasBeenBuiltOn = true;
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

    public void Erosion(int strength)
    {
        if (eroded || strength <= 0) return;
        eroded = true;

        int count = Physics.OverlapBoxNonAlloc(
            transform.position,
            Vector3.one * 8f,
            overlapResults
        );

        for (int i = 0; i < count; i++)
        {
            Tile tile = overlapResults[i].GetComponent<Tile>();
            if (tile == null || tile == this) continue;

            tile.Erosion(strength - 1);
        }

        if (Random.value < strength / 10f)
        {
            Destroy(gameObject);
        }
    }

}

[System.Serializable]
public class TileInfo
{
    public int x;
    public int y;
    public int c;
    public TileType thisType;
    public float yOffset;
    public void Set(int sx, int sy, int sc, TileType tt, float yO)
    {
        x = sx;
        y = sy;
        c = sc;
        thisType = tt;
        yOffset = yO;
    }
}
