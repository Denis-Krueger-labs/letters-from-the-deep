using System.Collections.Generic;

namespace LettersFromTheDeep.Dialogue;

public class Dialogue
{
    public string StartNodeId { get; }

    private readonly Dictionary<string, DialogueNode> _nodes;

    public Dialogue(
        string startNodeId,
        IEnumerable<DialogueNode> nodes)
    {
        StartNodeId = startNodeId;

        _nodes = new Dictionary<string, DialogueNode>();

        foreach (DialogueNode node in nodes)
        {
            _nodes.Add(node.Id, node);
        }
    }

    public DialogueNode GetNode(string nodeId)
    {
        if (!_nodes.TryGetValue(nodeId, out DialogueNode? node))
        {
            throw new KeyNotFoundException(
                $"Dialogue node '{nodeId}' does not exist.");
        }

        return node;
    }
}