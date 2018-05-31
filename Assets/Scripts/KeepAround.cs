using UnityEngine;
using System.Collections;

// 加载新的场景时，不要销毁被该脚本附加的任何GameObject
public class KeepAround : MonoBehaviour
{
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }
}