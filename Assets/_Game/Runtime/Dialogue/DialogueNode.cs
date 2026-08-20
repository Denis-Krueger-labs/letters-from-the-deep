using System.Collections.Generic;

namespace LettersFromTheDeep.Dialogue;

public class DialogueNode
{
    public string Id { get; }
    public string Speaker { get; }
    public string Text { get; }
    public IReadOnlyList<DialogueChoice> Choices { get; }

    public DialogueNode(
        string id,
        string speaker,
        string text,
        IReadOnlyList<DialogueChoice> choices)
    {
        Id = id;
        Speaker = speaker;
        Text = text;
        Choices = choices;
    }
}