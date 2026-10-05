using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelExit : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private GameObject actionPrompt;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [Header("Next Level")]

#if UNITY_EDITOR
    [SerializeField] private SceneAsset nextScene;
#endif

    [SerializeField, HideInInspector]
    private string nextScenePath;

    [Header("Player")]
    [SerializeField] private string playerTag = "Player";

    private bool playerInRange;
    private bool isLoading;

    private void Start()
    {
        if (actionPrompt != null)
        {
            actionPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (!playerInRange || isLoading)
        {
            return;
        }

        if (Input.GetKeyDown(interactionKey))
        {
            LoadNextLevel();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        nextScenePath = nextScene != null
            ? AssetDatabase.GetAssetPath(nextScene)
            : string.Empty;

        AddSceneToBuildList(nextScenePath);
    }

    private void AddSceneToBuildList(string scenePath)
    {
        if (string.IsNullOrEmpty(scenePath))
        {
            return;
        }

        EditorBuildSettingsScene[] currentScenes =
            EditorBuildSettings.scenes;

        foreach (EditorBuildSettingsScene scene in currentScenes)
        {
            if (scene.path == scenePath)
            {
                return;
            }
        }

        List<EditorBuildSettingsScene> scenes =
            new List<EditorBuildSettingsScene>(currentScenes);

        scenes.Add(
            new EditorBuildSettingsScene(
                scenePath,
                true
            )
        );

        EditorBuildSettings.scenes = scenes.ToArray();

        Debug.Log(
            $"Added next level to build scene list: {scenePath}"
        );
    }
#endif

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.root.CompareTag(playerTag))
        {
            return;
        }

        playerInRange = true;

        if (actionPrompt != null)
        {
            actionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.root.CompareTag(playerTag))
        {
            return;
        }

        playerInRange = false;

        if (actionPrompt != null)
        {
            actionPrompt.SetActive(false);
        }
    }

    private void LoadNextLevel()
    {
        if (string.IsNullOrEmpty(nextScenePath))
        {
            Debug.LogError(
                $"No next scene assigned to {gameObject.name}."
            );

            return;
        }

        isLoading = true;

        if (actionPrompt != null)
        {
            actionPrompt.SetActive(false);
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(nextScenePath);
    }
}