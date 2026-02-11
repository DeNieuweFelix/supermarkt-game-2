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
    [SerializeField] private int minLength = 100;

    [SerializeField] private GameObject enemyPath;
    [SerializeField] private Transform pathTransform ;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private EnemyPathRenderer enemyPathRenderer;
    [SerializeField] private bool hasFoundStartingPoint = false;

    [SerializeField] private int[] previousWay = new int[2];

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
        GameObject PathHolder = new GameObject("PathHolder");

        int zWay = 0;
        int xWay = 1;
        int rot = 0;

        int misses = 0;
        Vector3 lastValidPos = new Vector3();

        int length = 0;

        List<Vector3> enemyPathTransforms = new List<Vector3>();

        RaycastHit hit;
        while (length < minLength)
        {

            pathTransform.position += new Vector3(xWay, 0f, zWay);

            if(Physics.Raycast(pathTransform.position, Vector3.down, out hit,  999f, tileLayer))
            {
                if(UnityEngine.Random.Range(0, 10) > 6 && !hasFoundStartingPoint)
                {
                    hasFoundStartingPoint = true;
                }
                else if(!hasFoundStartingPoint)
                {
                    continue;
                }

                Tile thisTile = hit.collider.gameObject.GetComponent<Tile>();

                if (thisTile.wasCheckedByPathGenerator)
                {
                    xWay = -xWay;
                }

                Vector3 pos;
                pos = hit.collider.gameObject.transform.position;

                lastValidPos = pos;

                GameObject thisPath = Instantiate(enemyPath, pos, Quaternion.Euler(0f, rot, 0f));
                thisPath.transform.SetParent(PathHolder.transform);

                enemyPathTransforms.Add(pos + Vector3.up * (thisTile.info.yOffset + 1));

                thisTile.wasCheckedByPathGenerator = true;

                if (hasFoundStartingPoint && UnityEngine.Random.Range(0,10) > 3)
                {
                    new_way:

                    int way = UnityEngine.Random.Range(0, 100);

                    if(way < 20 || zWay != 0)
                    {
                        zWay = 0;
                        xWay = 1;
                        rot = 180;
                        
                    }else if(way < 40)
                    {
                        zWay = 0;
                        xWay = -1;
                        rot = 0;
                    }
                    else
                    {
                        zWay = 1;
                        xWay = 0;
                        rot = -90;
                    }

                    if(zWay == previousWay[0] && xWay == previousWay[1])
                    {
                        Debug.Log("same direction found!");

                        yield return new WaitForFixedUpdate();
                        goto new_way;
                    }

                    previousWay[0] = zWay;
                    previousWay[1] = xWay;
                }

                misses = 0;

                length++;
                Debug.Log("current path length: " + length);
            }
            else
            {
                misses++;

                if (misses >= 100)
                {
                    pathTransform.position = lastValidPos + Vector3.forward;
                    zWay = 1;
                    xWay = -xWay;
                    misses = 0;
                }
            }

            // Debug.Log("path generator misses: " + misses);
            yield return new WaitForFixedUpdate();
        }

        enemyPathRenderer.RendererSetup(enemyPathTransforms);

        //only for testing!!!!!!!!
        EnemySpawner.Instance.SpawnEnemies(enemyPathTransforms);
    }
}
