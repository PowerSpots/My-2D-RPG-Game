using UnityEngine;
using System.Collections;

public class Entity : ScriptableObject
{
    // 姓名
    public string name;
    // 年龄
    public int age;
    // 帮派
    string faction;
    // 职业
    public string occupation;
    // 等级
    public int level = 1;
    // 健康
    public int health = 2;
    // 力量
    public int strength = 1;
    // 魔法
    public int magic = 0;
    // 防御
    public int defense = 0;
    // 速度
    public int speed = 1;
    // 伤害
    public int damage = 1;
    // 护甲
    public int armor = 0;
    // 攻击次数
    public int noOfAttacks = 1;
    // 武器
    public string weapon;
    // 位置
    public Vector2 position;
    // 承受伤害时调用的方法
    public void TakeDamage(int Amount)
    {
        health = health - Mathf.Clamp((Amount - armor), 0, int.MaxValue);
    }
    // 攻击时调用的方法
    public void Attack(Entity Entity)
    {
        Entity.TakeDamage(strength);
    }
}