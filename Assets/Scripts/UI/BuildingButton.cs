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

        if(building.type == Building.Types.Residential)
        {
            int rot;
            int ran = Random.Range(0, 100);

            if(ran > 75)
            {
                rot = 0;
            }else if(ran > 50)
            {
                rot = 90;
            }else if(ran > 25)
            {
                rot = 0;
            }
            else
            {
                rot = -90;
            }

            b.transform.localRotation = Quaternion.Euler(0f, rot, 0f);
        }
    }
}
