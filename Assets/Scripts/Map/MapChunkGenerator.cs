using System.Collections;
using UnityEngine;

public class MapChunkGenerator : MonoBehaviour
{
    public GameObject tile;
    public Vector2 chunkSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLoad()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        for(int i = 0; i < chunkSize.x; i++)
        {
            for(int j = 0; j < chunkSize.y; j++)
            {
                GameObject t = Instantiate(tile, transform, true);

                Vector3 pos = transform.position - new Vector3(20f, 0f, 20f) + (new Vector3(i, 0f, j) * 10f);

                t.transform.position = pos;
                t.transform.rotation = Quaternion.identity;

                yield return null;
            }
        }
    }
}
