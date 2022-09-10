using UnityEngine;

namespace ComboSystem.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TweenConfig_", menuName = "Combo System/Tweening Configuration")]
    public class TweenConfig : ScriptableObject
    {
        [Header("Combo Counter Tween Settings")]
        public float minScale = 0.17f;
        public float maxScale = 0.15f;
        public float minRotation = 5f;
        public float maxRotation = 10f;
        public float inDuration = 0.15f;
        public float outDuration = 2.5f;
        [Header("Text PopUp Tween Settings")]
        public float popUpDuration = 1.2f;
        public Vector3 popUpMovement = new Vector3(0f, 5f, 0f);
        public AnimationCurve popUpCurve;
        #if TEXTMESHPRO_INSTALLED
        [Header("Level Configurations")]
        public LevelConfig[] levels = new LevelConfig[3];
        #endif
    }
}
