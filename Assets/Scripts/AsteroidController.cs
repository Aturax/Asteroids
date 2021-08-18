using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public int Size
    {
        get; private set;
    } = 1;
    float speed = 1f;

    private Rigidbody2D _rigidbody = null;

    public static event Action<AsteroidController> Collide = (AsteroidController asteroid) => { };

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        RandomSpeeds();
    }

    public void SpawnInBorder()
    {
        ChangeSize(1);
        RandomSpeeds();

        transform.position = RandomPosition();
    }

    public void SpawnInPosition(int scale, Vector3 position)
    {
        ChangeSize(scale);
        RandomSpeeds();

        transform.position = RandomPosition(position);
    }

    // RANDOM POSITION NEAR ASTEROID EXPLOSION POSITION
    Vector3 RandomPosition(Vector3 position)
    {
        Vector3 rnPos = new Vector3(position.x + UnityEngine.Random.Range(-0.5f, 0.5f),
                                    position.y + UnityEngine.Random.Range(-0.5f, 0.5f),
                                    0);

        return rnPos;
    }

    // RANDOM POSITION IN SCREEN BORDERS
    Vector3 RandomPosition()
    {
        float randx = UnityEngine.Random.Range(0.01f, 0.99f);
        float randy = 0f;

        if (randx > 0.2)
        {
            randy = UnityEngine.Random.Range(0, 2);
        }
        else
        {
            randy = UnityEngine.Random.Range(0.01f, 0.99f);
        }

        Vector2 rnPos = new Vector2(randx,randy);

        rnPos = Camera.main.ViewportToWorldPoint(rnPos);
       
        return new Vector3(rnPos.x, rnPos.y, 0);
    }

    void ChangeSize(int scale)
    {
        Size = scale;
        transform.localScale = new Vector3((float)1/scale, (float)1/scale, 1);
    }

    void RandomSpeeds()
    {
        speed = UnityEngine.Random.Range(1f, 2f) * Size/2f;

        _rigidbody.velocity = new Vector2(UnityEngine.Random.Range(-1f, 1f),
                                  UnityEngine.Random.Range(-1f, 1f)).normalized * speed;
    }

    public void StopAsteroid()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject); // TODO ObjectPooling for Lasers
            Collide(this);
        }        
    }
}
