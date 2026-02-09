using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Building building;

    public void Set(Building b)
    {
        image.sprite = b.icon;
        building = b;
    }

    public void Build()
    {
        Tile tileToBuildOn = PlayerTileGetter.Instance.tileSelected;

        Debug.Log("attempting to build tile on: x" + tileToBuildOn.info.x + " y: " + tileToBuildOn.info.y);
        GameObject tileGOB = tileToBuildOn.gameObject;

        GameObject b = Instantiate(building.model);

        b.transform.position = tileGOB.transform.position + Vector3.up * tileToBuildOn.info.yOffset;
        b.transform.SetParent(tileGOB.transform);

        tileToBuildOn.BuildOn();
    }
}
