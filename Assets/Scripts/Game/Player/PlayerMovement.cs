using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 dir;
    public float speed = 10f;
    [SerializeField] private float TimeMoving = 0f;
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 dir_0 = context.ReadValue<Vector2>();
        
        dir = new Vector3(dir_0.x, 0f, dir_0.y);
    }

    void Update()
    {
        if(dir != Vector3.zero)
        {
            
            TimeMoving +=  1f * Time.deltaTime;

            if(TimeMoving > 2f)
            {
                transform.Translate(dir * Time.deltaTime * (10f + (Mathf.Clamp(TimeMoving, 0f, 10f) - 2f) * 3f));
            }
            else
            {
                transform.Translate(dir * Time.deltaTime * 10f);
            }
        }
        else
        {
            TimeMoving = 0f;
        }
    }
}
