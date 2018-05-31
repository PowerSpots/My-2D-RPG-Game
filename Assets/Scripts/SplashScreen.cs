using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// 在指定的时间之后按名称加载一个场景
public class SplashScreen : MonoBehaviour
{
    public string sceneToLoadName;
    public int timeToLoad;

    void Start()
    {
        Invoke("NextScene", timeToLoad);
    }

    void NextScene()
    {
        SceneManager.LoadScene(sceneToLoadName);
    }
}
