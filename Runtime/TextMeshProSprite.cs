using System.Linq;
using TMPro;
using UnityEngine;

namespace Audune.Utils.InputSystem
{
  // Class that defines a TextMeshPro sprite
  public sealed class TextMeshProSprite
  {
    // The name of the sprite
    public readonly string spriteName;

    // The name of the sprite asset to use
    public readonly string spriteAssetName;

    // Indicate if the sprite should be tinted
    public readonly bool tint;




    // Return the TextMeshPro sprite
    public TMP_SpriteCharacter sprite => spriteAsset.spriteCharacterTable
      .FirstOrDefault(c => c.name == spriteName);

    // Return the TextMashPro sprite asset to use
    public TMP_SpriteAsset spriteAsset => !string.IsNullOrEmpty(spriteAssetName)
      ? Resources.Load<TMP_SpriteAsset>(TMP_Settings.defaultSpriteAssetPath + spriteAssetName)
      : TMP_Settings.defaultSpriteAsset;

    // Return if the TextMeshPro sprite exists
    public bool spriteExists => sprite != null;


    // Constructor
    public TextMeshProSprite(string spriteName, string spriteAssetName = null, bool tint = false)
    {
      this.spriteName = spriteName;
      this.spriteAssetName = spriteAssetName;
      this.tint = tint;
    }

    // Return the string representation of the sprite
    public override string ToString()
    {
      if (!string.IsNullOrEmpty(spriteAssetName))
        return $"<sprite=\"{spriteAssetName}\" name=\"{spriteName}\" tint={(tint ? "1" : "0")}>";
      else
        return $"<sprite name=\"{spriteName}\" tint={(tint ? "1" : "0")}>";
    }


    #region Implicit operators
    // Return the string representation of the sprite
    public static implicit operator string(TextMeshProSprite sprite)
    {
      return sprite.ToString();
    }
    #endregion
  }
}
