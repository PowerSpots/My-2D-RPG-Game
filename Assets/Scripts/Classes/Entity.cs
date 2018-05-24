using UnityEngine;
/// <summary>
/// ���н�ɫ�Ļ���
/// </summary>
public class Entity : ScriptableObject
{
    // ����
    public string name;
    // ����
    public int age;
    // ����
    string faction;
    // ְҵ
    public string occupation;
    // �ȼ�
    public int level = 1;
    // ����
    public int health = 2;
    // ����
    public int strength = 1;
    // ħ��
    public int magic = 0;
    // ����
    public int defense = 0;
    // �ٶ�
    public int speed = 1;
    // �˺�
    public int damage = 1;
    // ����
    public int armor = 0;
    // ��������
    public int noOfAttacks = 1;
    // ����
    public string weapon;
    // λ��
    public Vector2 position;
    // �����˺�ʱ���õķ���
    public void TakeDamage(int Amount)
    {
        health = health - Mathf.Clamp((Amount - armor), 0, int.MaxValue);
    }
    // ����ʱ���õķ���
    public void Attack(Entity Entity)
    {
        Entity.TakeDamage(strength);
    }
}