using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    // 战斗面板
    public CanvasGroup theButtons;

    // 敌人可能生成的预制点和预制件
    public GameObject[] EnemySpawnPoints;
    public GameObject[] EnemyPrefabs;
    // 控制龙生成动画的曲线
    public AnimationCurve SpawnAnimationCurve;

    // 战斗状态
    public enum BattleState
    {
        Begin_Battle,
        Intro,
        Player_Move,
        Player_Attack,
        Change_Control,
        Enemy_Attack,
        Battle_Result,
        Battle_End
    }
    // 缓存状态机需要的散列值的字典
    private Dictionary<int, BattleState> battleStateHash = new Dictionary<int, BattleState>();
    // 保存当前状态的属性
    private BattleState currentBattleState;
    public Animator battleStateManager;

    // 战斗入场动画
    public GameObject introPanel;
    private Animator introPanelAnim;

    // 设置战斗场景的变量
    // 记录场景中活动的敌人数量
    private int enemyCount;
    // 是否可以选中敌人
    private bool canSelectEnemy;
    // 敌人名称
    private string selectedTargetName;
    private EnemyController selectedTarget;
    // 对SelectionCircle预制件的引用
    public GameObject selectionCircle;
    // 是否可以攻击
    bool attacking = false;
    public bool CanSelectEnemy
    {
        get { return canSelectEnemy; }
    }
    public int EnemyCount
    {
        get { return enemyCount; }
    }

    // 战斗效果粒子系统
    public GameObject smackParticle;
    public GameObject wackParticle;
    public GameObject kickParticle;
    public GameObject chopParticle;
    private GameObject attackParticle;


    void Awake()
    {
        introPanelAnim = introPanel.GetComponent<Animator>();

        battleStateManager = GetComponent<Animator>();
        if (battleStateManager == null)
        {
            Debug.LogError("No battleStateMachine Animator found.");
        }
    }

    void Start ()
    {
        // 初始化战斗场景并用协程生成随机数量的敌人
        // 计算需要随机生成敌人的数量
        enemyCount = Random.Range(1, EnemySpawnPoints.Length);
        // 开始生成敌人的协程
        StartCoroutine(SpawnEnemies());
        // 在Mecanim中为相应的动画状态生成一个散列，并将结果散列存储在字典中
        GetAnimationStates();
}

void Update () {
        // 根据是否轮到玩家回合开启和关闭按钮
        if (currentBattleState == BattleState.Player_Move)
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

        // 从状态机中获取当前状态的散列值，在字典中根据散列值获取当前状态，根据当前状态为可能发生的事情设置一个选项
        currentBattleState = battleStateHash[battleStateManager.GetCurrentAnimatorStateInfo(0).shortNameHash];
        switch (currentBattleState)
        {
            case BattleState.Intro:
                // 通知状态机触发战斗进场动画
                introPanelAnim.SetTrigger("Intro");
                break;
            case BattleState.Player_Move:
                if (GetComponent<Attack>().attackSelected == true)
                {
                    canSelectEnemy = true;
                }
                break;
            case BattleState.Player_Attack:
                canSelectEnemy = false;
                if (!attacking)
                {
                    StartCoroutine(AttackTarget());
                }
                break;
            case BattleState.Change_Control:
                break;
            case BattleState.Enemy_Attack:
                break;
            case BattleState.Battle_Result:
                break;
            case BattleState.Battle_End:
                break;
            default:
                break;
        }
    }

    // 用循环遍历敌人，并将预制实例化在屏幕外，再在另一个协程中用动画移动到生成点
    IEnumerator SpawnEnemies()
    {     
        for (int i = 0; i < enemyCount; i++)
        {
            // 在屏幕外创建敌人的实例
            var newEnemy =(GameObject)Instantiate(EnemyPrefabs[0]);
            newEnemy.transform.position = new Vector3(10, -1, 0);
            // 使用另一个协程序将它动画到屏幕上
            yield return StartCoroutine(MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
            // 将敌人锚定到指定的重生点
            newEnemy.transform.parent =EnemySpawnPoints[i].transform;

            var controller = newEnemy.GetComponent<EnemyController>();
            controller.BattleManager = this;

            // 使用脚本对象资源设置敌人资料
            var EnemyProfile = ScriptableObject.CreateInstance<Enemy>();
            EnemyProfile.Class = EnemyClass.Dragon;
            EnemyProfile.level = 1;
            EnemyProfile.damage = 1;
            EnemyProfile.health = 20;
            EnemyProfile.name = EnemyProfile.Class + " " + i.ToString();
            // 用新的EnemyProfile类初始化控制器
            controller.EnemyProfile = EnemyProfile;
        }
        // 通知状态机进入战斗状态
        battleStateManager.SetBool("BattleReady", true);
    }

    // 将龙从屏幕外移动到预期的生成点
    IEnumerator MoveCharacterToPoint(GameObject destination, GameObject character)
    {
        // 计算出敌人从哪里开始
        float timer = 0f;
        var StartPosition = character.transform.position;
        // 检查AnimationCurve中是否存在动画步骤（键）
        if (SpawnAnimationCurve.length > 0)
        {
            // 如果动画中有键，那么继续迭代，直到到达基于该步时间和当前迭代时间的动画的最后一个步骤
            while (timer < SpawnAnimationCurve.keys[SpawnAnimationCurve.length - 1].time)
            {
                // 在开始位置到结束位置之间使用Lerp和动画曲线的速率来控制对象位置
                character.transform.position =
                    Vector3.Lerp(StartPosition,
                        destination.transform.position,
                            SpawnAnimationCurve.Evaluate(timer));
                timer += Time.deltaTime;
                // 当下一帧准备好时，我们只进入下一个动画步骤来逐渐逐步完成流畅的移动
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

    // 在Mecanim中为相应的动画状态生成一个散列，并将结果散列存储在字典中
    void GetAnimationStates()
    {
        foreach (BattleState state in (BattleState[])System.Enum.GetValues(typeof(BattleState)))
        {
            battleStateHash.Add(Animator.StringToHash(state.ToString()), state);
        }
    }

    // 选择敌人
    public void SelectEnemy(EnemyController enemy, string name)
    {
        selectedTarget = enemy;
        selectedTargetName = name;
    }

    //  根据选定的攻击降低选定的敌人的健康
    IEnumerator AttackTarget()
    {
        // 设置表示玩家正在攻击的变量
        attacking = true;
        var damageAmount = GetComponent<Attack>().hitAmount;
        // 查看攻击量选择正确的粒子系统进行实例化
        switch (damageAmount)
        {
            case 5:
                attackParticle = (GameObject)GameObject.Instantiate(smackParticle);
                break;
            case 10:
                attackParticle = (GameObject)GameObject.Instantiate(wackParticle);
                break;
            case 15:
                attackParticle = (GameObject)GameObject.Instantiate(kickParticle);
                break;
            case 20:
                attackParticle = (GameObject)GameObject.Instantiate(chopParticle);
                break;
        }
        if (attackParticle != null)
        {
            attackParticle.transform.position = selectedTarget.transform.position;
        }
        // 根据选定的攻击降低选定的敌人的生命值
        selectedTarget.EnemyProfile.health -= damageAmount;
        // 等待1秒钟以重置攻击数值
        yield return new WaitForSeconds(1f);
        attacking = false;
        GetComponent<Attack>().hitAmount = 0;
        // 切换到下一个状态
        battleStateManager.SetBool("PlayerReady", false);
        Destroy(attackParticle);
    }
}
