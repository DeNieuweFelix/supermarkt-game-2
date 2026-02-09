using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] private Image image;

    public void Set(Building b)
    {
        image.sprite = b.icon;
    }
}
