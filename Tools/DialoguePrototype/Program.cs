using LettersFromTheDeep.Dialogue;

var dialogue = new Dialogue(
    "start",
    new[]
    {
        new DialogueNode(
            "start",
            "Dockmaster",
            "Did you bring the package?",
            new[]
            {
                new DialogueChoice("Yes.", "has_package"),
                new DialogueChoice("No.", "no_package")
            }),

        new DialogueNode(
            "has_package",
            "Dockmaster",
            "Good. Hand it over.",
            new[]
            {
                new DialogueChoice("Here you go.", null)
            }),

        new DialogueNode(
            "no_package",
            "Dockmaster",
            "Then why did you come all the way down here?",
            new[]
            {
                new DialogueChoice("I wanted to talk.", "wanted_to_talk"),
                new DialogueChoice("Good question.", null)
            }),

        new DialogueNode(
            "wanted_to_talk",
            "Dockmaster",
            "...That's new.",
            new[]
            {
                new DialogueChoice("Is that a problem?", null),
                new DialogueChoice("Never mind.", null)
            })
    });

var runner = new DialogueRunner();

runner.Start(dialogue);

while (runner.IsRunning)
{
    DialogueNode node = runner.CurrentNode!;

    Console.WriteLine();
    Console.WriteLine($"{node.Speaker}:");
    Console.WriteLine(node.Text);
    Console.WriteLine();

    for (int i = 0; i < node.Choices.Count; i++)
    {
        Console.WriteLine($"[{i + 1}] {node.Choices[i].Text}");
    }

    int selectedChoice = ReadChoice(node.Choices.Count);

    runner.Choose(selectedChoice);
}

Console.WriteLine();
Console.WriteLine("[Dialogue finished]");

static int ReadChoice(int choiceCount)
{
    while (true)
    {
        Console.Write("> ");

        string? input = Console.ReadLine();

        if (int.TryParse(input, out int choice))
        {
            int index = choice - 1;

            if (index >= 0 && index < choiceCount)
            {
                return index;
            }
        }

        Console.WriteLine(
            $"Please enter a number between 1 and {choiceCount}.");
    }
}