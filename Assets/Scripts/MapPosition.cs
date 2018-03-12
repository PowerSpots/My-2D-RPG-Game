using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPosition : MonoBehaviour {

    // Use this for initialization
    void Awake () {
        //寻找玩家在场景中的最后一个位置，如果存在，移动到那
        var lastPosition = GameState.GetLastScenePosition(SceneManager.GetActiveScene().name);
        if (lastPosition != Vector3.zero)
        {
            transform.position = lastPosition;
        }
    }

    void OnDestroy()
    {
        // 关闭场景时保存最后一个位置
        if (GameState.saveLastPosition)
        {
            GameState.SetLastScenePosition(SceneManager.GetActiveScene().name, transform.position);
        }
    }
}
