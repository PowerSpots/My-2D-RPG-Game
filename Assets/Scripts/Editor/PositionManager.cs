using UnityEngine;
using UnityEditor;
public class PositionManager : MonoBehaviour
{
    // 创建保存序列化信息的菜单项
    [MenuItem("Assets/Create/PositionManager")]
    public static void CreateAsset()
    {
        ScriptingObjects positionManager =ScriptableObject.CreateInstance<ScriptingObjects>();

        AssetDatabase.CreateAsset(positionManager,"Assets/newPositionManager.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = positionManager;
    }
} 