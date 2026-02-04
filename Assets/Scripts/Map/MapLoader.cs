using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public static MapLoader Instance;
    public List<MapChunkGenerator> chunks = new List<MapChunkGenerator>();
    public TileType lastChunkFinalType = null;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
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

    private IEnumerator StartErosion()
    {
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("Tile");

        int amountErosionTiles = Random.Range(2, 5);
        
        List<GameObject> chosenErosionTiles = new List<GameObject>();
        
        for(int i = 0; i < amountErosionTiles; i++)
        {
            chosenErosionTiles.Add(Tiles[Random.Range(0, Tiles.Length)]);
            yield return new WaitForSeconds(0.1f);
        }

        foreach(GameObject t in chosenErosionTiles)
        {
            t.GetComponent<Tile>().Erosion(50);
            yield return new WaitForSeconds(0.1f);
        }

    }

    private IEnumerator LoadSequence()
    {
        yield return new WaitForSeconds(0.1f);

        int i = 1;
        foreach(MapChunkGenerator c in chunks)
        {
            c.gameObject.SetActive(true);
            c.gameObject.name = "c_" + i;
            c.StartLoad(i);

            i++;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(StartErosion());

        yield return new WaitForSeconds(0.2f);

        EnemyPathGenerator.Instance.Generate();
    }
}
