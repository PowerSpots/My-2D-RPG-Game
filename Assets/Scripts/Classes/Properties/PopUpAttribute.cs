using UnityEngine;

// 从PropertyAttribute类派生的属性类必须
public class PopUpAttribute : PropertyAttribute
{
    public string[] value;
    // 与属性数量相同参数的构造函数
    public PopUpAttribute(params string[] input)
    {
        value = input;
    }
}