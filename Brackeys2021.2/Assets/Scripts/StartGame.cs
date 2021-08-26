using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] int sceneID = 0;
    [SerializeField] GameObject button;

    [Header("Animations")]
    [SerializeField] Animation fadeAnimation;
    [SerializeField] AnimationClip fadeInClip;
    [SerializeField] AnimationClip fadeOutClip;

    public void OnStartClick()
    {
        button.SetActive(false);
        DontDestroyOnLoad(gameObject);

        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        fadeAnimation.clip = fadeInClip;
        fadeAnimation.Play();

        yield return new WaitForSeconds(.8f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        fadeAnimation.PlayQueued(fadeOutClip.name);
        Destroy(this.gameObject, 2f);

    }
}
