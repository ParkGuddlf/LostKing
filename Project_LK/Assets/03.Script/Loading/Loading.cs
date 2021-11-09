using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] Image progressBar;
    static string nextScene;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    void Start()
    {
        StartCoroutine(LoadingSceneProgress());
    }


    IEnumerator LoadingSceneProgress()
    {
        AsyncOperation aop = SceneManager.LoadSceneAsync(nextScene);
        aop.allowSceneActivation = false;

        float timer = 0f;
        while (!aop.isDone)
        {
            yield return null;

            if (aop.progress < 0.9f)
            {
                progressBar.fillAmount = aop.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    aop.allowSceneActivation = true;
                    yield break;
                }
            }

        }
    }
}
