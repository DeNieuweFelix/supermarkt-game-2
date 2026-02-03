using UnityEngine;

public class GreenMath : MonoBehaviour
{
    public static GreenMath I;

    void Awake()
    {
        if(I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

}
