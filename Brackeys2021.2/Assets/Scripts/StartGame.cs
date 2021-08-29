using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] int index;

    [SerializeField] int sceneID = 0;
    [SerializeField] GameObject button;
    [SerializeField] GameObject button2;

    [Header("Animations")]
    [SerializeField] Animation fadeAnimation;
    [SerializeField] AnimationClip fadeInClip;
    [SerializeField] AnimationClip fadeOutClip;


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            OnStartClick(index);
        }
    }

    public void OnStartClick(int index)
    {
        //    button.SetActive(false);
        //   button2.SetActive(false);
        DontDestroyOnLoad(gameObject);

        //              fadeAnimation.clip = fadeInClip;

        sceneID = index;
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        //   fadeAnimation.clip = fadeInClip;
        //  fadeAnimation.Play();

        yield return new WaitForSeconds(.5f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //  fadeAnimation.PlayQueued(fadeOutClip.name);
        Destroy(this.gameObject, 2f);
    }

    public void LoadFazenda()
    {
        SceneManager.LoadScene(1);
    }
}
