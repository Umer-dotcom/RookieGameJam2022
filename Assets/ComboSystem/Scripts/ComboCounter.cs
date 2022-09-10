#if TEXTMESHPRO_INSTALLED
using System.Text;
using ComboSystem.Scripts.ScriptableObjects;
using UnityEngine;
using TMPro;

namespace ComboSystem.Scripts
{
    public class ComboCounter : MonoBehaviour
    {
        
        #region Serialized Fields
        [Header("References")]
        [SerializeField] private GameObject comboGameObject = null;
        [SerializeField] private TweenConfig tweenConfiguration = null;
        [SerializeField] private TextPopup textPopUpComponent = null;
        
        [Header("Text Format")]
        [SerializeField] [TextArea] private string preCounterFormat = null;
        [SerializeField] [TextArea] private string postCounterFormat = null;
        #endregion
        
        #region Private Fields
        private int counter = 0;
        // Caches
        private int activeLevel;
        private TMP_Text comboText;
        private RectTransform comboTransform;
        #endregion

        #region Unity Methods
        private void Start()
        {
            comboText = comboGameObject.GetComponent<TMP_Text>();
            comboTransform = comboGameObject.GetComponent<RectTransform>();
            comboTransform.localScale = Vector3.zero;
            System.Array.Sort(tweenConfiguration.levels, (x, y) =>
                x.levelBoundaryLower.CompareTo(y.levelBoundaryLower));
        }
        #endregion
        
        #region Public Methods
        // The functions to call from your script
        public void Increment()
        {
            CounterCheck();
            UpdateLevel();
            counter++;
            UpdateText();
            #if USING_LEANTWEEN || USING_DOTWEEN
            TweenText();
            #else
            Debug.Log("Please Setup a Tweening Plugin from UnityToolBar->Tools/ComboSystem/TweeningSetup");
            #endif
            PopUpText();
        }

        public void Break()
        {
            #if USING_LEANTWEEN || USING_DOTWEEN
            ResetTweens();
            #endif
            counter = 0;
        }
        #endregion
        
        #region Private Methods
        private void CounterCheck() => counter = comboTransform.localScale == Vector3.zero ? 0 : counter; // Reset Counter if needed
        private void UpdateLevel()
        {
            if (counter == 0)
            {
                comboText.font = tweenConfiguration.levels[0].comboFont;
                activeLevel = 0;
            }
            else
            {
                for (var i = 1; i < tweenConfiguration.levels.Length; i++)
                {
                    if (counter != tweenConfiguration.levels[i].levelBoundaryLower - 1) continue;
                    comboText.font = tweenConfiguration.levels[i].comboFont;
                    activeLevel = i;
                    break;
                }
            }
        }
        private void UpdateText()
        {
            var stringBuilder = new StringBuilder(preCounterFormat);
            stringBuilder.Append(counter);
            stringBuilder.Append(postCounterFormat);
            comboText.text = stringBuilder.ToString();
        }
        
        #if USING_LEANTWEEN || USING_DOTWEEN
        #region Tweening Methods
        private void TweenText()
        {
            ResetTweens();
            Tween.Scale(comboGameObject, RandomScale(), tweenConfiguration.inDuration);
            var temp = Tween.Rotate(Tween.Axis.Z, comboGameObject, (Random.value > 0.5f ? 1 : -1) *
                Random.Range(tweenConfiguration.minRotation, tweenConfiguration.maxRotation), 
                tweenConfiguration.inDuration);
            Tween.ActionOnComplete(temp, Minimize);
        }
        private void ResetTweens()
        {
            Tween.Clear(comboGameObject);
            comboTransform.localScale = Vector3.zero;
            comboTransform.localRotation = Quaternion.identity;
        }
        private Vector3 RandomScale()
        {
            var rand = Random.Range(tweenConfiguration.minScale, tweenConfiguration.maxScale);
            return new Vector3(rand, rand, 1f);
        }
        private void Minimize()
        {
            Tween.Scale(comboGameObject, Vector3.zero, tweenConfiguration.outDuration);
            Tween.Rotate(Tween.Axis.Z, comboGameObject, 0f, tweenConfiguration.outDuration);
        }
        #endregion
        #endif
        
        private void PopUpText()
        {
            if (textPopUpComponent) textPopUpComponent.PopUpWord(activeLevel);
        }
        #endregion
    }
}
#endif