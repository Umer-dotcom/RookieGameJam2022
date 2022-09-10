#if TEXTMESHPRO_INSTALLED
using ComboSystem.Scripts.ScriptableObjects;
using UnityEngine;
using TMPro;

namespace ComboSystem.Scripts
{
    public class TextPopup : MonoBehaviour
    {
        
        #region Serialized Fields
        [Header("References")]
        [SerializeField] private GameObject popUpGameObject = null;
        [SerializeField] private TweenConfig tweenConfiguration = null;
        #endregion
        
        # region Private Fields
        private string[][] words;
        // Caches
        private TMP_Text popUpText;
        private RectTransform popUpTransform;
        private Vector3 originalPosition;
        private Vector3 scaleCache;
        #endregion

        #region Unity Methods
        private void Start()
        {
            popUpText = popUpGameObject.GetComponent<TMP_Text>();
            popUpTransform = popUpGameObject.GetComponent<RectTransform>();
            popUpTransform.localScale = Vector3.zero;
            originalPosition = popUpTransform.position;
            System.Array.Sort(tweenConfiguration.levels, (x, y)
                => x.levelBoundaryLower.CompareTo(y.levelBoundaryLower));
            words = new string[tweenConfiguration.levels.Length][];
            for (var i = 0; i < tweenConfiguration.levels.Length; i++)
            {
                words[i] = tweenConfiguration.levels[i].wordsFile.text.Split('\n');
            }
        }
        #endregion

        #region Public Methods
        // The function to call from your script
        public void PopUpWord(int level)
        {
            popUpText.font = tweenConfiguration.levels[level].popUpFont;
            popUpText.text = words[level][Random.Range(0, words[level].Length)];
            var scale = tweenConfiguration.levels[level].popUpMaximumScale;
            scaleCache = new Vector3(scale, scale, 1f);
            #if USING_LEANTWEEN || USING_DOTWEEN
            TweenText();
            #else
            Debug.Log("Please Setup a Tweening Plugin from UnityToolBar->Tools/ComboSystem/TweeningSetup");
            #endif
        }
        #endregion
        
        #region Private Methods
        #if USING_LEANTWEEN || USING_DOTWEEN
        private void TweenText()
        {
            ResetTweens();
            var x = Tween.Scale(popUpGameObject, scaleCache,
                tweenConfiguration.popUpDuration);
            Tween.SetOnEase(x, tweenConfiguration.popUpCurve);
            Tween.Move(popUpGameObject, originalPosition + tweenConfiguration.popUpMovement,
                tweenConfiguration.popUpDuration, false);
        }
        private void ResetTweens()
        {
            Tween.Clear(popUpGameObject);
            popUpTransform.localScale = Vector3.zero;
            popUpTransform.position = originalPosition;
        }
        #endif
        #endregion
    }
}
#endif