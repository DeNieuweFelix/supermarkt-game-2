using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetTileInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TypeText;
    [SerializeField] private RawImage image;

    public void SetInfo(Tile tile)
    {
        TypeText.text = "Tile: " + tile.info.x + ", " + tile.info.y;
        image.texture = tile.info.thisType.tileMaterial.GetTexture("_BaseMap");
    }
}
