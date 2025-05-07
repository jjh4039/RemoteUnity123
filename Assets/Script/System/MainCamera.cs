using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    public int cameraLevel;
    public float smoothTimePosition = 0.2f;
    private Vector3 velocityPosition = Vector3.zero;
    public Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        transform.position = GameManager.Instance.player.transform.position;
    }

    void FixedUpdate()
    {
        switch (cameraLevel)
        {
            case 0:
                transform.position = new Vector3(GameManager.Instance.player.transform.position.x + 3f, 0.265f, -10);
                break;
            case 1:
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(GameManager.Instance.player.transform.position.x + 6.2f, 3.3f, -10f), ref velocityPosition, smoothTimePosition);
                break;
            case 2:
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(118.45f, 3.0f, -10f), ref velocityPosition, smoothTimePosition);
                break;
            case 3:
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(120.4f, 2f, -10f), ref velocityPosition, 0.15f);
                break;
        }
        
        if (transform.position.x < 1.53)
        {
            transform.position = new Vector3(1.53f, 0.265f, -10);
        }
    }

    public IEnumerator SizeFiveZoom()
    {
        while (mainCamera.orthographicSize <= 5)
        {
            yield return new WaitForSeconds(0.01f);
            mainCamera.orthographicSize += 0.06f;
        }
    }

    public IEnumerator SizeFourZoom()
    {
        while (mainCamera.orthographicSize >= 4)
        {
            yield return new WaitForSeconds(0.01f);
            mainCamera.orthographicSize -= 0.06f;
        }
        mainCamera.orthographicSize = 4;
    }

    public IEnumerator SizeThreeZoom()
    {
        while (mainCamera.orthographicSize >= 3)
        {
            yield return new WaitForSeconds(0.01f);
            mainCamera.orthographicSize -= 0.05f;
        }
        mainCamera.orthographicSize = 3;
    }
}
