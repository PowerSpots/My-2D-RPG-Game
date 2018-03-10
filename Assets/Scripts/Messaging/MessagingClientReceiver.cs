using UnityEngine;
public class MessagingClientReceiver : MonoBehaviour
{
    void Start()
    {
        MessagingManager.Instance.Subscribe(ThePlayerIsTryingToLeave);
    }

    void ThePlayerIsTryingToLeave()
    {
        // 获取并检查 对话组件 脚本
        var dialog = GetComponent<ConversationComponent>();
        if (dialog != null)
        {
            // 检查对话组件里是否有对话内容
            if (dialog.Conversations != null && dialog.Conversations.Length > 0)
            {
                var conversation = dialog.Conversations[0];
                if (conversation != null)
                {
                    // 如果有内容，调用ConversationManager脚本开始第一个对话
                    ConversationManager.Instance.StartConversation(conversation);
                }
            }
        }
    }
}