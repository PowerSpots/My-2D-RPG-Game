using System.Collections.Generic;
using UnityEngine;

// 保存游戏状态的静态类
public static class GameState
{
    // 立即重新进入战斗场景的标记
    public static bool justExitedBattle;
    // 是否会保存上一个游戏位置
    public static bool saveLastPosition = true;

    // 保存当前玩家的数据
    public static Player CurrentPlayer = ScriptableObject.CreateInstance<Player>();
    // 记录玩家去过的场景以及他们在该场景中的最后位置的字典
    public static Dictionary<string, Vector3> LastScenePositions = new Dictionary<string, Vector3>();
    // 使用字典类的辅助函数
    public static Vector3 GetLastScenePosition(string sceneName)
    {
        // 当从字典中请求场景的最后位置时，检查字典中场景是否存在，然后返回位置
        if (GameState.LastScenePositions.ContainsKey(sceneName))
        {
            var lastPos = GameState.LastScenePositions[sceneName];
            return lastPos;
        }
        // 如果字典中还没有该值，返回默认值
        else
        {
            return Vector3.zero;
        }
    }

    public static void SetLastScenePosition(string sceneName, Vector3 position)
    {
        // 当您向字典中添加新值时，检查它是否已经存在，如果存在，则更新现有值
        if (GameState.LastScenePositions.ContainsKey(sceneName))
        {
            GameState.LastScenePositions[sceneName] = position;
        }
        // 如果该值在您尝试添加时不存在，则只会将其添加到字典中
        else
        {
            GameState.LastScenePositions.Add(sceneName, position);
        }
    }
}