using UnityEngine;

public class OffScreenTeleport : MonoBehaviour
{

    void Update()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 newPosition = transform.position;

        if (viewportPos.x > 1.01 || viewportPos.x < -0.01)
        {
            newPosition.x = (int)-newPosition.x;
        }

        if (viewportPos.y > 1.01 || viewportPos.y < -0.01)
        {
            newPosition.y = (int)-newPosition.y;
        }

        transform.position = newPosition;
    }
}
