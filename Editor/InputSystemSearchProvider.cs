using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Audune.Utils.InputSystem.Editor
{
  // Base class that defines a search provider
  internal abstract class InputSystemSearchProvider<TValue> : ScriptableObject, ISearchWindowProvider
  {
    // Class that defines an item in a selector for FMOD studio components
    public sealed class Item
    {
      // The value of the item
      public readonly TValue value;

      // The path of the item
      public readonly string path;

      // The icon of the item
      public readonly Texture icon;


      // Constructor
      public Item(TValue value, string path, Texture icon)
      {
        this.value = value;
        this.path = path;
        this.icon = icon;
      }
    }

    // The title of the search provider
    public string title;

    // The items of the search provider
    public List<Item> items;

    // The callback of the search provider when an entry is selected
    public Action<TValue> onSelectCallback;


    // Create the search tree
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
      var list = new List<SearchTreeEntry>();
      var groups = new List<string>();

      // Add the root and the none value to the tree
      list.Add(new SearchTreeGroupEntry(new GUIContent(title), 0));
      list.Add(new SearchTreeEntry(new GUIContent("None")) { level = 1, userData = default(TValue) });

      // Iterate over the items
      foreach (var item in items.GroupBy(item => item.path).Select(group => group.First()))
      {
        var components = item.path.Split("/");

        // Add the groups to the tree
        var group = "";
        for (var i = 0; i < components.Length - 1; i++)
        {
          group += components[i];
          if (!groups.Contains(group))
          {
            list.Add(new SearchTreeGroupEntry(new GUIContent(components[i], i == 0 ? InputSystemEditorGUIUtils.LoadIcon("InputActionAsset") : null), i + 1));
            groups.Add(group);
          }
          group += "/";
        }

        // Add the item to the tree
        list.Add(new SearchTreeEntry(new GUIContent(components[^1], item.icon)) { level = components.Length, userData = item.value });
      }

      return list;
    }

    // Event handler for selecting an entry
    public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
    {
      onSelectCallback?.Invoke((TValue)entry.userData);
      return true;
    }
  }

  // Class that defines a search provider for action maps
  internal class ActionMapSearchProvider : InputSystemSearchProvider<InputActionMap>
  {
    // Create a search provider
    public static InputSystemSearchProvider<InputActionMap> Create(IEnumerable<InputActionAsset> actionAssets, Action<InputActionMap> onSelectCallback)
    {
      var provider = CreateInstance<ActionMapSearchProvider>();
      provider.title = "Action Maps";
      provider.items = actionAssets
        .SelectMany(asset => asset.actionMaps.Select(actionMap => new Item(actionMap, $"{asset.name}/{actionMap.name}", InputSystemEditorGUIUtils.LoadIcon("InputControl"))))
        .ToList();
      provider.onSelectCallback = onSelectCallback;
      return provider;
    }
  }

  // Class that defines a search provider for control schemes
  internal class ControlSchemeSearchProvider : InputSystemSearchProvider<InputControlScheme>
  {
    // Create a search provider
    public static InputSystemSearchProvider<InputControlScheme> Create(IEnumerable<InputActionAsset> actionAssets, Action<InputControlScheme> onSelectCallback)
    {
      var provider = CreateInstance<ControlSchemeSearchProvider>();
      provider.title = "Action Maps";
      provider.items = actionAssets
        .SelectMany(asset => asset.controlSchemes.Select(controlScheme => new Item(controlScheme, $"{asset.name}/{controlScheme.name}", InputSystemEditorGUIUtils.LoadIconForControlScheme(controlScheme))))
        .ToList();
      provider.onSelectCallback = onSelectCallback;
      return provider;
    }
  }
}