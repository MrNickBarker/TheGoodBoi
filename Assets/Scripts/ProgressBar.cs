using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public RectTransform container;
    public RectTransform bar;
    public float maximum = 1;
    public bool requireFullToUse = false;

    float current = 1;
    public float Current {
        get {
            return current;
        }
        set {
            current = Mathf.Clamp(value, 0f, maximum);
            SetPercent(current / maximum);
        }
    }

    public bool CanUse {
        get {
            return requireFullToUse ? Mathf.Approximately(current, maximum) : Mathf.Approximately(current, 0) == false;
        }
    }

    void SetPercent(float percent) {
        bar.sizeDelta = new Vector2(container.rect.width * percent, bar.sizeDelta.y);
    }
}
