using TMPro;
using UnityEngine;

public class FinalPanel : MonoBehaviour
{
    public TextMeshProUGUI dinero1Text;
    public TextMeshProUGUI dinero2Text;

    public TextMeshProUGUI winnerText;

    public void SetWinnerText(string text)
    {
        winnerText.text = text;
    }
    public void SetMoneyText(string text)
    {
        dinero1Text.text = text;
    }
    public void SetMoneyTexts(string leftText, string rightText)
    {
        dinero1Text.text = leftText;
        dinero2Text.text = rightText;
    }
}