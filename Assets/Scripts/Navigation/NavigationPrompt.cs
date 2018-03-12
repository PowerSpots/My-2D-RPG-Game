using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NavigationPrompt : MonoBehaviour
{
    public Vector3 startingPosition;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (NavigationManager.CanNavigate(this.tag))
        {
            Debug.Log("attempting to exit via " + tag);
            NavigationManager.NavigateTo(this.tag);
        }
        // 如果我们不保存最后的位置，加载一个指定的Vector3值，而不是获取玩家的最后位置
        GameState.saveLastPosition = false;
        GameState.SetLastScenePosition(SceneManager.GetActiveScene().name, startingPosition);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (NavigationManager.CanNavigate(this.tag))
        {
            Debug.Log("attempting to exit via " + tag);
            NavigationManager.NavigateTo(this.tag);
        }
        GameState.saveLastPosition = false;
        GameState.SetLastScenePosition(SceneManager.GetActiveScene().name, startingPosition);
    } 
}