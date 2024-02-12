using UnityEditor;
using UnityEngine;

public class AppMenu {
    static string packageFile = "Unity 2-02.unitypackage";

    [MenuItem ("Main Menu/Export Backup", false, 0)]
    static void action01 () {
        string[] exportpaths = new string[] {
            "Assets/Animations",
            "Assets/Editor",
            "Assets/Fonts",
            "Assets/Resources",
            "Assets/Scenes",
            "Assets/Scripts",
            "Assets/Sprites",
            "ProjectSettings/TagManager.asset",
            "ProjectSettings/EditorBuildSettings.asset"
        };

        AssetDatabase.ExportPackage (exportpaths, packageFile,
            ExportPackageOptions.Interactive |
            ExportPackageOptions.Recurse |
            ExportPackageOptions.IncludeDependencies
        );
        Debug.Log ("Backup Export Complete!");
    }

    [MenuItem ("Main Menu/Import Backup", false, 1)]
    static void action02 () {
        AssetDatabase.ImportPackage (packageFile, true);
    }
}