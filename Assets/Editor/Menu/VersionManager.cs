using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;

public class VersionManager : IPostprocessBuildWithReport
{
    public int callbackOrder => 0; //?
    public static bool AutoIncrease = true;
    private const string AutoIncreaseMenuName = "Build/Auto Increase Build Version";
    static VersionManager()
    {
        AutoIncrease = EditorPrefs.GetBool(AutoIncreaseMenuName, true);
    }

    [MenuItem(AutoIncreaseMenuName, false, 1)]
    private static void SetAutoIncrease()
    {
        AutoIncrease = !AutoIncrease;
        EditorPrefs.SetBool(AutoIncreaseMenuName, AutoIncrease);
        Debug.Log("Auto Increase : " + AutoIncrease);
    }
    [MenuItem(AutoIncreaseMenuName, true)]
    private static bool SetAutoIncreaseValidate()
    {
        Menu.SetChecked(AutoIncreaseMenuName, AutoIncrease);
        return true;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    [MenuItem("Build/Check Current Version", false, 2)]
    private static void CheckCurrentVersion()
    {
        Debug.Log("Build v" + PlayerSettings.bundleVersion + "(" + PlayerSettings.Android.bundleVersionCode + ")");
    }

    [PostProcessBuild(1)]
    public void OnPostprocessBuild(BuildReport report)
    {
        if (AutoIncrease) IncreaseBuild();
    }

    [MenuItem("Build/Increase Major Version", false, 50)]
    private static void IncreaseMajor()
    {
        string[] lines = PlayerSettings.bundleVersion.Split('.');
        EditVersion(1, -int.Parse(lines[1]), -int.Parse(lines[2]));
    }
    [MenuItem("Build/Increase Minor Version", false, 51)]
    private static void IncreaseMinor()
    {
        string[] lines = PlayerSettings.bundleVersion.Split('.');
        EditVersion(0, 1, -int.Parse(lines[2]));
    }

    private static void IncreaseBuild()
    {
        EditVersion(0, 0, 1);
    }
    static void EditVersion(int majorIncr, int minorIncr, int buildIncr)
    {
        string[] lines = PlayerSettings.bundleVersion.Split('.');

        int MajorVersion = int.Parse(lines[0]) + majorIncr;
        int MinorVersion = int.Parse(lines[1]) + minorIncr;
        int Build = int.Parse(lines[2]) + buildIncr;

        PlayerSettings.bundleVersion = MajorVersion.ToString("0") + "." +
                                       MinorVersion.ToString("0") + "." +
                                       Build.ToString("0");
        PlayerSettings.Android.bundleVersionCode = MajorVersion * 10000 + MinorVersion * 1000 + Build;
        CheckCurrentVersion();
    }
}
