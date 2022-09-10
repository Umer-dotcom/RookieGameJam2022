//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;

//public class RageController : MonoBehaviour
//{
//    [SerializeField] GameObject sliderObject;
//    [SerializeField] GameObject buttonObject;
//    [SerializeField] GameObject playerObj;
//    [SerializeField] int hitssNeededToRage;

//    private const string rageAnimName = "Fight";
//    Slider slider;
//    Button button;
//    Animator animator;
//    private int rageCounter = 0;
//    bool rageAvailable = true;
//    PlayerScript player;

//    private void Awake()
//    {
//        slider = sliderObject.GetComponent<Slider>();
//        button = buttonObject.GetComponent<Button>();
//        animator = playerObj.GetComponent<Animator>();
//        player = playerObj.GetComponent<PlayerScript>();

//        //button.enabled = false;
//        slider.value = 0;
//    }
//    private void OnEnable()
//    {
//        Bullets.HitEvent += Hit;
        
//    }
//    private void OnDisable()
//    {
//        Bullets.HitEvent -= Hit;
//    }
//    private void Hit(int hitDamage)
//    {
//        rageCounter += hitDamage;
//        slider.value = (rageCounter / hitssNeededToRage) * 100;
//        if (rageCounter >=  hitssNeededToRage)
//        {
//            rageAvailable = true;
//        }
//    }
//    public void InitRageMode()
//    {
//        Debug.Log("RAGE BEGUNNN");
//        if (rageAvailable)
//        {
//            player.gun.StopGun();
           
            
//            animator.SetTrigger("rage");
//            //rageAvailable = false;

//        }
        
//    }
//    IEnumerator EndRageMode()
//    {
//        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !animator.IsInTransition(0))
//        {
//            yield return null;
//            player.gun.StartGun();
//            slider.value = 0;
            
//        }
        
//    }
//    IEnumerator FillSlider(float increments)
//    {
//        while (slider.value < 100)
//        {
//            yield return null;
//            slider.value += increments;
//        }

//    }
    

//}
