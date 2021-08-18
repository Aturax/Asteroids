using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _maxSpeed = 20.0f;
    [SerializeField] private float _acceleration = 5.0f;
    [SerializeField] private float _rotateSpeed = 200.0f;
    [SerializeField] private float _fireCooldown = 0.2f;
    private float _lastShot = 0.0f;
    
    [SerializeField] private GameObject _laser = null;
    [SerializeField] private Transform _laserSpawn = null;

    private bool _isAlive = true;

    private Rigidbody2D _rigidbody = null;
    private Renderer _renderer = null;
    private Collider2D _collider = null;

    public event Action Death = () => { };

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider2D>();

        HidePlayer();
    }

    void Start()
    {
        _lastShot = Time.time;
    }

    void Update()
    {
        if (_isAlive)
        {
            Rotate();
            Fire();
        }        
    }

    void FixedUpdate()
    {
        if (_isAlive)
        {
            _rigidbody.AddForce(transform.up * _acceleration * PlayerInput.ThrottleUp());
        }

        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }
    }

    void Rotate()
    {
        transform.Rotate(0, 0, PlayerInput.Horizontal() * -_rotateSpeed * Time.deltaTime);
    }

    void Fire()
    {
        if ((Time.time - _lastShot > _fireCooldown) && PlayerInput.Fire())
        {
            GameObject shot = Instantiate(_laser, _laserSpawn.transform.position, transform.rotation);
            _lastShot = Time.time;
            Destroy(shot, 0.5f); // TODO ObjectPooling for Lasers
        }        
    }

    public void ActivePlayer()
    {
        StartCoroutine("SetPlayer");
    }

    IEnumerator SetPlayer()
    {
        yield return new WaitForSeconds(2);
        _isAlive = true;
        _renderer.enabled = true;
        yield return new WaitForSeconds(1);
        _collider.enabled = true;
    }

    void HidePlayer()
    {
        _isAlive = false;
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    void PlayerDies()
    {
        HidePlayer();
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            PlayerDies();
            Death();
        }
    }
}
