
using UnityEditor;

[InitializeOnLoad]
public class GlobalConfig
{
    static GlobalConfig()
    {
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        PlayerSettings.Android.keystorePass = "Hoot8632*Games";
        PlayerSettings.Android.keyaliasName = "hotgames";
        PlayerSettings.Android.keyaliasPass = "Hoot8632*Games";
    }
}