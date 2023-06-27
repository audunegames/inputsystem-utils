using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.InputSystem;

namespace Audune.Utils.InputSystem
{
  // Class that defines utility methods for displaying input actions
  public static class InputDisplayUtils
  {
    // Class that defines options for converting bindings to a TextMeshPro sprite
    public class SpriteDisplayOptions
    {
      // The name of the sprite asset to use
      public string spriteAssetName = "";

      // Indicate if the sprite should be tinted
      public bool tint = false;      
    }


    #region Rewriting control paths of bindings
    // Rewrite a control path so it can be used as a sprite name by replacing angle brackets for layouts to square brackets
    // E.g. "<Gamepad>/buttonSouth" becomes "[Gamepad]/buttonSouth"
    private static string RewriteControlPath(string path)
    {
      var builder = new StringBuilder();

      var components = InputControlPath.Parse(path).ToArray();
      for (var i = 0; i < components.Length; i++)
      {
        if (i > 0)
          builder.Append("/");

        var component = components[i];
        if (!string.IsNullOrEmpty(component.layout))
          builder.Append($"[{component.layout}]");
        foreach (var usage in component.usages)
          builder.Append($"{{{usage}}}");
        if (!string.IsNullOrEmpty(component.displayName))
          builder.Append($"#({component.displayName})");
        if (!string.IsNullOrEmpty(component.name))
          builder.Append(component.name);
      }

      return builder.ToString();
    }
    #endregion

    #region Converting a binding to a TextMeshPro sprite
    // Return a string containing a TextMeshPro sprite for a binding
    public static string ToTextMeshProSprite(this InputBinding binding, SpriteDisplayOptions options = null)
    {
      options ??= new SpriteDisplayOptions();

      if (binding == null)
        return string.Empty;
      return new TextMeshProSprite(RewriteControlPath(binding.effectivePath), options.spriteAssetName, options.tint);
    }

    // Return a string containing TextMeshPro sprites for an enumerable of bindings
    public static string ToTextMeshProSpriteSequence(this IEnumerable<InputBinding> bindings, SpriteDisplayOptions options = null, string separator = "")
    {
      options ??= new SpriteDisplayOptions();

      if (bindings == null)
        return string.Empty;
      return string.Join(separator, bindings.Select(binding => ToTextMeshProSprite(binding, options)));
    }

    // Return a string containing a TextMeshPro sprite for the first binding in an enumerable of bindings
    public static string ToTextMeshProSpriteFirstOnly(this IEnumerable<InputBinding> bindings, SpriteDisplayOptions options = null)
    {
      options ??= new SpriteDisplayOptions();

      if (bindings == null)
        return string.Empty;
      return ToTextMeshProSprite(bindings.ElementAtOrDefault(0), options);
    }
    #endregion

    #region Converting a binding reference to a TextMeshPro sprite
    // Return a string containing a TextMeshPro sprite for a binding
    public static string ToTextMeshProSprite(this BindingReference binding, SpriteDisplayOptions options = null)
    {
      options ??= new SpriteDisplayOptions();

      if (binding == null)
        return string.Empty;
      return ToTextMeshProSprite(binding.binding, options);
    }

    // Return a string containing TextMeshPro sprites for an enumerable of bindings
    public static string ToTextMeshProSpriteSequence(this IEnumerable<BindingReference> bindings, SpriteDisplayOptions options = null, string separator = "")
    {
      options ??= new SpriteDisplayOptions();

      if (bindings == null)
        return string.Empty;
      return string.Join(separator, bindings.Select(binding => ToTextMeshProSprite(binding, options)));
    }

    // Return a string containing a TextMeshPro sprite for the first binding in an enumerable of bindings
    public static string ToTextMeshProSpriteFirstOnly(this IEnumerable<BindingReference> bindings, SpriteDisplayOptions options = null)
    {
      options ??= new SpriteDisplayOptions();

      if (bindings == null)
        return string.Empty;
      return ToTextMeshProSprite(bindings.ElementAtOrDefault(0), options);
    }
    #endregion
  }
}
