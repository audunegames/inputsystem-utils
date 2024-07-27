using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;

using UnityInputSystem = UnityEngine.InputSystem.InputSystem;

namespace Audune.Utils.InputSystem
{
  // Class that defines utility methods for input control layouts
  public static class InputControlLayoutUtils
  {
    #region Matching layouts
    // Try to match a layout or one of its base layouts to the specified predicate and convert it to a result
    public static bool TryMatchLayout<T>(string layoutName, Func<string, T> selector, Func<T, bool> predicate, out T matchedResult)
    {
      if (layoutName == null)
        throw new ArgumentNullException(nameof(layoutName));
      if (selector == null)
        throw new ArgumentNullException(nameof(selector));

      matchedResult = selector(layoutName);
      if (predicate == null || predicate(matchedResult))
        return true;

      var layout = UnityInputSystem.LoadLayout(layoutName);
      foreach (var baseLayoutName in layout.baseLayouts)
      {
        if (TryMatchLayout(baseLayoutName, selector, predicate, out matchedResult))
          return true;
      }

      return false;
    }

    // Try to match a layout or one of its base layouts to the specified predicate
    public static bool TryMatchLayout(string layoutName, Func<String, bool> predicate, out string matchedLayoutName)
    {
      return TryMatchLayout(layoutName, l => l, predicate, out matchedLayoutName);
    }
    #endregion

    #region Getting and matching layouts for control paths
    // Replace the first occurrence in a string
    private static string ReplaceFirst(this string s, string oldValue, string newValue)
    {
      var regex = new Regex(Regex.Escape(oldValue));
      return regex.Replace(s, newValue, 1);
    }


    // Try to get the layout for a control path based on the specified predicate
    public static bool TryGetLayoutForPath(string path, out string layoutName, out string layoutPath)
    {
      layoutName = string.Empty;
      layoutPath = path;
      if (string.IsNullOrEmpty(path))
        return false;

      var pathComponents = InputControlPath.Parse(path).ToList();
      if (pathComponents.Count == 0)
        return false;

      layoutName = pathComponents[0].layout;
      if (!string.IsNullOrEmpty(layoutName))
        return true;

      var deviceName = pathComponents[0].name;
      if (string.IsNullOrEmpty(deviceName))
        return false;

      var device = UnityInputSystem.GetDevice(deviceName);
      if (device == null)
        return false;

      layoutName = device.layout;
      layoutPath = path.ReplaceFirst(deviceName, $"<{layoutName}>");
      return !string.IsNullOrEmpty(layoutName);
    }

    // Try to match the layout of a control path to the specified predicate and convert it to a result
    public static bool TryMatchLayoutForPath<T>(string path, Func<string, string, T> selector, Func<string, T, bool> predicate, out T matchedResult)
    {
      if (selector == null)
        throw new ArgumentNullException(nameof(selector));

      matchedResult = default;
      if (!TryGetLayoutForPath(path, out var layoutName, out var layoutPath))
        return false;

      return TryMatchLayout(layoutName, l => selector(layoutPath.ReplaceFirst($"<{layoutName}>", $"<{l}>"), l), l => predicate == null || predicate(layoutPath, l), out matchedResult);
    }

    // Try to match the layout of a control path to the specified predicate
    public static bool TryMatchLayoutForPath(string path, Func<string, string, bool> predicate, out string matchedLayoutName)
    {
      return TryMatchLayoutForPath(path, (p, l) => l, predicate, out matchedLayoutName);
    }
    #endregion
   }
}
