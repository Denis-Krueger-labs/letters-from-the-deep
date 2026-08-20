namespace LettersFromTheDeep.Dialogue;

public class DialogueChoice
{
    public string Text { get; }
    public string? NextNodeId { get; }

    public DialogueChoice(string text, string? nextNodeId)
    {
        Text = text;
        NextNodeId = nextNodeId;
    }
}