using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public RectTransform container;
    public RectTransform bar;
    public float maximum = 1;

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
            return Mathf.Approximately(current, 0) == false;
        }
    }

    void SetPercent(float percent) {
        bar.sizeDelta = new Vector2(container.rect.width * percent, bar.sizeDelta.y);
    }
}
