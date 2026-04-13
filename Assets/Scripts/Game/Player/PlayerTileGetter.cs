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

    private bool isFocussedOnUpgradeableBuilding = false;
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

            byte status = CheckBuild();

            if(status == 0 || status == 1)
            {
                isFocussedOnUpgradeableBuilding = false;
                UpgradeShow.Instance.Hide();
            }

            tileInfoUI.SetInfo(TileLookingAt);
            oldHit = hit;
        }
        else
        {
            TileGOBlookingAt = null;
        }
    }

    //0 = not on building tile
    //1 = on building tile, but not  an offensive building tile
    //2 = on offensive building ti;e
    private byte CheckBuild()
    {
        if(tileSelected.hasBeenBuiltOn == true)
        {
            BuildingScript b = tileSelected.gameObject.GetComponentInChildren<BuildingScript>();

            if(b.thisBuilding.type == Building.Types.Offensive)
            {
                ShowUpgrades(b.thisBuilding);
                isFocussedOnUpgradeableBuilding = true;
                return 2;
            }

            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void ShowUpgrades(Building b)
    {
        OffensiveBuilding oB = tileSelected.gameObject.GetComponentInChildren<OffensiveBuilding>();
        if(oB == null)
        {
            Debug.LogError("no offensiveBuilding script found ):");
            return;
        }
        UpgradeInfo u = new UpgradeInfo();

        u.amountOfUpgrades = (byte)oB.upgrades.Count;
        u.buildingName = b.name;
        u.currentLevel = oB.upgradeLevel;
        u.upgradeCost = 9999;

        UpgradeShow.Instance.Show(u);
    }
    
}
