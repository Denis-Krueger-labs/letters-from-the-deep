using System;

namespace LettersFromTheDeep.Dialogue;

public class DialogueRunner
{
    private Dialogue? _dialogue;

    public DialogueNode? CurrentNode { get; private set; }

    public bool IsRunning => _dialogue is not null;

    public bool IsFinished => _dialogue is null;

    public void Start(Dialogue dialogue)
    {
        _dialogue = dialogue;
        CurrentNode = dialogue.GetNode(dialogue.StartNodeId);
    }

    public void Choose(int choiceIndex)
    {
        if (_dialogue is null || CurrentNode is null)
        {
            throw new InvalidOperationException(
                "No dialogue is currently running.");
        }

        if (choiceIndex < 0 || choiceIndex >= CurrentNode.Choices.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(choiceIndex),
                "Choice index is outside the available choices.");
        }

        DialogueChoice choice = CurrentNode.Choices[choiceIndex];

        if (choice.NextNodeId is null)
        {
            End();
            return;
        }

        CurrentNode = _dialogue.GetNode(choice.NextNodeId);
    }

    public void End()
    {
        _dialogue = null;
        CurrentNode = null;
    }
}