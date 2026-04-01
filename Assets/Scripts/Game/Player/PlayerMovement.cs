using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 dir;
    public float speed = 10f;
    [SerializeField] private float TimeMoving = 0f;
    [SerializeField] private float checkRadius = 10f;
    [SerializeField] private LayerMask TileLayer;

    [SerializeField] private AudioSource windAudioSource;

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 dir_0 = context.ReadValue<Vector2>();
        
        dir = new Vector3(dir_0.x, 0f, dir_0.y);
    }

    void Update()
    {
        if(dir != Vector3.zero)
        {
            if(!CheckMove()) return;
            
            TimeMoving +=  1f * Time.deltaTime;

            if(TimeMoving > 0.5f)
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

    void FixedUpdate()
    {
        if(dir != Vector3.zero)
        {
            float volume = MathF.Round(0.3f + Mathf.Clamp(Mathf.Clamp(TimeMoving, 0f, 10f) / 10, 0f, 0.7f), 2);
            
            if(windAudioSource.volume != volume)
            {
                windAudioSource.volume = volume;
            }
        }
        else
        {
            if(windAudioSource.volume != 0.3f)
            {
                windAudioSource.volume = 0.3f;
            }
        }
    }

    private bool CheckMove()
    {
        if(Physics.CheckSphere(transform.position, checkRadius, TileLayer))
        {
            return true;
        }
        else
        {
            // Debug.LogWarning("out of bounds!");

            //RETURN TO FALSE AFTER FIXING !!!
            return true;
        }
    }
}
