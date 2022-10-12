using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ParameterField : MonoBehaviour
{
    [field: SerializeField] public float Value { get; private set; }

    [Header("References")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private BumpAnimation bumpAnimation;

    [Header("Range")]
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    private void OnEnable()
    {
        inputField.onValueChanged.AddListener(delegate { RefreshValue(); });
        inputField.onSelect.AddListener(delegate { OnSelect(); });
        inputField.onDeselect.AddListener(delegate { OnDeselect(); });

        inputField.text = Value.ToString();
        bumpAnimation.enabled = false;
    }

    private void OnDisable()
    {
        inputField.onValueChanged.RemoveListener(delegate { RefreshValue(); });
        inputField.onSelect.RemoveListener(delegate { OnSelect(); });
        inputField.onDeselect.RemoveListener(delegate { OnDeselect(); });
    }

    private void RefreshValue()
    {
        if (float.TryParse(inputField.text, out float parsedValue))
        {
            Value = Mathf.Clamp(parsedValue, minValue, maxValue);
        }
        else
        {
            Value = Mathf.Clamp(0, minValue, maxValue);
        }
    }

    private void OnSelect()
    {
        bumpAnimation.enabled = true;
    }

    private void OnDeselect()
    {
        inputField.text = Value.ToString();
        bumpAnimation.enabled = false;
    }
}