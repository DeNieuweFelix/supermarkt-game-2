using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraControls : MonoBehaviour
{
    [SerializeField] private float zoomStep;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoom = 10f;
    [SerializeField] private float maxZoom = 30f;
    [SerializeField] private Camera camera;
    [SerializeField] private float targetZoom;


     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<Camera>();
        targetZoom = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(camera.orthographicSize != MathF.Round(targetZoom, 2))
        {
            camera.orthographicSize = Mathf.Lerp(
                camera.orthographicSize,
                targetZoom,
                zoomSpeed * Time.deltaTime
            );
        }
    }

    public void ChangeZoom(InputAction.CallbackContext context)
    {
        Vector2 scrollValue = context.ReadValue<Vector2>();

        Debug.Log(scrollValue);

        targetZoom = (
            Math.Clamp(
                MathF.Round(
                    targetZoom + (-Mathf.Sign(scrollValue.y) * zoomStep),
                    2
                ),
                minZoom,
                maxZoom + zoomStep
            )
        );
    }


}
