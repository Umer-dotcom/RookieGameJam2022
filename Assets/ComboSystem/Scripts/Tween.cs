using UnityEngine;
#if USING_DOTWEEN
using DG.Tweening;
#endif

namespace ComboSystem.Scripts
{
    public static class Tween
    {
        public enum Axis { X, Y, Z };

        #region MANAGE
        public static void Clear(GameObject go)
        {
            #if USING_LEANTWEEN
                LeanTween.cancel(go);
            #elif USING_DOTWEEN
                DOTween.Kill(go.transform);
            #endif
        }
        #if USING_LEANTWEEN
            public static LTDescr ActionOnComplete(LTDescr tween, System.Action action) => tween.setOnComplete(action);
            public static LTDescr SetOnEase(LTDescr tween, AnimationCurve ease) => tween.setEase(ease);
        #elif USING_DOTWEEN
            public static DG.Tweening.Tween ActionOnComplete(DG.Tweening.Tween tween, TweenCallback action)
            {
                tween.onComplete += action;
                return tween;
            }
            public static DG.Tweening.Tween SetOnEase(DG.Tweening.Tween tween, AnimationCurve ease) => tween.SetEase(ease);
        #endif
        #endregion

        #region MOVE
        #if USING_LEANTWEEN
            public static LTDescr Move(Axis axis, GameObject go, float destination, float duration, bool local = true)
            {
                return axis switch
                {
                    Axis.X => !local ? LeanTween.moveX(go, destination, duration) : LeanTween.moveLocalX(go, destination, duration),
                    Axis.Y => !local ? LeanTween.moveY(go, destination, duration) : LeanTween.moveLocalY(go, destination, duration),
                    Axis.Z => !local ? LeanTween.moveZ(go, destination, duration) : LeanTween.moveLocalZ(go, destination, duration),
                    _ => null
                };
            }
            public static LTDescr Move(GameObject go, Vector3 destination, float duration, bool local = true)
            {
                return !local ? LeanTween.move(go, destination, duration) : LeanTween.moveLocal(go, destination, duration);
            }
        #elif USING_DOTWEEN
            public static DG.Tweening.Tween Move(Axis axis, GameObject go, float movement, float duration, bool local = true)
            {
                switch (axis)
                {
                    case Axis.X:
                        return !local ? go.transform.DOMoveX(movement, duration) : go.transform.DOLocalMoveX(movement, duration);
                    case Axis.Y:
                        return !local ? go.transform.DOMoveY(movement, duration) : go.transform.DOLocalMoveY(movement, duration);
                    case Axis.Z when !local:
                        return go.transform.DOMoveZ(movement, duration);
                    case Axis.Z:
                        return go.transform.DOLocalMoveZ(movement, duration);
                }
                return null;
            }
            public static DG.Tweening.Tween Move(GameObject go, Vector3 destination, float duration, bool local = true)
            {
                return !local ? go.transform.DOMove(destination, duration) : go.transform.DOLocalMove(destination, duration);
            }
        #endif
        #endregion

        #region ROTATE
        #if USING_LEANTWEEN
            public static LTDescr Rotate(Axis axis, GameObject go, float rotation, float duration)
            {
                return axis switch
                {
                    Axis.X => LeanTween.rotateX(go, rotation, duration),
                    Axis.Y => LeanTween.rotateY(go, rotation, duration),
                    Axis.Z => LeanTween.rotateZ(go, rotation, duration),
                    _ => null
                };
            }
            public static LTDescr Rotate(GameObject go, Vector3 rotation, float duration, bool local = true)
            {
                return !local ? LeanTween.rotate(go, rotation, duration) : LeanTween.rotateLocal(go, rotation, duration);
            }
        #elif USING_DOTWEEN
            public static DG.Tweening.Tween Rotate(Axis axis, GameObject go, float rotation, float duration)
            {
                var eulerAngles = go.transform.eulerAngles;
                return axis switch
                {
                    Axis.X => go.transform.DORotate(eulerAngles + new Vector3(rotation, 0f, 0f), duration),
                    Axis.Y => go.transform.DORotate(eulerAngles + new Vector3(0f, rotation, 0f), duration),
                    Axis.Z => go.transform.DORotate(eulerAngles + new Vector3(0f, 0f, rotation), duration),
                    _ => null
                };
            }
            public static DG.Tweening.Tween Rotate(GameObject go, Vector3 rotation, float duration, bool local = true)
            {
                return !local ? go.transform.DORotate(go.transform.eulerAngles + rotation, duration) : go.transform.DOLocalRotate(go.transform.eulerAngles + rotation, duration);
            }
        #endif
        #endregion

        #region SCALE
        #if USING_LEANTWEEN
            public static LTDescr Scale(Axis axis, GameObject go, float scale, float duration)
            {
                return axis switch
                {
                    Axis.X => LeanTween.scaleX(go, scale, duration),
                    Axis.Y => LeanTween.scaleY(go, scale, duration),
                    Axis.Z => LeanTween.scaleZ(go, scale, duration),
                    _ => null
                };
            }
            public static LTDescr Scale(GameObject go, Vector3 scale, float duration)
            {
                return LeanTween.scale(go, scale, duration);
            }
        #elif USING_DOTWEEN
            public static DG.Tweening.Tween Scale(Axis axis, GameObject go, float scale, float duration)
            {
                return axis switch
                {
                    Axis.X => go.transform.DOScaleX(scale, duration),
                    Axis.Y => go.transform.DOScaleY(scale, duration),
                    Axis.Z => go.transform.DOScaleZ(scale, duration),
                    _ => null
                };
            }
            public static DG.Tweening.Tween Scale(GameObject go, Vector3 scale, float duration)
            {
                return go.transform.DOScale(scale, duration);
            }
        #endif
        #endregion
        
    }
}