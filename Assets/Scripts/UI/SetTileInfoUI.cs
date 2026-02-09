using TMPro;
using UnityEngine;

public class SetTileInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TypeText;

    public void SetInfo(Tile tile)
    {
        TypeText.text = "Tile: " + tile.info.x + ", " + tile.info.y;
    }
}
