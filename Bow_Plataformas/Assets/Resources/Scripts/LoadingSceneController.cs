using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(loadSceneAsync(PlayerPrefs.GetString("EscenaCargar")));
    }

    IEnumerator loadSceneAsync(string escena)
    {
        yield return new WaitForSeconds(1f);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(escena);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
