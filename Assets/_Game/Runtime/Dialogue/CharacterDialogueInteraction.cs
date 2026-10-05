using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LettersFromTheDeep.Dialogue;

public class CharacterDialogueInteraction : MonoBehaviour
{
    private enum SpeakerSide
    {
        Left,
        Right,
        None
    }

    [System.Serializable]
    private class DialogueLine
    {
        [Header("Dialogue")]
        public string speakerName;

        [TextArea]
        public string text;

        [Header("Speaker")]
        public SpeakerSide speakerSide = SpeakerSide.Left;

        [Header("Expressions")]
        [Tooltip("Leave empty to keep the current left expression.")]
        public Sprite leftExpression;

        [Tooltip("Leave empty to keep the current right expression.")]
        public Sprite rightExpression;
    }

    [Header("Interaction")]
    [SerializeField] private GameObject actionPrompt;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueView;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Portraits")]
    [SerializeField] private Image leftPortrait;
    [SerializeField] private Image rightPortrait;

    [Header("Portrait Brightness")]
    [Range(0f, 1f)]
    [SerializeField] private float inactiveBrightness = 0.45f;

    [Header("Dialogue")]
    [SerializeField] private List<DialogueLine> lines = new List<DialogueLine>();

    [Header("Game")]
    [SerializeField] private bool pauseGameDuringDialogue = true;

    private readonly DialogueRunner dialogueRunner = new DialogueRunner();

    private bool playerInRange;
    private bool dialogueActive;

    private int currentLineIndex;

    private float previousTimeScale = 1f;

    private void Start()
    {
        if (actionPrompt != null)
        {
            actionPrompt.SetActive(false);
        }

        if (dialogueView != null)
        {
            dialogueView.SetActive(false);
        }
    }

    private void Update()
    {
        if (!playerInRange)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        if (!dialogueActive)
        {
            StartDialogue();
        }
        else
        {
            ContinueDialogue();
        }
    }

    private void StartDialogue()
    {
        if (lines.Count == 0)
        {
            Debug.LogWarning("No dialogue lines configured.");
            return;
        }

        List<DialogueNode> nodes = new List<DialogueNode>();

        for (int i = 0; i < lines.Count; i++)
        {
            string id = $"line_{i}";

            string nextId = i < lines.Count - 1
                ? $"line_{i + 1}"
                : null;

            DialogueChoice continueChoice =
                new DialogueChoice("Continue", nextId);

            DialogueLine line = lines[i];

            nodes.Add(
                new DialogueNode(
                    id,
                    line.speakerName,
                    line.text,
                    new List<DialogueChoice>
                    {
                        continueChoice
                    }
                )
            );
        }

        Dialogue dialogue = new Dialogue("line_0", nodes);

        dialogueRunner.Start(dialogue);

        currentLineIndex = 0;
        dialogueActive = true;

        if (pauseGameDuringDialogue)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }

        if (actionPrompt != null)
        {
            actionPrompt.SetActive(false);
        }

        if (dialogueView != null)
        {
            dialogueView.SetActive(true);
        }

        RefreshDialogueUI();
    }

    private void ContinueDialogue()
    {
        dialogueRunner.Choose(0);

        if (dialogueRunner.IsFinished)
        {
            EndDialogue();
            return;
        }

        currentLineIndex++;

        RefreshDialogueUI();
    }

    private void RefreshDialogueUI()
    {
        DialogueNode node = dialogueRunner.CurrentNode;

        if (node == null)
        {
            return;
        }

        if (currentLineIndex < 0 || currentLineIndex >= lines.Count)
        {
            return;
        }

        DialogueLine line = lines[currentLineIndex];

        if (speakerNameText != null)
        {
            speakerNameText.text = node.Speaker;
        }

        if (dialogueText != null)
        {
            dialogueText.text = node.Text;
        }

        UpdateExpressions(line);
        UpdatePortraitBrightness(line.speakerSide);
    }

    private void UpdateExpressions(DialogueLine line)
    {
        if (leftPortrait != null && line.leftExpression != null)
        {
            leftPortrait.sprite = line.leftExpression;
            leftPortrait.enabled = true;
            leftPortrait.preserveAspect = true;
        }

        if (rightPortrait != null && line.rightExpression != null)
        {
            rightPortrait.sprite = line.rightExpression;
            rightPortrait.enabled = true;
            rightPortrait.preserveAspect = true;
        }
    }

    private void UpdatePortraitBrightness(SpeakerSide speakerSide)
    {
        Color activeColor = Color.white;

        Color inactiveColor = new Color(
            inactiveBrightness,
            inactiveBrightness,
            inactiveBrightness,
            1f
        );

        switch (speakerSide)
        {
            case SpeakerSide.Left:
                SetPortraitColor(leftPortrait, activeColor);
                SetPortraitColor(rightPortrait, inactiveColor);
                break;

            case SpeakerSide.Right:
                SetPortraitColor(leftPortrait, inactiveColor);
                SetPortraitColor(rightPortrait, activeColor);
                break;

            case SpeakerSide.None:
                SetPortraitColor(leftPortrait, activeColor);
                SetPortraitColor(rightPortrait, activeColor);
                break;
        }
    }

    private void SetPortraitColor(Image portrait, Color color)
    {
        if (portrait != null)
        {
            portrait.color = color;
        }
    }

    private void EndDialogue()
    {
        dialogueRunner.End();

        dialogueActive = false;

        if (pauseGameDuringDialogue)
        {
            Time.timeScale = previousTimeScale;
        }

        if (dialogueView != null)
        {
            dialogueView.SetActive(false);
        }

        if (actionPrompt != null && playerInRange)
        {
            actionPrompt.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.root.CompareTag("Player"))
        {
            return;
        }

        playerInRange = true;

        if (!dialogueActive && actionPrompt != null)
        {
            actionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.root.CompareTag("Player"))
        {
            return;
        }

        playerInRange = false;

        if (actionPrompt != null)
        {
            actionPrompt.SetActive(false);
        }

        if (dialogueActive)
        {
            EndDialogue();
        }
    }

    private void OnDisable()
    {
        if (dialogueActive && pauseGameDuringDialogue)
        {
            Time.timeScale = previousTimeScale;
        }
    }
}