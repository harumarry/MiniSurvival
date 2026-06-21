using UnityEngine;

public class MapBounds : MonoBehaviour
{
    [SerializeField] Vector2 mapSize = new Vector2(40f, 25f);

    public Vector2 Min => (Vector2)transform.position - mapSize * 0.5f;
    public Vector2 Max => (Vector2)transform.position + mapSize * 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, mapSize);
    }
}
