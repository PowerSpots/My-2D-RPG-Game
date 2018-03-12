using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : Singleton<ConversationManager>
{
    // 继承自单例模式
    protected ConversationManager () {}

    // 是否有对话进行
    bool talking = false;

    // 当前显示的文本行
    ConversationEntry currentConversationLine;

    // 对话框的组属性
    public CanvasGroup dialogBox;

    // 图片占位符
    public Image imageHolder;

    // 文本占位符
    public TMP_Text textHolder;

    // 查找对话框及其子对象，启动对话协程
    public void StartConversation(Conversation conversation)
    {
        dialogBox = GameObject.Find("Dialog Box").GetComponent<CanvasGroup>();
        imageHolder = GameObject.Find("Speaker Image").GetComponent<Image>();
        textHolder = GameObject.Find("Dialog Text").GetComponent<TMP_Text>();

        if (!talking)
        {
            StartCoroutine(DisplayConversation(conversation));
        }
    } 

    // 接受对话对象并遍历所有要显示的行的协程
    IEnumerator DisplayConversation(Conversation conversation)
    {
        talking = true;
        foreach (var conversationLine in conversation.ConversationLines)
        {
            currentConversationLine = conversationLine;
            textHolder.text = currentConversationLine.ConversationText;
            imageHolder.sprite = currentConversationLine.DisplayPic;
            yield return new WaitForSeconds(3);
        }
        talking = false;
    }

    // 根据是否在交谈将信息显示在屏幕上
    void OnGUI()
    {
        if (talking)
        {
            dialogBox.alpha = 1;
            dialogBox.blocksRaycasts = true;
        }
        else
        {
            dialogBox.alpha = 0;
            dialogBox.blocksRaycasts = false;
        }
    }
}