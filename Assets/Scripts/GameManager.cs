using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float _Lives;
    public int _Money;
    [SerializeField] private TextMeshProUGUI _LivesText;
    [SerializeField] private TextMeshProUGUI _MoneyText;

    private void Awake()
    {
        updateLivesText();
        updateMoneyText();
    }

    public void updateLivesText()
    {
        _LivesText.SetText("Lives : " + _Lives);
        if (_Lives <= 0) Application.Quit();
    }

    public void updateMoneyText()
    {
        _MoneyText.SetText("Money : " + _Money);
    }
}
