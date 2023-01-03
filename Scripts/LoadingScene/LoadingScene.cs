using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image progressBar;
    [SerializeField] Image FadeOut;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator FaidOut()
    {
        Color fadeColor;
        float timer = 0.0f;
        fadeColor = FadeOut.color;
        while (fadeColor.a>0)
        {
            timer += Time.timeScale / 4;
            fadeColor.a -= 0.000007f * timer;
            FadeOut.color = fadeColor;
            yield return null;
        }
    }

    IEnumerator LoadScene()
    {
        yield return StartCoroutine(FaidOut());
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);

        float timer = 0f;
        while (!asyncOperation.isDone)
        {
            yield return null;
            if (asyncOperation.progress < 0.9f)
            {
                progressBar.fillAmount = asyncOperation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1.0f)
                {
                    asyncOperation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
