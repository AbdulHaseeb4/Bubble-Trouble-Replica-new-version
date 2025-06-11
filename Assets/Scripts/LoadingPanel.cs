using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingSlider : MonoBehaviour
{
    public Slider slider;
    public Text loadingText;
    public float loadTime = 3f;

    void Start()
    {
        StartCoroutine(FillSlider());
    }

    IEnumerator FillSlider()
    {
        float time = 0f;

        while (time < loadTime)
        {
            time += Time.deltaTime;
            float percent = Mathf.Clamp01(time / loadTime) * 100f;

            slider.value = percent;
            loadingText.text = Mathf.RoundToInt(percent) + "%";

            yield return null;
        }

        loadingText.text = "100%";

        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<UIController>().ShowMainMenuAfterLoading();
    }
}
