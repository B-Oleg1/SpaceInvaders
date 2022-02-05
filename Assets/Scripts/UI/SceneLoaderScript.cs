using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderScript : MonoBehaviour
{
    public static SceneLoaderScript sceneLoader;

    private void Start()
    {
        sceneLoader = this;

        gameObject.SetActive(false);
    }

    public void LoadNewScene(int sceneId)
    {
        StartCoroutine(LoadingNewScene(sceneId));
    }

    // �������� ������������ �����
    private IEnumerator LoadingNewScene(int sceneId)
    {
        gameObject.SetActive(true);

        var slider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();

        // � ������� ������ �������� ������, ��� ������ ������������ ��������� ��������
        var sliderWidth = slider.GetComponent<RectTransform>().rect.width;
        var pointer = transform.GetChild(1).GetChild(1).GetComponent<RectTransform>();

        // ���������� ������� ��������, ���� ����� ��������� ����������
        var load = SceneManager.LoadSceneAsync(sceneId);
        while (!load.isDone)
        {
            // ��������� ������� �������� ����� �����
            var progress = load.progress / .9f;
            slider.value = progress;

            // ������� ���������
            pointer.localPosition = new Vector2(progress * sliderWidth, 0);

            yield return null;
        }
    }
}