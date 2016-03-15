using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{

    public static AsyncOperation async;

    void Start()
    {
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
    }
}