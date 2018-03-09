using UnityEngine;
using System.Collections;
public class NavigationPrompt : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        // 进入边界触发事件
        if (col.gameObject.CompareTag("Borders"))
        {
            Debug.Log("leave town");
        }
    }
}