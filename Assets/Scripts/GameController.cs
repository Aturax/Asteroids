public class GameController
{

    private PlayerController _player = null;
    private IntVariable _score = null;
    private IntVariable _lives = null;
    private AsteroidManager _asteroidManager = null;

    public GameController(PlayerController player, IntVariable score, IntVariable lives, AsteroidManager asteroidManager)
    {
        _player = player;
        _score = score;
        _lives = lives;

        _score.Value = 0;
        _lives.Value = 3;

        _asteroidManager = asteroidManager;

        _player.Death += PlayerDeath;
        AsteroidController.Collide += AddScore;
    }

    public void StartGame()
    {
        _asteroidManager.SpawnBigAsteroids();
        _player.ActivePlayer();
    }

    void PlayerDeath()
    {
        _lives.Value--;

        if (_lives.Value >= 0)
        {
            _player.ActivePlayer();
        }
    }

    void AddScore(AsteroidController asteroid)
    {
        _score.Value += asteroid.Size * 10;
    }

    public void Dispose()
    {
        _player.Death -= PlayerDeath;
        AsteroidController.Collide -= AddScore;
    }
}
