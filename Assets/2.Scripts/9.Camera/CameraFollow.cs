using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] MapBounds mapBounds;
    [SerializeField] float smoothTime = 0.15f;

    Camera cam;
    Vector3 velocity;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null || mapBounds == null) return;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        float cameraHalfHeight = cam.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * cam.aspect;

        float minX = mapBounds.Min.x + cameraHalfWidth;
        float maxX = mapBounds.Max.x - cameraHalfWidth;

        float minY = mapBounds.Min.y + cameraHalfHeight;
        float maxY = mapBounds.Max.y - cameraHalfHeight;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
