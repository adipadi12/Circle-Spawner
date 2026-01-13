using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ClientListItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private Button button;

    private MergedClientData boundData;
    private Action<MergedClientData> onClick;

    public void Bind(MergedClientData data, Action<MergedClientData> clickCallback)
    {
        boundData = data;
        onClick = clickCallback;

        labelText.text = data.label;
        pointsText.text = data.points.ToString();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        onClick?.Invoke(boundData);
    }
}
