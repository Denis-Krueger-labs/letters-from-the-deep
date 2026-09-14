using LettersFromTheDeep.Dialogue;

namespace DialoguePrototype.Tests;

public class DialogueRunnerTests
{
    [Test]
    public void Start_SetsCurrentNodeToStartNode()
    {
        Dialogue dialogue = CreateDialogue();
        DialogueRunner runner = new();

        runner.Start(dialogue);

        Assert.That(runner.IsRunning, Is.True);
        Assert.That(runner.CurrentNode, Is.Not.Null);
        Assert.That(runner.CurrentNode!.Id, Is.EqualTo("start"));
    }

    [Test]
    public void Choose_FollowsSelectedBranch()
    {
        Dialogue dialogue = CreateDialogue();
        DialogueRunner runner = new();

        runner.Start(dialogue);
        runner.Choose(1);

        Assert.That(runner.CurrentNode, Is.Not.Null);
        Assert.That(runner.CurrentNode!.Id, Is.EqualTo("no"));
    }

    [Test]
    public void Choose_WithEndingChoice_FinishesDialogue()
    {
        Dialogue dialogue = CreateDialogue();
        DialogueRunner runner = new();

        runner.Start(dialogue);

        runner.Choose(0);
        runner.Choose(0);

        Assert.That(runner.IsFinished, Is.True);
        Assert.That(runner.CurrentNode, Is.Null);
    }

    [Test]
    public void Choose_WithInvalidIndex_Throws()
    {
        Dialogue dialogue = CreateDialogue();
        DialogueRunner runner = new();

        runner.Start(dialogue);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => runner.Choose(99));
    }

    private static Dialogue CreateDialogue()
    {
        return new Dialogue(
            "start",
            new[]
            {
                new DialogueNode(
                    "start",
                    "Dockmaster",
                    "Did you bring the package?",
                    new[]
                    {
                        new DialogueChoice("Yes.", "yes"),
                        new DialogueChoice("No.", "no")
                    }),

                new DialogueNode(
                    "yes",
                    "Dockmaster",
                    "Good.",
                    new[]
                    {
                        new DialogueChoice("Continue.", null)
                    }),

                new DialogueNode(
                    "no",
                    "Dockmaster",
                    "That's unfortunate.",
                    new[]
                    {
                        new DialogueChoice("Sorry.", null)
                    })
            });
    }
}