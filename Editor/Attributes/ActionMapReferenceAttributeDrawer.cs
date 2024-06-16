using UnityEditor;
using UnityEngine;

namespace Audune.Utils.InputSystem.Editor
{
  // Class that defines a drawer for a reference to an input action map
  [CustomPropertyDrawer(typeof(ActionMapReferenceAttribute))]
  public class ActionMapReferenceAttributeDrawer : PropertyDrawer
  {
    // Draw the property
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      InputSystemEditorGUI.ActionMapDropdown(position, label, property);
    }

    // Return the property height
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return EditorGUIUtility.singleLineHeight;
    }
  }
}