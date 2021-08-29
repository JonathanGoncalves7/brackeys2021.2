using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField] private int _index;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Load(_index);
        }
    }

    public void Load(int index)
    {
        SceneManager.LoadScene(index);
    }

        public void LoadFazenda()
    {
        SceneManager.LoadScene(1);
    }
}
