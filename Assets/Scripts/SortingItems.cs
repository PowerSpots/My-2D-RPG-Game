using UnityEngine;
using System.Collections;
public class SortingItems : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        // 在主角上方
        if (transform.position.y >= player.transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = (player.GetComponent<SpriteRenderer>().sortingOrder) - 1;

        }
        // 在主角下方
        if (transform.position.y < player.transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = (player.GetComponent<SpriteRenderer>().sortingOrder) + 1;
        }
    }
}