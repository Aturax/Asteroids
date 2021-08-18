using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{

    [SerializeField] private PlayerController _player = null;
    [SerializeField] private IntVariable _score = null;
    [SerializeField] private IntVariable _lives = null;
    [SerializeField] private AsteroidController _asteroidPrefab = null;

    [SerializeField] private Transform _asteroidsParent = null;
    private AsteroidManager _asteroidManager = null;

    private GameController _gameController = null;

    void Awake()
    {
        _asteroidManager = new AsteroidManager(_asteroidsParent, _asteroidPrefab);
        _gameController = new GameController(_player, _score, _lives, _asteroidManager);
    }

    void Start()
    {
        _gameController.StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(_lives.Value < 0 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }
    }

    void OnDestroy()
    {
        _asteroidManager.Dispose();
        _gameController.Dispose();
    }


}
