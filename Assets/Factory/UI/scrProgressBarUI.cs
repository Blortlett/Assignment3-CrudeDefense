using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fillImage; // Assign the fill Image in the Inspector
    private float maxProgress = 100f;
    private float currentProgress = 100f;

    public void SetProgress(float progress)
    {
        currentProgress = Mathf.Clamp(progress, 0, maxProgress);
        fillImage.fillAmount = currentProgress / maxProgress;
    }
}