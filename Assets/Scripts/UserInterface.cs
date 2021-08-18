using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{

    [SerializeField] private GameObject _pressToStart = null;
    [SerializeField] private GameObject _gameOver = null;

    [SerializeField] private TMP_Text _scoreText = null;
    [SerializeField] private IntVariable _score = null;

    [SerializeField] private TMP_Text _livesText = null;
    [SerializeField] private IntVariable _lives = null;

    void OnEnable()
    {
        _score.OnValueChanged += ShowScore;
        _lives.OnValueChanged += ShowLives;
    }

    void OnDisable()
    {
        _score.OnValueChanged -= ShowScore;
        _lives.OnValueChanged -= ShowLives;
    }

    void ShowScore()
    {
        _scoreText.text = _score.Value.ToString();
    }

    void ShowLives()
    {
        _livesText.text = _lives.Value.ToString();

        if (_lives.Value == -1)
        {
            _gameOver.SetActive(true);
            _pressToStart.SetActive(true);
            _livesText.text = "0";
        }
    }
}
