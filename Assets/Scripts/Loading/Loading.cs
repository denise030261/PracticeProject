using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Text textComponent; // 로딩 텍스트
    public List<Image> imageComponents; // 로딩 그림
    public float fadeInSpeed = 0.1f; // 페이드 인 속도
    public float fadeOutSpeed = 0.1f; // 페이드 아웃 속도
    public int repeatCount = 1; // 반복 횟수

    private string originalText;
    private List<string> characters;
    private int Day;

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day", 0);
        PlayerPrefs.SetInt("Day", Day);

        float BGMVolume = PlayerPrefs.GetFloat("BGM", 0.6f); // 맨 처음에 BGM은 0.6으로 맞춘다.
        PlayerPrefs.SetFloat("BGM", BGMVolume);
        float SFXVolume = PlayerPrefs.GetFloat("SFX", 0.6f); // 맨 처음에 BGM은 0.6으로 맞춘다.
        PlayerPrefs.SetFloat("SFX", SFXVolume);
    }

    private void Start()
    {
        originalText = textComponent.text; // 로딩 텍스트 저장
        characters = SplitTextIntoCharacters(originalText); // 텍스트 나누기

        if (imageComponents.Count < characters.Count)
        {
            int countToAdd = characters.Count - imageComponents.Count;
            for (int i = 0; i < countToAdd; i++)
            {
                Image newImage = Instantiate(imageComponents[0]);
                newImage.transform.SetParent(transform);
                newImage.gameObject.SetActive(false);
                imageComponents.Add(newImage);
            }
        }

        StartCoroutine(ShowLoading());

        Invoke("LoadNextScene", 13f);
    }

    private IEnumerator ShowLoading()
    {
        for (int r = 0; r < repeatCount; r++)
        {
            ResetLoading();

            foreach (string character in characters)
            {
                textComponent.text += character;
                yield return new WaitForSeconds(fadeInSpeed);
            } // 텍스트 나타나는 시간

            yield return new WaitForSeconds(1f);

            foreach (Image image in imageComponents)
            {
                image.gameObject.SetActive(true);
                image.canvasRenderer.SetAlpha(0f);
                image.CrossFadeAlpha(1f, fadeInSpeed, true);
                yield return new WaitForSeconds(fadeInSpeed);
            } // 이미지 나타나는 시간

            float maxFadeOutTime = characters.Count * fadeOutSpeed;
            float currentFadeOutTime = 0f;

            while (currentFadeOutTime < maxFadeOutTime)
            {
                float deltaTime = Time.deltaTime;
                currentFadeOutTime += deltaTime;

                foreach (Image image in imageComponents)
                {
                    float fadeAmount = Mathf.Lerp(1f, 0f, currentFadeOutTime / maxFadeOutTime);
                    image.canvasRenderer.SetAlpha(fadeAmount);
                }

                textComponent.canvasRenderer.SetAlpha(Mathf.Lerp(1f, 0f, currentFadeOutTime / maxFadeOutTime));

                yield return null;
            }

            foreach (Image image in imageComponents)
            {
                image.gameObject.SetActive(false);
            }

            textComponent.text = "";
        }
    }

    private List<string> SplitTextIntoCharacters(string text)
    {
        List<string> characters = new List<string>();
        foreach (char c in text)
        {
            characters.Add(c.ToString());
        }
        return characters;
    }

    private void ResetLoading()
    {
        foreach (Image image in imageComponents)
        {
            image.canvasRenderer.SetAlpha(0f);
        }

        textComponent.canvasRenderer.SetAlpha(1f);
        textComponent.text = "";
    }

    private void LoadNextScene()
    {
        if (Day == 0)
            SceneManager.LoadScene("Tutorial");
        else
            SceneManager.LoadScene("Heaven");
    } // 다음 씬으로 넘어가기
}
