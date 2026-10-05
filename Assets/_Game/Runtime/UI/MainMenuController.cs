using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Game")]

#if UNITY_EDITOR
    [SerializeField] private SceneAsset gameplayScene;
#endif

    [SerializeField, HideInInspector]
    private string gameplayScenePath;

    private void Start()
    {
        Time.timeScale = 1f;

        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (gameplayScene != null)
        {
            gameplayScenePath =
                AssetDatabase.GetAssetPath(gameplayScene);

            AddSceneToBuildList(gameplayScenePath);
        }
        else
        {
            gameplayScenePath = string.Empty;
        }

        // Also register the Main Menu scene itself.
        if (gameObject.scene.IsValid() &&
            !string.IsNullOrEmpty(gameObject.scene.path))
        {
            AddSceneToBuildList(gameObject.scene.path);
        }
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
            $"Added scene to shared build scene list: {scenePath}"
        );
    }
#endif

    public void StartGame()
    {
        if (string.IsNullOrEmpty(gameplayScenePath))
        {
            Debug.LogError(
                "No gameplay scene assigned to MainMenuController."
            );

            return;
        }

        SceneManager.LoadScene(gameplayScenePath);
    }

    public void OpenSettings()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}