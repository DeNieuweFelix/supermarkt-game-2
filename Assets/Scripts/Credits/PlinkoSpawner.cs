using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoSpawner : MonoBehaviour
{
    [SerializeField] private float[] spawnLimits = new float[2];
    [SerializeField] private GameObject plinkoBall;
    [SerializeField] private float spawnDelay = 5f;

    [SerializeField] private List<CreditEntry> allEntries = new List<CreditEntry>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnLoop()
    {
        SpawnBall();

        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        if(allEntries.Count == 0) return;
        
        CreditEntry e = allEntries[Random.Range(0, allEntries.Count)];

        Vector3 pos = new Vector3(Random.Range(
            spawnLimits[0],
            spawnLimits[1]
        ), 5f, 0f);

        GameObject b = Instantiate(plinkoBall, pos, Quaternion.identity);

        b.GetComponent<BallScript>().Set(e);
    }
}
