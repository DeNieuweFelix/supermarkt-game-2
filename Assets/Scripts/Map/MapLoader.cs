using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public List<MapChunkGenerator> chunks = new List<MapChunkGenerator>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadMap()
    {
        
        GameObject[] chunkGOB = GameObject.FindGameObjectsWithTag("Chunk");

        foreach(GameObject c in chunkGOB)
        {
            chunks.Add(c.GetComponent<MapChunkGenerator>());
            c.SetActive(false);
        }

        StartCoroutine(LoadSequence());
    }

    private IEnumerator LoadSequence()
    {
        foreach(MapChunkGenerator c in chunks)
        {
            c.gameObject.SetActive(true);

            c.StartLoad();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
