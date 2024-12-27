using UnityEditor;
using UnityEngine;

public class ShaderForgeMaterialInspector : ShaderGUI {
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties) {
        // Викликаємо базовий GUI
        base.OnGUI(materialEditor, properties);

        // Додаємо кастомну логіку для властивостей, якщо потрібно
        EditorGUILayout.LabelField("Custom Shader Properties", EditorStyles.boldLabel);
    }
}
