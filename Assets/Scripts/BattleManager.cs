using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    // 战斗面板
    public CanvasGroup theButtons;

    // 配置战斗场景
    public GameObject[] EnemySpawnPoints;
    public GameObject[] EnemyPrefabs;
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
    private Dictionary<int, BattleState> battleStateHash = new Dictionary<int, BattleState>();
    private BattleState currentBattleState;
    public Animator battleStateManager;

    // 战斗入场动画
    public GameObject introPanel;
    private Animator introPanelAnim;

    // 战斗场景的变量
    private int enemyCount;
    private bool canSelectEnemy;
    private string selectedTargetName;
    private EnemyController selectedTarget;
    public GameObject selectionCircle;
    bool attacking = false;
    public bool CanSelectEnemy
    {
        get { return canSelectEnemy; }
    }
    public int EnemyCount
    {
        get { return enemyCount; }
    }

    // 粒子系统
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
        enemyCount = Random.Range(1, EnemySpawnPoints.Length);
        StartCoroutine(SpawnEnemies());

        GetAnimationStates();

}

void Update () {
        // 根据是否轮到玩家开启和关闭按钮
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

        // 从状态机中获取当前状态，根据当前状态切换条件语句
        currentBattleState = battleStateHash[battleStateManager.GetCurrentAnimatorStateInfo(0).shortNameHash];
        switch (currentBattleState)
        {
            case BattleState.Intro:
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

    // 用循环遍历敌人，并将预制实例化在渲染屏幕外，再在另一个协程中用动画移动到生成点
    IEnumerator SpawnEnemies()
    {     
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy =(GameObject)Instantiate(EnemyPrefabs[0]);
            newEnemy.transform.position = new Vector3(10, -1, 0);
            yield return StartCoroutine(MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
            newEnemy.transform.parent =EnemySpawnPoints[i].transform;

            var controller = newEnemy.GetComponent<EnemyController>();
            controller.BattleManager = this;

            var EnemyProfile = ScriptableObject.CreateInstance<Enemy>();
            EnemyProfile.Class = EnemyClass.Dragon;
            EnemyProfile.level = 1;
            EnemyProfile.damage = 1;
            EnemyProfile.health = 20;
            EnemyProfile.name = EnemyProfile.Class + " " + i.ToString();
            controller.EnemyProfile = EnemyProfile;
        }
        battleStateManager.SetBool("BattleReady", true);
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

    // 在Mecanim中为相应的动画状态生成一个散列，并将结果散列存储在字典中
    void GetAnimationStates()
    {
        foreach (BattleState state in (BattleState[])System.Enum.GetValues(typeof(BattleState)))
        {
            battleStateHash.Add(Animator.StringToHash(state.ToString()), state);
        }
    }

    public void SelectEnemy(EnemyController enemy, string name)
    {
        selectedTarget = enemy;
        selectedTargetName = name;
    }

    //
    IEnumerator AttackTarget()
    {
        attacking = true;
        var damageAmount = GetComponent<Attack>().hitAmount;
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
        selectedTarget.EnemyProfile.health -= damageAmount;
        yield return new WaitForSeconds(1f);
        attacking = false;
        GetComponent<Attack>().hitAmount = 0;
        battleStateManager.SetBool("PlayerReady", false);
        Destroy(attackParticle);
    }
}
