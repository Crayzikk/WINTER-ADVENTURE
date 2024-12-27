using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class MyEditor : AssetPostprocessor
{
    public static void OnPostprocessAllAssets(string[] importedAsset, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAsset)
        {
            CheckSceneSetting();
        }
    }

    static int cnt = 0;
    public static void CheckSceneSetting()
    {
        if (cnt > 0)
        {
            return;
        }

        List<string> dirs = new List<string>();
        GetDirs(Application.dataPath, ref dirs);
        EditorBuildSettingsScene[] newSettings = new EditorBuildSettingsScene[dirs.Count];
        for (int i = 0; i < newSettings.Length; i++)
        {
            newSettings[i] = new EditorBuildSettingsScene(dirs[i], true);
        }
        EditorBuildSettings.scenes = newSettings;
        AssetDatabase.SaveAssets();

        cnt++;
    }

    public static void GetDirs(string dirPath, ref List<string> dirs)
    {
        foreach (string path in Directory.GetFiles(dirPath))
        {
            if (Path.GetExtension(path) == ".unity")
            {
                // Нормалізація шляху
                string normalizedPath = path.Replace("\\", "/");
                int assetsIndex = normalizedPath.IndexOf("Assets/");
                if (assetsIndex >= 0)
                {
                    dirs.Add(normalizedPath.Substring(assetsIndex));
                }
                else
                {
                    Debug.LogWarning($"Не вдалося знайти 'Assets/' у шляху: {normalizedPath}");
                }
            }
        }

        foreach (string subDir in Directory.GetDirectories(dirPath))
        {
            GetDirs(subDir, ref dirs);
        }
    }
}
