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
        foreach(MaterialCost mc in building.materialsCosts)
        {
            string m = mc.material.name;
            int c = Player.Instance.GetMaterial(m);

            if(mc.cost > c)
            {
                Debug.LogWarning("not enough resources!");
                return;
            }
        }

        Debug.Log("enough resources!");

        foreach(MaterialCost mc in building.materialsCosts)
        {
            string m = mc.material.name;
            
            Player.Instance.RemoveMaterial(m, mc.cost);
        }

        Debug.Log("removed resources from player!");     

        Tile tileToBuildOn = PlayerTileGetter.Instance.tileSelected;

        if (tileToBuildOn.hasBeenBuiltOn)
        {
            Debug.LogWarning("has been built on already!");
            return;
        }

        Debug.Log("attempting to build tile on: x" + tileToBuildOn.info.x + " y: " + tileToBuildOn.info.y);
        GameObject tileGOB = tileToBuildOn.gameObject;

        GameObject b = Instantiate(building.model);

        b.transform.position = tileGOB.transform.position + Vector3.up * (tileToBuildOn.info.yOffset / 10f);
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

        BuildingScript buildingScript = b.AddComponent<BuildingScript>();
        buildingScript.thisBuilding = building;
    }

    public void SetPreview()
    {
        Debug.Log(building);
        BuildingPreviewManager.Instance.SetBuildingPreview(building);
    }
}
