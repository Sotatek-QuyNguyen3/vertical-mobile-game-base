using Hellmade.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;
using Lean.Pool;
using UnityEngine.Serialization;

public class FxCurrency : MonoBehaviour
{
    // Singleton
    static FxCurrency _Instance;

    // UI
    [Header("Configs")] [SerializeField, Range(5, 20)]
    int maxObjects = 5;

    private int _quantityObject = 20;
    [Header("References")] public GameObject preGold;
    public GameObject preStar;
    public GameObject preExp;
    public GameObject preStamina;
    public GameObject preFurniture;

    //[Header("UI")]
    //public GameObject nodeMove;
    //public GameObject nodeFinish;

    [HideInInspector] public static bool isShow;

    RewardType _type;
    Vector3 _target;
    Transform _targetTransform;
    double _value;
    int _count;
    double _max;
    bool _updateTextAnim;

    Action _onCollideFx;
    Action _onFinish;
    Vector3 _vectorShow;

    public static void Show(Vector3 VectorStart, double value, Vector3 to, Action onFinish = null,
        Action onCollideFx = null, Transform targetTransfom = null, RewardType type = RewardType.Gold,
        int quantityObject = 20)
    {
        _Instance = Instantiate(
            Resources.Load<FxCurrency>("Prefabs/Effects/FX Currency"),
            Popups.CanvasPopup4.transform,
            false);
        _Instance._quantityObject = quantityObject;
        _Instance._vectorShow = VectorStart;
        _Instance._onCollideFx = onCollideFx;
        _Instance._onFinish = onFinish;
        _Instance._target = to;
        _Instance._value = value;
        _Instance._type = type;
        // GameMainUI.Instance.StartAnimClaim(type);
        _Instance.Animate(targetTransfom);
        Hellmade.Sound.EazySoundManager.PlaySound(Sounds.Instance.Sfx_Collect_Claim);
    }

    private void CheckOnFinish()
    {
        if (_onFinish != null)
        {
            _onFinish.Invoke();
            _onFinish = null;
        }
    }

    public static void ShowMid(double value, Vector3 to, Action onFinish = null, Action onCollideFx = null)
    {
        if (_Instance == null)
        {
            _Instance = Instantiate(
                Resources.Load<FxCurrency>("Prefabs/Effects/FX Currency"),
                Popups.CanvasPopup4.transform,
                false);
        }

        _Instance._onCollideFx = onCollideFx;
        _Instance._onFinish = onFinish;
        _Instance._target = to;
        _Instance._value = value;
        _Instance.Animate(null);
    }

    public static void SetStartPos(Vector3 vector)
    {
        if (_Instance == null)
        {
            _Instance = Instantiate(
                Resources.Load<FxCurrency>("Prefabs/Effects/FX Currency"),
                Popups.CanvasPopup4.transform,
                false);
        }

        _Instance._vectorShow = vector;
    }

    void Animate(Transform targetTransform)
    {
        if (_value > 0)
        {
            _updateTextAnim = false;
            StartCoroutine(RunAnimation(targetTransform));
        }
        else
        {
            // GameMainUI.Instance.StartAnimClaim(_type);
            if (_onFinish != null)
                _onFinish.Invoke();
        }
    }

    IEnumerator RunAnimation(Transform targetTransform)
    {
        //
        //GameStatics.IsAnimating = true;
        isShow = true;

        _max = _quantityObject;

        _count = 0;
        //EazySoundManager.PlaySound(Sounds.Instance.Ready_Collect_Coin);

        while (_count < _max)
        {
            GameObject go = null;
            switch (_type)
            {
                case RewardType.Gold:
                    go = LeanPool.Spawn(preGold, _vectorShow, UnityEngine.Quaternion.Euler(0, 0, 0), transform);
                    go.transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
                    break;
                case RewardType.Star: // Bur
                    go = LeanPool.Spawn(preStar, _vectorShow, UnityEngine.Quaternion.Euler(0, 0, 0), transform);
                    go.transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
                    break;
                case RewardType.Exp:
                    go = LeanPool.Spawn(preExp, _vectorShow, UnityEngine.Quaternion.Euler(0, 0, 0), transform);
                    go.transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
                    break;
                default:
                    go = LeanPool.Spawn(preGold, _vectorShow, UnityEngine.Quaternion.Euler(0, 0, 0), transform);
                    go.transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
                    break;
            }

            if (targetTransform != null)
            {
                RunByTransform(go, targetTransform, _count);
            }
            else
            {
                Run(go, _count);
            }

            _count++;
            yield return new WaitForEndOfFrame();
        }

        //GameStatics.IsAnimating = false;
        isShow = false;
        yield return null;
    }


    void RunByTransform(GameObject obj, Transform targetTransform, int num)
    {
        float originScale = obj.transform.localScale.x;

        LeanTween.alpha(obj.GetComponent<RectTransform>(), 1, 0.15f);
        LeanTween.scaleX(obj, originScale, 0.15f);
        LeanTween.moveLocal(obj,
            new Vector3(
                UnityEngine.Random.Range(obj.transform.localPosition.x - 200, obj.transform.localPosition.x + 200),
                UnityEngine.Random.Range(obj.transform.localPosition.y - 200, obj.transform.localPosition.y + 200f),
                0),
            0.15f).setOnComplete(() =>
        {
            Vector3 startPos = obj.transform.position;
            LeanTween.delayedCall(UnityEngine.Random.Range(0.1f, 1f), () =>
            {
                if (obj != null)
                    LeanTween.value(obj, 0, 1, UnityEngine.Random.Range(0.4f, 0.7f)).setOnUpdate((float value) =>
                    {
                        obj.transform.position = Vector3.Lerp(startPos, targetTransform.position, value);
                    }).setEaseInSine().setOnComplete(() =>
                    {
                        if (obj != null)
                        {
                            LeanPool.Despawn(obj);
                            //Destroy(obj);

                            //
                            if (_onCollideFx != null)
                                _onCollideFx.Invoke();

                            if (num >= _max - 1) CheckOnFinish();
                            // Sfx
                            //FxManager.ShowEffect(SFX.Currency_Collide, _target, 1);
                        }
                    });
            });
        });
    }

    void Run(GameObject obj, int num)
    {
        if (obj != null)
        {
            float originScale = obj.transform.localScale.x;

            obj.SetActive(true);
            obj.transform.localScale = new Vector3(originScale, originScale, originScale);
            //  obj.transform.localPosition = Vector3.zero;

            LeanTween.alpha(obj.GetComponent<RectTransform>(), 1, 0.15f);
            LeanTween.scaleX(obj, originScale, 0.15f);

            LeanTween.moveLocal(obj,
                new Vector3(
                    UnityEngine.Random.Range(obj.transform.localPosition.x - 200, obj.transform.localPosition.x + 200),
                    UnityEngine.Random.Range(obj.transform.localPosition.y - 200, obj.transform.localPosition.y + 200f),
                    0),
                0.15f).setOnComplete(() =>
            {
                LeanTween.delayedCall(0.2f, () =>
                {
                    if (obj != null)
                    {
                        //LeanTween.rotateY(obj, -1079f, 0.9f);

                        LeanTween.move(obj, _target, 0.9f).setEaseInQuart().setOnComplete(() =>
                        {
                            if (!_updateTextAnim)
                            {
                                switch (_type)
                                {
                                    case RewardType.Gold:
                                        //  DataGameSave.data.Coin += (int)_value;
                                        break;
                                }

                                _updateTextAnim = true;
                                // Push data to firebase
                                //Debug.Log("PUSH DATA TO FIREBASE");
                                //SyncManager.Instance.Push();
                                if (num >= _max - 1) CheckOnFinish();
                            }

                            if (obj != null)
                            {
                                LeanPool.Despawn(obj);
                                //Destroy(obj);

                                //
                                if (_onCollideFx != null)
                                    _onCollideFx.Invoke();

                                // Sfx
                                //FxManager.ShowEffect(SFX.Currency_Collide, _target, 1);
                            }

                            // release
                            if (_count == _max)
                            {
                                LeanTween.delayedCall(0.75f, () =>
                                {
                                    //GameStatics.IsAnimating = false;
                                    isShow = false;

                                    //if (/*GameStatics.IsFxCoinShowAd && */AdManager.Instance.IsInterstitialReady)
                                    //{
                                    //GameStatics.IsFxCoinShowAd = false;
                                    //    AdManager.Instance.ShowInterstitial(_onFinish);
                                    //}
                                    //else
                                    //{

                                    //}
                                });
                            }

                            // Sound
                            isSoundCoin++;
                            if (isSoundCoin == 5)
                            {
                                isSoundCoin = 0;
                            }

                            // EazySoundManager.PlaySound(Sounds.Instance.Sfx_Coin,.2f);
                            //UIManager.Instance.EffectCoin();
                        }).setEase(LeanTweenType.easeInQuad);
                    }
                });
            });
        }
    }

    int isSoundCoin = 0;

    void Scale(GameObject obj)
    {
        //EazySoundManager.PlaySound(Sounds.Instance.FX_Coin);
        if (obj != null)
        {
            LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.15f).setOnComplete(() =>
            {
                if (obj != null)
                {
                    LeanTween.scale(gameObject, Vector3.one, 0.15f).setOnComplete(() =>
                    {
                        if (!_updateTextAnim)
                        {
                            switch (_type)
                            {
                                case RewardType.Gold:
                                    // DataGameSave.data.Coin += (int)_value;
                                    break;
                            }

                            _updateTextAnim = true;
                            // Push data to firebase
                            //Debug.Log("PUSH DATA TO FIREBASE");
                            //SyncManager.Instance.Push();
                            CheckOnFinish();
                        }

                        if (obj != null)
                        {
                            LeanPool.Despawn(obj);
                            //Destroy(obj);
                        }

                        // release
                        if (_count == _max)
                        {
                            // GameStatics.IsAnimating = false;
                            isShow = false;
                        }
                    });
                }
            });
        }
    }
}