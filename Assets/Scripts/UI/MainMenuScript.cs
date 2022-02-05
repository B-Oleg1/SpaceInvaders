using System.Collections;
using System.Collections.Generic;
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
        // �������� �������, ���� �� ����������
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
            _volumeButton.transform.GetChild(0).GetComponent<Text>().text = "���";

            _turnSound = true;
        }
        else if (AudioListener.volume == 0)
        {
            _volumeButton.GetComponent<Image>().color = Color.red;
            _volumeButton.transform.GetChild(0).GetComponent<Text>().text = "����";

            _turnSound = false;
        }
    }

    // ���/���� �����
    public void OnChangeVolume()
    {
        if (_turnSound)
        {
            AudioListener.volume = 0;

            // ����� ����� ������
            _volumeButton.GetComponent<Image>().color = Color.red;
            _volumeButton.transform.GetChild(0).GetComponent<Text>().text = "����";

            _turnSound = false;
        }
        else
        {
            AudioListener.volume = 1;

            _volumeButton.GetComponent<Image>().color = Color.green;
            _volumeButton.transform.GetChild(0).GetComponent<Text>().text = "���";

            _turnSound = true;
        }
    }

    // ����� �� ����
    public void QuitGame()
    {
        Application.Quit();
    }
}