using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MapChunkGenerator : MonoBehaviour
{
    public GameObject tile;
    public Vector2 chunkSize;
    public List<TileType> tileTypes = new List<TileType>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLoad(int index)
    {
        StartCoroutine(Generate(index));
    }

    private IEnumerator Generate(int index)
    {
        int typeSize = 0;
        int sizeLeft = 0;
        TileType type = null;

        if(MapLoader.Instance.lastChunkFinalType != null)
        {
            type = MapLoader.Instance.lastChunkFinalType;
            typeSize = Mathf.RoundToInt(type.maxSize / 1.5f);
            sizeLeft = typeSize;
        }

        for(int i = 0; i < chunkSize.x; i++)
        {
            for(int j = 0; j < chunkSize.y; j++)
            {
                // if(Random.Range(0, 10) > 6)
                // {
                //     continue;
                // }

                if(type == null)
                {
                    if(Random.Range(0, 10) > 5)
                    {
                        type = tileTypes[Random.Range(1, tileTypes.Count)];
                    }
                    else
                    {
                        type = tileTypes[0];
                    }

                    typeSize = type.maxSize;
                    sizeLeft = type.maxSize;
                }

                GameObject t = Instantiate(tile, transform, true);

                Vector3 pos = transform.position - new Vector3(20f, -2f, 20f) + (new Vector3(i, 0f, j) * 10f);

                t.transform.position = pos;
                t.transform.rotation = Quaternion.identity;

                Tile tTile = t.GetComponent<Tile>(); 
                tTile.info.Set(i, j, index, type);

                tTile.Init();

                sizeLeft--;
                if(Random.Range(0, typeSize) > sizeLeft)
                {
                    type = null;
                }

                LoadTextUpdater.Instance.UpdateUI("generating tile: x" + i + " y:" + j + "(" + index + ")");

                if(i == chunkSize.x - 1 && j == chunkSize.y - 1)
                {
                    Debug.Log("final tile reached!");
                    MapLoader.Instance.lastChunkFinalType = type;
                }

                yield return null;
            }
        }
    }
}

