using UnityEngine;
using System.Collections;
public class Sorting : MonoBehaviour
{
    public Transform player;

    void Update ()
    {
        // 在主角上方
        if (transform.position.y >= player.transform.position.y)
        {
            Debug.Log("behind player");
            GetComponent<SpriteRenderer>().sortingOrder = (player.GetComponent<SpriteRenderer>().sortingOrder) - 1;
            GetComponents<BoxCollider2D>()[1].enabled = false;
            GetComponents<BoxCollider2D>()[2].enabled = true;
        }
        // 在主角下方
        if (transform.position.y < player.transform.position.y)
        {
            Debug.Log("in front of player");
            GetComponent<SpriteRenderer>().sortingOrder = (player.GetComponent<SpriteRenderer>().sortingOrder) + 1;
            GetComponents<BoxCollider2D>()[1].enabled = true;
            GetComponents<BoxCollider2D>()[2].enabled = false;
        }
    }
}