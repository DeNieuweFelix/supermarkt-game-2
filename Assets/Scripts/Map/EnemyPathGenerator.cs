using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPathGenerator : MonoBehaviour
{
    public static EnemyPathGenerator Instance;
    [SerializeField] private GameObject startChunk;
    [SerializeField] private GameObject startTile;

    [SerializeField] private GameObject enemyPath;
    [SerializeField] private Transform pathTransform ;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private EnemyPathRenderer enemyPathRenderer;
    [SerializeField] private bool hasFoundStartingPoint = false;

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
        // pathTransform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        GameObject[] allChunks = GameObject.FindGameObjectsWithTag("Chunk");
        startChunk = allChunks.ToList().Find(x => x.transform.position.z < 0f);

        foreach(Transform t in startChunk.transform)
        {
            Tile tile = t.gameObject.GetComponent<Tile>();
            if(tile != null && (tile.info.x == 0 && tile.info.y == 0))
            {
                startTile = t.gameObject;
                break;
            }
        }

        pathTransform.position = startTile.transform.position + (Vector3.up * 5f);

        Debug.Log("start tile found!");

        StartCoroutine(LayPath());
    }

    private IEnumerator LayPath()
    {
        int zWay = 0;
        int xWay = 1;

        int misses = 0;

        List<Vector3> enemyPathTransforms = new List<Vector3>();

        RaycastHit hit;
        while (misses < 50)
        {

            pathTransform.position += new Vector3(xWay, 0f, zWay);

            if(Physics.Raycast(pathTransform.position, Vector3.down, out hit,  999f, tileLayer))
            {
                if(UnityEngine.Random.Range(0, 10) > 6)
                {
                    hasFoundStartingPoint = true;
                }
                else
                {
                    continue;
                }

                Tile thisTile = hit.collider.gameObject.GetComponent<Tile>();

                if (thisTile.wasCheckedByPathGenerator)
                {
                    continue;
                }

                Vector3 pos;
                pos = hit.collider.gameObject.transform.position;

                Instantiate(enemyPath, pos, Quaternion.identity);
                enemyPathTransforms.Add(pathTransform.position);

                thisTile.wasCheckedByPathGenerator = true;

                if (hasFoundStartingPoint)
                {
                    int way = UnityEngine.Random.Range(0, 100);

                    if(way < 20)
                    {
                        zWay = 0;
                        xWay = 1;
                    }else if(way < 40)
                    {
                        zWay = 0;
                        xWay = -1;
                    }
                    else
                    {
                        zWay = 1;
                        xWay = 0;
                    }
                }

                misses = 0;
            }
            else
            {
                misses++;
            }

            Debug.Log("path generator misses: " + misses);
            yield return new WaitForFixedUpdate();
        }

        enemyPathRenderer.RendererSetup(enemyPathTransforms);
    }
}
