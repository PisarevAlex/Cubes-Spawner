using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpAnimation : MonoBehaviour
{
    public enum AnimationMode
    {
        AppearDisappear,
        IncreaseAccent,
    }

    [Header("Parameters")]
    [SerializeField] private AnimationMode animationMode;
    [SerializeField] private bool destroyOnDisable;
    [SerializeField, Range(0, 2)] private float animationTime;
    [SerializeField, Range(0, 1)] private float returnTime;
    [SerializeField, Range(1, 2)] private float extraScalePower;

    private Vector3 defaultScale;
    private Vector3 extraScale;
    private Coroutine currentAnimation;

    private void Awake()
    {
        defaultScale = transform.localScale;
        extraScale = defaultScale * extraScalePower;
    }

    private void OnEnable()
    {
        if (currentAnimation != null) StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(EnterAnimationRoutine());
    }

    private void OnDisable()
    {
        if (currentAnimation != null) StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(ExitAnimationRoutine());
    }

    private IEnumerator EnterAnimationRoutine()
    {
        for (float i = 0; i < animationTime; i += Time.deltaTime)
        {
            float progress = Mathf.InverseLerp(0, animationTime, i);

            if (animationMode == AnimationMode.AppearDisappear)
            {
                transform.localScale = Vector3.Lerp(Vector3.zero, extraScale, progress);
            }
            else
            {
                transform.localScale = Vector3.Lerp(defaultScale, extraScale, progress);
            }

            yield return null;
        }

        transform.localScale = extraScale;

        if (animationMode == AnimationMode.AppearDisappear)
        {
            if (extraScalePower > 0)
            {
                for (float i = 0; i < returnTime / 2; i += Time.deltaTime)
                {
                    float progress = Mathf.InverseLerp(0, returnTime / 2, i);

                    transform.localScale = Vector3.Lerp(extraScale, defaultScale, progress);
                    yield return null;
                }
            }

            transform.localScale = defaultScale;
        }
    }

    private IEnumerator ExitAnimationRoutine()
    {
        for (float i = 0; i < animationTime / 1.5f; i += Time.deltaTime)
        {
            float progress = Mathf.InverseLerp(0, animationTime / 1.5f, i);

            if (animationMode == AnimationMode.AppearDisappear)
            {
                transform.localScale = Vector3.Lerp(extraScale, Vector3.zero, progress);
            }
            else
            {
                transform.localScale = Vector3.Lerp(extraScale, defaultScale, progress);
            }
            yield return null;
        }

        if (animationMode == AnimationMode.AppearDisappear)
        {
            transform.localScale = Vector3.zero;
        }
        else transform.localScale = defaultScale;

        if (destroyOnDisable) Destroy(gameObject);
    }
}
