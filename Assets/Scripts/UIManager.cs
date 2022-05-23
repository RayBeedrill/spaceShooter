using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesDisplay;
    [SerializeField]
    private TMP_Text _gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        WatchScores();
        if (_player.GetLifes() <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void WatchScores()
    {
        _scoreText.text = "Score: " + _player.GetPlayerScore();
    }

    public void WatchLives()
    {
        int lifes = _player.GetLifes();
        _livesDisplay.sprite = _livesSprites[lifes];
        if(lifes <= 0)
        {
            _gameOverText.gameObject.SetActive(true);
        }
    }

    public void OnShowGameoverText()
    {
        StartCoroutine(GameOverFlicker());
    }

    private IEnumerator GameOverFlicker()
    {
        while(true)
        {
            _gameOverText.text = "Game Over" +
                "\nPress R to restart";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
