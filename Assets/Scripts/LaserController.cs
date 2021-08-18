using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private float _speed = 0.0f;

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

}
