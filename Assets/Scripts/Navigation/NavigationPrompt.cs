using UnityEngine;
using System.Collections;
public class NavigationPrompt : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (NavigationManager.CanNavigate(this.tag))
        {
            Debug.Log("attempting to exit via " + tag);
            NavigationManager.NavigateTo(this.tag);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (NavigationManager.CanNavigate(this.tag))
        {
            Debug.Log("attempting to exit via " + tag);
            NavigationManager.NavigateTo(this.tag);
        }
    } 
}