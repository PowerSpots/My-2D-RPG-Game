using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SaveSceneOnPlay
{

    // 静态构造类，启动时初始化
    static SaveSceneOnPlay()
    {
        //使用事件连接委托
        EditorApplication.playmodeStateChanged += SaveSceneIfPlaying;
    }

    // 点击播放按钮时保存场景和和资源
    static void SaveSceneIfPlaying()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
        {

            Debug.Log("Automaticly saving scene (" + EditorApplication.currentScene + ") before entering play mode ");

            AssetDatabase.SaveAssets();
            EditorApplication.SaveScene();
        }
    }

    // 清理资源
    static void Dispose()
    {
        EditorApplication.playmodeStateChanged -= SaveSceneIfPlaying;
    }
}