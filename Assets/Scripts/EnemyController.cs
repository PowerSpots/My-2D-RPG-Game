using System.Collections;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    // 选中敌人的光标
    private bool selected;
    GameObject selection;

    private BattleManager battleManager;
    public Enemy EnemyProfile;
    Animator enemyAI;
    public BattleManager BattleManager
    {
        get { return battleManager; }
        set { battleManager = value; }
    }

    public void Awake()
    {
        enemyAI = GetComponent<Animator>();
        if (enemyAI == null)
        {
            Debug.LogError("No AI System Found");
        }
    }

    void Update()
    {
        UpdateAI();
    }
    public void UpdateAI()
    {
        if (enemyAI != null && EnemyProfile != null)
        {
            enemyAI.SetInteger("EnemyHealth", EnemyProfile.health);
            enemyAI.SetInteger("PlayerHealth", GameState.CurrentPlayer.health);
            enemyAI.SetInteger("EnemiesInBattle", battleManager.EnemyCount);
        }
    }

    void OnMouseDown()
    {
        if (battleManager.CanSelectEnemy)
        {
            selection = (GameObject)GameObject.Instantiate(battleManager.selectionCircle);
            selection.transform.parent = transform;
            selection.transform.localPosition = new Vector3(0f, -1f, 0f);
            selection.transform.localScale = new Vector3(4f, 4f, 1f);
            StartCoroutine("SpinObject", selection);
            battleManager.SelectEnemy(this, EnemyProfile.name);
            battleManager.GetComponent<Attack>().attackSelected = false;
            battleManager.battleStateManager.SetBool("PlayerReady", true);
        }
    }

    // 更新选中光标的旋转变换
    IEnumerator SpinObject(GameObject target)
    {
        while (true)
        {
            target.transform.Rotate(0, 0, 180 * Time.deltaTime);
            yield return null;
        }

    }


}