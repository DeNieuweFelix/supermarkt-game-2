using UnityEngine;

public class PlayerTileGetter : MonoBehaviour
{
    public static PlayerTileGetter Instance;

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
    
    public GameObject TileGOBlookingAt;
    [SerializeField] private GameObject cameraGOB;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private SetTileInfoUI tileInfoUI;
    private RaycastHit hit;
    private RaycastHit oldHit;

    public bool isTileSelected = false;
    public Tile tileSelected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cameraGOB.transform.position, cameraGOB.transform.forward);
        // TileGOBlookingAt = 
        if(Physics.Raycast(ray, out hit, 1000f, tileLayer))
        {
            isTileSelected = true;

            if((oldHit.collider == hit.collider) && oldHit.collider != null && hit.collider != null) return;
            else if(oldHit.collider != null)
            {
                oldHit.collider.gameObject.GetComponent<Tile>().Deselect();
            }

            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
            TileGOBlookingAt = hit.collider.gameObject;

            Tile TileLookingAt = TileGOBlookingAt.GetComponent<Tile>();
            TileLookingAt.Select();

            tileSelected = TileLookingAt;

            tileInfoUI.SetInfo(TileLookingAt);
            oldHit = hit;
        }
        else
        {
            TileGOBlookingAt = null;
        }
    }
}
