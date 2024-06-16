using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Audune.Utils.InputSystem.Editor
{
  using UnityInputSystem = UnityEngine.InputSystem.InputSystem;

  // Class that defines GUI utility methods for input system in the Unity editor
  public static class InputSystemEditorGUIUtils
  {
    // The path to the icons of the input system package
    private const string iconPath = "Packages/com.unity.inputsystem/InputSystem/Editor/Icons/";


    #region Loading icons
    // Load an icon from the input system package
    public static Texture2D LoadIcon(string name)
    {
      var skinPrefix = EditorGUIUtility.isProSkin ? "d_" : "";
      var scale = Mathf.Clamp((int)EditorGUIUtility.pixelsPerPoint, 0, 4);
      var scalePostFix = scale > 1 ? $"@{scale}x" : "";
      if (name.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
        name = string.Join("_", name.Split(Path.GetInvalidFileNameChars()));
      var path = Path.Combine(iconPath, skinPrefix + name + scalePostFix + ".png");
      return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
    }

    // Load an icon for a input layout
    public static Texture2D LoadIconForLayout(string layoutName)
    {
      var icon = LoadIcon(layoutName);
      if (icon != null)
        return icon;

      var layout = UnityInputSystem.LoadLayout(layoutName);
      if (layout != null)
      {
        foreach (var baseLayoutName in layout.baseLayouts)
        {
          icon = LoadIconForLayout(baseLayoutName);
          if (icon != null)
            return icon;
        }
      }

      return null;
    }

    // Load an icon for an input control scheme
    public static Texture2D LoadIconForControlScheme(InputControlScheme controlScheme)
    {
      if (controlScheme.deviceRequirements.Count == 0)
        return null;
  
      return LoadIconForLayout(controlScheme.deviceRequirements[0].controlPath
        .Replace("<", "")
        .Replace(">", ""));
    }
    #endregion
  }
}