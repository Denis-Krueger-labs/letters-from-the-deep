using TMPro;
using UnityEngine;

public class LanternTip : MonoBehaviour
{
    [Header("Tip")]
    [TextArea]
    [SerializeField] private string tipText;

    [Header("UI")]
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TMP_Text speechBubbleText;

    private void Start()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        ShowTip();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        HideTip();
    }

    private void ShowTip()
    {
        if (speechBubbleText != null)
        {
            speechBubbleText.text = tipText;
        }

        if (speechBubble != null)
        {
            speechBubble.SetActive(true);
        }
    }

    private void HideTip()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
    }
}