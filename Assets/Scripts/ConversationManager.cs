using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : Singleton<ConversationManager>
{
    // 确保类是一个单例，不能使用构造函数
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
    public Text textHolder;

    // 查找对话框及其子对象，启动对话协程
    public void StartConversation(Conversation conversation)
    {
        dialogBox = GameObject.Find("Dialog Box").GetComponent<CanvasGroup>();
        imageHolder = GameObject.Find("Speaker Image").GetComponent<Image>();
        textHolder = GameObject.Find("Dialog Text").GetComponent<Text>();

        if (!talking)
        {
            StartCoroutine(DisplayConversation(conversation));
        }
    }

    // 接受Conversation对话对象并遍历所有要显示的行
    IEnumerator DisplayConversation(Conversation conversation)
    {
        // / 将会话标志设置为false以表明对话开始
        talking = true;
        foreach (var conversationLine in conversation.ConversationLines)
        {
            // 设置指向当前对话项目的指针
            currentConversationLine = conversationLine;
            // 将文本和图像添加到文本和图像的持有者
            textHolder.text = currentConversationLine.ConversationText;
            imageHolder.sprite = currentConversationLine.DisplayPic;

            yield return new WaitForSeconds(3);
        }
        // 将会话标志设置为false以表明对话已经完成
        talking = false;
    }

    // 根据交谈标志设置对话框的透明度和阻止raycast
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