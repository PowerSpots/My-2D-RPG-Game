using System.Collections;
using UnityEngine;

// 控制敌人执行操作
public class EnemyController : MonoBehaviour
{
    // 确定当前的敌人是否被选中
    private bool selected;
    // 选中敌人的光标预制件
    GameObject selection;

    // 对BattleManager脚本的引用
    private BattleManager battleManager;
    // 对敌人资料的引用
    public Enemy EnemyProfile;
    // AI状态机的引用
    Animator enemyAI;
    public BattleManager BattleManager
    {
        get { return battleManager; }
        set { battleManager = value; }
    }

    public void Awake()
    {
        // 获取当前针对GameObject所配置的AI状态机的引用
        enemyAI = GetComponent<Animator>();
        if (enemyAI == null)
        {
            Debug.LogError("No AI System Found");
        }
    }

    void Update()
    {
        // 确保需要战斗信息的AI每一帧都是最新的
        UpdateAI();
    }
    public void UpdateAI()
    {
        if (enemyAI != null && EnemyProfile != null)
        {
            // 将AI的属性置全部设为当前值
            enemyAI.SetInteger("EnemyHealth", EnemyProfile.health);
            enemyAI.SetInteger("PlayerHealth", GameState.CurrentPlayer.health);
            enemyAI.SetInteger("EnemiesInBattle", battleManager.EnemyCount);
        }
    }
    // 使用BoxCollider2D和OnMouseDown函数的组合来选择Dragon并显示选择标记
    void OnMouseDown()
    {
        if (battleManager.CanSelectEnemy)
        {
            // 创建SelectionCircle预制件的实例
            selection = (GameObject)GameObject.Instantiate(battleManager.selectionCircle);
            // 将其父对象设置为选定的龙
            selection.transform.parent = transform;
            // 设置本地位置，使其位于父对象龙的下方
            selection.transform.localPosition = new Vector3(0f, -1f, 0f);
            // 放大选择标记
            selection.transform.localScale = new Vector3(4f, 4f, 1f);
            // 启动SelectionCircle协程旋转
            if (selection != null)
            {
                StartCoroutine("SpinObject", selection);
            }
            
            // 通知BattleManager已经选择了要攻击的目标
            battleManager.SelectEnemy(this, EnemyProfile.name);
            // 重置攻击脚本中的attackSelected选择状态
            battleManager.GetComponent<Attack>().attackSelected = false;
            // 改变战斗状态来执行攻击
            battleManager.battleStateManager.SetBool("PlayerReady", true);

            Destroy(selection, 3f);
        }
    }

    // 不断更新选择标记的旋转变换
    IEnumerator SpinObject(GameObject target)
    {
        while (target != null)
        {
            target.transform.Rotate(0, 0, 180 * Time.deltaTime);
            yield return null;
        }
    }
}