using System;
using System.Collections;
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

    [SerializeField] private AudioClip zoomTickSound;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject cursorImg;


     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<Camera>();
        targetZoom = camera.orthographicSize;

        StartCoroutine(CursorLoop());
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

        audioSource.PlayOneShot(zoomTickSound);

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

    //stuff for the cursor
    private void SetCursor()
    {
        Enemy e = EnemySpawner.Instance.GetFrontEnemy();
        if (e == null) return;
    
        Vector3 dir = e.gameObject.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        cursorImg.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private IEnumerator CursorLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            SetCursor();
        }
    }

}
