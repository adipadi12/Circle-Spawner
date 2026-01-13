using UnityEngine;
using TMPro;
using DG.Tweening;

public class ClientPopupView : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text addressText;
    [SerializeField] private CanvasGroup canvasGroup;

    public void Show(MergedClientData data)
    {
        nameText.text = data.name;
        pointsText.text = data.points.ToString();
        addressText.text = data.address;

        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        transform.localScale = Vector3.one * 0.9f;

        canvasGroup.DOFade(1, 0.25f);
        transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        canvasGroup.DOFade(0, 0.2f);
        transform.DOScale(0.9f, 0.2f)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
