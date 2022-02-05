using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControllGameScript : MonoBehaviour
{
    public static ControllGameScript controllGameScript;

    [SerializeField]
    private AudioSource _supportMusic;

    [SerializeField]
    private ParticleSystem _killParticleSystem;

    [SerializeField]
    private GameObject _endPanel;

    [SerializeField]
    private Text _endScoreText;


    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _healthText;

    [SerializeField]
    private Image _playerImage;

    private int _score = 0;
    private int _health = 3;

    private bool _canHitPlayer = true;

    private void Start()
    {
        Time.timeScale = 1;

        controllGameScript = this;
    }

    // If you killed an enemy
    public void KillEnemy(Transform hitPos)
    {
        _supportMusic.Play();

        _killParticleSystem.transform.position = hitPos.position;
        _killParticleSystem.Play();

        _score++;
        _scoreText.text = $"явер {_score.ToString()}";
    }

    // If the ship has received damage
    public void HitPlayer()
    {
        // If 3 seconds have passed since the previous hit
        if (_canHitPlayer)
        {
            _canHitPlayer = false;

            _health--;
            _healthText.text = _health.ToString();

            StartCoroutine(FlashingOnHit());

            if (_health <= 0)
            {
                EndGame();
            }
        }
    }

    // Flashing of the ship
    private IEnumerator FlashingOnHit()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                while (_playerImage.color.a > 0)
                {
                    var alpha = _playerImage.color;
                    alpha.a -= 0.1f;
                    _playerImage.color = alpha;
                }
            }
            else
            {
                while (_playerImage.color.a < 1)
                {
                    var alpha = _playerImage.color;
                    alpha.a += 0.1f;
                    _playerImage.color = alpha;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }

        _canHitPlayer = true;
    }

    // When the player was killed
    public void EndGame()
    {
        // Freeze the scene
        Time.timeScale = 0f;

        _endPanel.SetActive(true);
        _endScoreText.text = _score.ToString();

        // Saving a record
        if (PlayerPrefs.GetInt("Record") < _score)
        {
            PlayerPrefs.SetInt("Record", _score);
        }
    }
}