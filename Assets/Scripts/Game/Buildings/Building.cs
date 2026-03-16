using System.Collections;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public Building thisBuilding;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);

        if (thisBuilding.type == Building.Types.Residential)
        {
            GameManager.Instance.stats.MaxResidents += thisBuilding.personCapacity;
        }

        if (thisBuilding.type == Building.Types.Productive)
        {
            foreach (MaterialProduce p in thisBuilding.materialsProduce)
            {
                StartCoroutine(ProduceMaterial(p));
            }
        }
    }

    private IEnumerator ProduceMaterial(MaterialProduce produce)
    {
        while (true)
        {
            yield return new WaitForSeconds(produce.delay);

            Player.Instance.AddMaterial(produce.material.name, produce.amount);
        }
    }
}