using Unity.VisualScripting;
using UnityEngine;

public class BuildingPreviewManager : MonoBehaviour
{
    public static BuildingPreviewManager Instance;

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

    [SerializeField] private GameObject holder;
    [SerializeField] private float rotateSpeed = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        holder.transform.Rotate(new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
    }

    public void SetBuildingPreview(Building building)
    {
        foreach(Transform child in holder.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject b = Instantiate(building.model);
        b.transform.SetParent(holder.transform);

        //fix the position and scale (messed some stuff up)
        b.transform.localPosition = Vector3.zero;
        b.transform.localScale = Vector3.one * 0.2f;
    }
}
