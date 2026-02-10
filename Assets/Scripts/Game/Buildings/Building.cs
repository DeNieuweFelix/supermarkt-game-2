using System.Collections;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public Building thisBuilding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        //add amount of residents when residential building is built
        Building.Types type = thisBuilding.type;

        if(type == Building.Types.Residential)
        {
            GameManager.Instance.stats.MaxResidents += thisBuilding.personCapacity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
