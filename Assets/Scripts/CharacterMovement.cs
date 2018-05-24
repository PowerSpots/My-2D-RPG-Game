using UnityEngine;
using System.Collections;
/// <summary>
/// 角色的移动控制
/// </summary>
public class CharacterMovement : MonoBehaviour
{
    // 玩家的RigidBody2D组件实例
    private Rigidbody2D playerRigidBody2D;

    // 玩家角色的动画组件引用
    private Animator playerAnim;

    // 玩家角色的精灵渲染器组件引用
    private SpriteRenderer playerSpriteImage;

    // 跟踪从输入获得的运动变量来控制运动方向
    private float movePlayerHorizontal;
    private float movePlayerVertical;
    private Vector2 movement;

    // 玩家移动的速度
    public float speed = 4.0f;

    // 初始化所有组件
    void Awake()
    {
        // 明确所要求的组件类型
        playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        playerAnim = (Animator)GetComponent(typeof(Animator));
        playerSpriteImage = (SpriteRenderer)GetComponent(typeof(SpriteRenderer));
    }

    void Update ()
    {
        // 使用默认的水平键（左和右）和垂直键（上和下）来检查玩家是否正在控制游戏
        movePlayerHorizontal = Input.GetAxis("Horizontal");
        movePlayerVertical = Input.GetAxis("Vertical");
        // 施加有方向的力量来移动英雄
        movement = new Vector2(movePlayerHorizontal,movePlayerVertical);
        playerRigidBody2D.velocity = movement * speed;

        // 角色行走的动画控制
        if (movePlayerVertical != 0)
        {
            playerAnim.SetBool("xMove", false);
            playerSpriteImage.flipX = false;
            // 向上走
            if (movePlayerVertical > 0)
            {
                playerAnim.SetInteger("yMove", 1);
            }
            // 向下走
            else if (movePlayerVertical < 0)
            {
                playerAnim.SetInteger("yMove", -1);
            }
        }
        else
        {
            // 水平行走
            playerAnim.SetInteger("yMove", 0);
            // 向右水平行走
            if (movePlayerHorizontal > 0)
            {
                playerAnim.SetBool("xMove", true);
                playerSpriteImage.flipX = false;
            }
            // 向左水平行走
            else if (movePlayerHorizontal < 0)
            {
                playerAnim.SetBool("xMove", true);
                playerSpriteImage.flipX = true;
            }
            else
            {
                playerAnim.SetBool("xMove", false);
            }
        }
        // 角色停止的动画控制
        if (movePlayerVertical == 0 && movePlayerHorizontal == 0)
        {
            playerAnim.SetBool("moving", false);
        }
        else
        {
            playerAnim.SetBool("moving", true);
        }
    }
}