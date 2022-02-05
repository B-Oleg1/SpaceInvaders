using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private Text _recordText;

    [SerializeField]
    private Button _volumeButton;

    private bool _turnSound = true;

    private void Start()
    {
        // Loading a record, if it exists
        if (PlayerPrefs.HasKey("Record"))
        {
            _recordText.text = PlayerPrefs.GetInt("Record").ToString();
        }
        else
        {
            _recordText.text = "0";
        }


        if (AudioListener.volume == 1)
        {
            _volumeButton.GetComponent<Image>().color = Color.green;
            _volumeButton.transform.GetChild(0).GetComponent<Text>().text = "Вкл";

            _turnSound = true;
        }
        else if (AudioListener.volume == 0)
        {
            _volumeButton.GetComponent<Image>().color = Color.red;
            _volumeButton.transform.GetChild(0).GetComponent<Text>().text = "Выкл";

            _turnSound = false;
        }
    }

    // On/Off sound
    public void OnChangeVolume()
    {
        if (_turnSound)
        {
            AudioListener.volume = 0;

            // Button color change
            _volumeButton.GetComponent<Image>().color = Color.red;
            _volumeButton.transform.GetChild(0).GetComponent<Text>().text = "Выкл";

            _turnSound = false;
        }
        else
        {
            AudioListener.volume = 1;

            _volumeButton.GetComponent<Image>().color = Color.green;
            _volumeButton.transform.GetChild(0).GetComponent<Text>().text = "Вкл";

            _turnSound = true;
        }
    }

    // Exiting the game
    public void QuitGame()
    {
        Application.Quit();
    }
}