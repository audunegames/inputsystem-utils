using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Audune.Utils.InputSystem.Editor
{
  // Class that defines GUI methods for input system in the Unity editor
  public class InputSystemEditorGUI
  {
    #region Drawing dropdowns
    // Draw a dropdown for action maps at the specified position
    public static void ActionMapDropdown(Rect position, GUIContent label, SerializedProperty property)
    {
      EditorGUI.BeginProperty(position, label, property);

      position = EditorGUI.PrefixLabel(position, label);

      var buttonLabel = !string.IsNullOrEmpty(property.stringValue) ? property.stringValue : "None";
      if (GUI.Button(position, buttonLabel, EditorStyles.popup))
        SearchWindow.Open(CreateSearchWindowContext(position), ActionMapSearchProvider.Create(Resources.FindObjectsOfTypeAll<InputActionAsset>(), (selected) => {
          property.serializedObject.Update();
          property.stringValue = selected?.name ?? null;
          property.serializedObject.ApplyModifiedProperties();
        }));

      EditorGUI.EndProperty();
    }

    // Draw a dropdown for control schemes at the specified position
    public static void ControlSchemeDropdown(Rect position, GUIContent label, SerializedProperty property)
    {
      EditorGUI.BeginProperty(position, label, property);

      position = EditorGUI.PrefixLabel(position, label);

      var buttonLabel = !string.IsNullOrEmpty(property.stringValue) ? property.stringValue : "None";
      if (GUI.Button(position, buttonLabel, EditorStyles.popup))
        SearchWindow.Open(CreateSearchWindowContext(position), ControlSchemeSearchProvider.Create(Resources.FindObjectsOfTypeAll<InputActionAsset>(), (selected) => {
          property.serializedObject.Update();
          property.stringValue = selected.name;
          property.serializedObject.ApplyModifiedProperties();
        }));

      EditorGUI.EndProperty();
    }


    // Create a search window context for a rect
    private static SearchWindowContext CreateSearchWindowContext(Rect position)
    {
      return new SearchWindowContext(GUIUtility.GUIToScreenPoint(position.max + new Vector2(position.width * -0.5f, position.height - EditorGUIUtility.standardVerticalSpacing - 1)), position.width);
    }
    #endregion
  }
}
