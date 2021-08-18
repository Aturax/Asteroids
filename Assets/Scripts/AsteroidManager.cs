using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager
{
    private AsteroidController _asteroidPrefab = null;
    private List<AsteroidController> _asteroidList = new List<AsteroidController>();
    private Transform _parent = null;

    public AsteroidManager(Transform parentTransform, AsteroidController prefab)
    {
        _parent = parentTransform;
        _asteroidPrefab = prefab;

        AsteroidController.Collide += DivideAsteroid;

        PreloadAsteroids();
    }

    void PreloadAsteroids()
    {
        // 16 ARE THE MAX ASTEROIDS THAT CAN BE ON SCREEN (4 BIGS THAT CRUSHING FORMS 8 MEDIUM THAT FORMS 16 SMALLS)
        for (int i = 0; i < 16; i++)
        {
            AsteroidController asteroid = MonoBehaviour.Instantiate(_asteroidPrefab);
            asteroid.transform.SetParent(_parent);
            _asteroidList.Add(asteroid);
            asteroid.gameObject.SetActive(false);
        }
    }

    public void SpawnBigAsteroids()
    {
        for (int i = 0; i < 4; i++)
        {
            _asteroidList[0].gameObject.SetActive(true);
            _asteroidList[0].SpawnInBorder();
            _asteroidList.RemoveAt(0);
        }
    }

    void DivideAsteroid(AsteroidController asteroid)
    {
        if (asteroid.Size >= 1 && asteroid.Size!=3)
        {
            _asteroidList[0].gameObject.SetActive(true);
            _asteroidList[0].SpawnInPosition(asteroid.Size + 1, asteroid.transform.position);
            _asteroidList.RemoveAt(0);
            asteroid.SpawnInPosition(asteroid.Size + 1, asteroid.transform.position);
        }
        else
        {
            RecycleAsteroid(asteroid);
        }

        //TODO
        // ADD PARTICLE & SOUND EFFECTS FOR EXPLOSIONS

        if (_asteroidList.Count == 16)
        {
            SpawnBigAsteroids();
        }
    }

    void RecycleAsteroid(AsteroidController asteroid)
    {
        asteroid.gameObject.SetActive(false);
        _asteroidList.Add(asteroid);
    }

    public void Dispose()
    {
        AsteroidController.Collide -= DivideAsteroid;
    }


}
