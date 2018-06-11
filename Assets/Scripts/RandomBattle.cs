using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RandomBattle : MonoBehaviour {

    // 在给定区域内遭遇战斗的概率
    public int battleProbability;
    // 随机数字，小于等于战斗概率，战斗发生
    int encounterChance = 100;
    // 两次战斗的间隔时间
    public int secondsBetweenBattles = 3;
    // 加载的战斗场景名称
    public string battleSceneName;

	private bool battleStay;

    // 当玩家进入区域时产生一个随机数（encounterChance）来确定是否会发生战斗
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!GameState.justExitedBattle)
        {
            encounterChance = Random.Range(1, 100);
            // 随机数大于战争概率，那么启动协程，一定时间后一个新的随机数被分配给encounterChance
            if (encounterChance > battleProbability)
            {
                
                StartCoroutine(RecalculateChance());
            }
        }
        // 如果justExitedBattle为true，则在两次战斗之间的时间结束后才会计算战斗概率
        else
        {
            StartCoroutine(RecalculateChance());
            GameState.justExitedBattle = false;
        }
    }

    IEnumerator RecalculateChance()
    {
		while (encounterChance > battleProbability)
        {
            yield return new WaitForSeconds(secondsBetweenBattles);
            GameState.saveLastPosition = true;
            encounterChance = Random.Range(1, 100);
        }

    }

    // 不断检查玩家是否要在特定区域内发生战斗
    void OnTriggerStay2D(Collider2D col)
    {
		if (encounterChance <= battleProbability) 
		{
			Debug.Log ("Battle");
			SceneManager.LoadScene (battleSceneName);
			battleProbability = 0;
		}
    }
    //  一旦玩家退出区域，在玩家重新进入区域之前不再尝试加载战斗场景
    void OnTriggerExit2D(Collider2D col)
    {
        encounterChance = 100;
        StopCoroutine(RecalculateChance());
    }
		
}
