#if TEXTMESHPRO_INSTALLED
using TMPro;
using UnityEngine;

namespace ComboSystem.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level_", menuName = "Combo System/Level Configuration")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] public int levelBoundaryLower;
        [Tooltip("Only used if ScriptableObject referenced in a ComboCounter Component")]
        [Header("Combo Counter Configuration")]
        [SerializeField] public TMP_FontAsset comboFont;
        [Tooltip("Only used if ScriptableObject referenced in a TextPopup Component")]
        [Header("Text Popup Configuration")]
        [SerializeField] public TMP_FontAsset popUpFont;
        [SerializeField] public TextAsset wordsFile;
        [SerializeField] public float popUpMaximumScale;
    }
}
#endif