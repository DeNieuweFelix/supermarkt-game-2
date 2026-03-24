using TMPro;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(CreditEntry cre)
    {
        spriteRenderer.material.SetTexture("_Texture2D", cre.image);
        name.text = cre.name;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "killBarrier")
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
}
