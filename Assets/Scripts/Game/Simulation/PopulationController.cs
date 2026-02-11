using System.Collections;
using UnityEngine;

public class PopulationController : MonoBehaviour
{
    [Header("[0] = min, [1] = max")]
    public int[] influenceLimits = new int[2];

    void Start()
    {
        StartCoroutine(InfluencePopulation());
    }

    private IEnumerator InfluencePopulation()
    {
        while (true)
        {
            if(GameManager.Instance.stats.CurrentResidents < GameManager.Instance.stats.MaxResidents)
            {
                int toAdd = Random.Range(influenceLimits[0], influenceLimits[1]);

                //add more if there's a lot of buildings
                toAdd += Mathf.FloorToInt((GameManager.Instance.stats.MaxResidents - GameManager.Instance.stats.CurrentResidents) / 75);

                MainResourceUpdater.Instance.UpdatePopulationColor(toAdd);

                GameManager.Instance.stats.CurrentResidents = Mathf.Clamp(
                    GameManager.Instance.stats.CurrentResidents + toAdd,
                    0,
                    GameManager.Instance.stats.MaxResidents
                );
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
