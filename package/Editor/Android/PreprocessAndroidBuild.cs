using System.IO;
using System.Collections.Generic;
using System;
using System.Reflection;
using Facebook.Unity.Editor;
using GooglePlayServices;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;



[InitializeOnLoad]
public class StartupFixMacOsM1
{
    class SdkSettings
    {
        public string PathInUnity;
        public string[] Binary;
        public string EditorPrefsKey;

    }
    static StartupFixMacOsM1()
    {
		#if UNITY_EDITOR_OSX
        var sdks = new Dictionary<string, SdkSettings>()
        {
            {
                "JAVA_HOME",
                new SdkSettings{
                
                    PathInUnity="PlaybackEngines/AndroidPlayer/OpenJDK/",
                    Binary= new string[]{"bin","java"}
                }
            },
            {
                "ANDROID_SDK_HOME",
            	new SdkSettings{
                
                    PathInUnity="PlaybackEngines/AndroidPlayer/SDK",
                    Binary= new string[]{"platform-tools","adb"},
                    EditorPrefsKey="AndroidSdkRoot"
                }
            }
        };


        Debug.Log("Up and running");
        // hack for m1 mac builds, since it tends to loose JAVA_HOME for some reason 

        foreach (var sdk in sdks)
        {
            var sdkHome = Environment.GetEnvironmentVariable(sdk.Key);
            if (String.IsNullOrEmpty(sdkHome) || !Directory.Exists(sdkHome))
            {
                sdkHome = EditorApplication.applicationContentsPath.Replace("Unity.app/Contents",
                    sdk.Value.PathInUnity);
                // check if we have SDK exisits 
                string checkPath = Path.Combine(sdkHome, Path.Combine(sdk.Value.Binary));
                if (File.Exists(checkPath))
                {
                    Environment.SetEnvironmentVariable(sdk.Key, sdkHome);
					if(!String.IsNullOrEmpty(sdk.Value.EditorPrefsKey))
					{
						EditorPrefs.SetString(sdk.Value.EditorPrefsKey,sdkHome);
					}
                }
                else
                {
                    Debug.LogError(
                        "Please install Android SDK & NDK tools with UnityHub https://docs.unity3d.com/Manual/android-sdksetup.html");
                }
            }
        }
		#endif

    }
}
namespace Robusta.Editor.Android
{
    public class PreprocessAndroidBuild : IPreprocessBuildWithReport
    {
        private static readonly string PackageName = "ex.unity.robusta";

        public static readonly string SourceFolderPath = Path.Combine("Packages", PackageName);

        public int callbackOrder => 1;

        public void OnPreprocessBuild(BuildReport report)
        {


            if (report.summary.platform != BuildTarget.Android)
            {
                return;
            }

            PrepareResolver();
            PreparePlayerSettings();
        }

        private static void PreparePlayerSettings()
        {
            // Set Android ARM64/ARMv7 Architecture
            PlayerSettings.SetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup,
                ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
            // Set Android min version
            if (PlayerSettings.Android.minSdkVersion < AndroidSdkVersions.AndroidApiLevel19)
            {
                PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;
            }
        }

        private static void PrepareResolver()
        {
            // Force playServices Resolver
            PlayServicesResolver.Resolve(null, true);

            // For some bizarre reason GooglePlayServices has most settings marked internal. Oh well...
            typeof(SettingsDialog).GetProperty("EnableAutoResolution", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, true);

            typeof(SettingsDialog).GetProperty("AutoResolveOnBuild", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, true);

            typeof(SettingsDialog).GetProperty("InstallAndroidPackages", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, true);

            typeof(SettingsDialog).GetProperty("PatchMainTemplateGradle", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, true);

            typeof(SettingsDialog).GetProperty("UseJetifier", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, true);

            typeof(SettingsDialog).GetProperty("VerboseLogging", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, true);
        }
    }
}