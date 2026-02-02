using UnityEngine;

public class PlayerTileGetter : MonoBehaviour
{
    public GameObject TileGOBlookingAt;
    [SerializeField] private GameObject cameraGOB;
    [SerializeField] private LayerMask tileLayer;
    private RaycastHit hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TileGOBlookingAt = 
        if(Physics.Raycast(transform.position, cameraGOB.transform.rotation.ToEulerAngles(), out hit, 1000f, tileLayer))
        {
            TileGOBlookingAt = hit.collider.gameObject;
            TileGOBlookingAt.GetComponent<Tile>().Select();
        }
        else
        {
            TileGOBlookingAt = null;
        }
    }
}
