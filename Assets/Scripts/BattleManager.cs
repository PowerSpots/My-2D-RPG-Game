using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

    // 配置战斗场景
    public GameObject[] EnemySpawnPoints;
    public GameObject[] EnemyPrefabs;
    public AnimationCurve SpawnAnimationCurve;

    // 管理战斗场景的变量
    private int enemyCount;
    enum BattlePhase
    {
        PlayerAttack, EnemyAttack
    }
    private BattlePhase phase;

    public CanvasGroup theButtons;

    // Use this for initialization
    void Start () {
        // 初始化战斗场景并用协程生成随机数量的敌人
        enemyCount = Random.Range(1, EnemySpawnPoints.Length);
        StartCoroutine(SpawnEnemies());
        phase = BattlePhase.PlayerAttack; 
    }

    // Update is called once per frame
    void Update () {
        // 根据是否轮到玩家开启和关闭按钮
        if (phase == BattlePhase.PlayerAttack)
        {
            theButtons.alpha = 1;
            theButtons.interactable = true;
            theButtons.blocksRaycasts = true;
        }
        else
        {
            theButtons.alpha = 0;
            theButtons.interactable = false;
            theButtons.blocksRaycasts = false;
        }
    }

    // 用循环遍历敌人，并将预制实例化在渲染屏幕外，再在另一个协程中用动画移动到生成点
    IEnumerator SpawnEnemies()
    {     
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy =(GameObject)Instantiate(EnemyPrefabs[0]);
            newEnemy.transform.position = new Vector3(10, -1, 0);
            yield return StartCoroutine(MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
            newEnemy.transform.parent =EnemySpawnPoints[i].transform;
        }
    }

    // 将敌人从屏幕外移动到其预设的生成点
    IEnumerator MoveCharacterToPoint(GameObject destination, GameObject character)
    {
        float timer = 0f;
        var StartPosition = character.transform.position;
        if (SpawnAnimationCurve.length > 0)
        {
            while (timer < SpawnAnimationCurve.keys[SpawnAnimationCurve.length - 1].time)
            {
                character.transform.position =
                    Vector3.Lerp(StartPosition,
                        destination.transform.position,
                            SpawnAnimationCurve.Evaluate(timer));
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            character.transform.position = destination.transform.position;
        }
    }

    // 逃跑后回到世界场景
    public void RunAway()
    {
        GameState.justExitedBattle = true;
        NavigationManager.NavigateTo("Overworld");
    }

}
