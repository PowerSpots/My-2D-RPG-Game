using UnityEngine;
using System.Collections;
public class CharacterMovement : MonoBehaviour
{
    // 玩家角色的刚体2D组件引用
    private Rigidbody2D playerRigidBody2D;

    // 用于记录玩家角色移动的水平、垂直移动变量和速度变量
    private float movePlayerHorizontal;
    private float movePlayerVertical;
    private Vector2 movement;

    // 玩家移动的速度修正
    public float speed = 4.0f;

    // 初始化组件引用
    void Awake()
    {
        // 手动指明要获得的组件类型，加速组件的获得
        playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
    }

    void Update ()
    {
        // 将输入所得的水平、垂直分量赋值给玩家的速度向量
        movePlayerHorizontal = Input.GetAxis("Horizontal");
        movePlayerVertical = Input.GetAxis("Vertical");
        movement = new Vector2(movePlayerHorizontal,movePlayerVertical);
        playerRigidBody2D.velocity=movement*speed;
    }
}