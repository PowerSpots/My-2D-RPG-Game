using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// 选择攻击按钮，然后选择敌人执行不同的攻击
public class Attack : MonoBehaviour
{
    public bool attackSelected = false;
    public int hitAmount = 0;

    public Button smack;
    public Button wack;
    public Button kick;
    public Button chop;

    // 当4个按钮被分别按下时，执行具有不同攻击量的不同攻击
    public void Smack()
    {
        hitAmount = 5;
        AttackTheEnemy();
    }
    public void Wack()
    {
        hitAmount = 10;
        AttackTheEnemy();
    }
    public void Kick()
    {
        hitAmount = 15;
        AttackTheEnemy();
    }
    public void Chop()
    {
        hitAmount = 20;
        AttackTheEnemy();
    }

    public void AttackTheEnemy()
    {
        // 打开玩家实际选择敌人进行攻击的功能
        attackSelected = true;
        // 高亮按钮的轮廓
        HighlightTheButton();
    }

    void HighlightTheButton()
    {
        // 根据hitAmount的当前值突出显示或取消选中适当的按钮
        if (hitAmount == 5)
        {
            // 向表示当前攻击的按钮添加轮廓
            smack.GetComponent<Outline>().enabled = true;
        }
        else
        {
            smack.GetComponent<Outline>().enabled = false;
        }
        if (hitAmount == 10)
        {
            wack.GetComponent<Outline>().enabled = true;
        }
        else { wack.GetComponent<Outline>().enabled = false;
        }
        if (hitAmount == 15)
        {
            kick.GetComponent<Outline>().enabled = true;
        }
        else
        {
            kick.GetComponent<Outline>().enabled = false;
        }
        if (hitAmount == 20)
        {
            chop.GetComponent<Outline>().enabled = true;
        }
        else
        {
            chop.GetComponent<Outline>().enabled = false;
        }
    }
}