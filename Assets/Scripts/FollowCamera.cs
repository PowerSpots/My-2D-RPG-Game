using UnityEngine;
using System.Collections;
public class FollowCamera : MonoBehaviour

{
    // 相机与角色的水平距离
    public float xOffset = 0f;

    // 相机与角色的垂直距离
    public float yOffset = 0f;

    // 角色位置的引用
    public Transform player;

    void Awake()
    {
        // 检查角色引用是否为空 
        if (player == null)
        {
            Debug.LogError("Player object not found");
        }
    }

    void LateUpdate()
    {
        // 相机只在水平方向上跟随玩家
        this.transform.position = new Vector3(player.transform.position.x + xOffset,
                                                this.transform.position.y + yOffset, -10);
    }
} 