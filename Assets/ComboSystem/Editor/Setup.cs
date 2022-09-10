using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace ComboSystem.Editor
{
#if UNITY_EDITOR
    public static class Memory { public static bool HasBeenSetup; }
    internal class Starter : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (Memory.HasBeenSetup) return;
            if (importedAssets.All(s => s.Substring(s.Length - 8) != "Setup.cs")) return;
            Setup.CheckTMP();
            Setup.ShowWindow();
            Memory.HasBeenSetup = true;
        }
    }
    public class Setup : EditorWindow
    {
        private enum TweeningPlugin { DoTween, LeanTween };

        private const string DO = "USING_DOTWEEN";
        private const string LT = "USING_LEANTWEEN";
        private const string TMP = "TEXTMESHPRO_INSTALLED";

        private static List<string> Definitions => PlayerSettings.GetScriptingDefineSymbolsForGroup
            (EditorUserBuildSettings.selectedBuildTargetGroup).Split(';').ToList();
        private static ListRequest request;

        [InitializeOnLoadMethod]
        public static void CheckTMP()
        {
            request = Client.List();
            EditorApplication.update += Progress;
        }
        private static void Progress()
        {
            if (request.IsCompleted)
            {
                if (request.Status == StatusCode.Success)
                {
                    var definitions = Definitions;
                    if (request.Result.Any(package => package.name == "com.unity.textmeshpro"))
                    {
                        if (!definitions.Contains(TMP)) definitions.Add(TMP);
                    }
                    else
                    {
                        if (definitions.Contains(TMP)) definitions.Remove(TMP);
                        Debug.LogWarning("Please Install TextMeshPro to use ComboSystem");
                    }
                    SaveSymbols(definitions);
                }
                else if (request.Status >= StatusCode.Failure)
                    Debug.LogError(request.Error.message);

                EditorApplication.update -= Progress;
            }
        }
        
        [MenuItem("Tools/Combo System/Tweening Setup")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<Setup>("ComboSystem Setup");
            window.minSize = new Vector2(400f, 220f);
            window.maxSize = new Vector2(400f, 220f);
        }
        private void OnGUI()
        {
            GUILayout.Label("");
            GUILayout.Label("Let's setup a Tweening Plugin to work with the ComboSystem\nYou can check current Define Symbols at:\nEdit/Project Settings/Player/Others Settings/Script Compilation", EditorStyles.boldLabel);
            GUILayout.Label("");
            if (GUILayout.Button("Install TextMeshPro"))
            {
                CheckTMP();
                UnityEditor.PackageManager.UI.Window.Open("com.unity.textmeshpro");
            }

            if (GUILayout.Button("Install DOTween"))
                Application.OpenURL("https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676");
            MakeButton(TweeningPlugin.DoTween, "DG.Tweening.DOTween,DOTween", DO,"aqua");
            if (GUILayout.Button("Install LeanTween"))
                Application.OpenURL("https://assetstore.unity.com/packages/tools/animation/leantween-3595");
            MakeButton(TweeningPlugin.LeanTween, "LeanTween", LT, "lime", "Assembly-CSharp");
            if (GUILayout.Button("Clear Define Symbols"))
            {
                SaveSymbols(ClearDefineSymbols());
                Debug.Log("USING_[TWEENINGPLUGIN] Symbols Removed from Scripting Definition Symbols!");
            }
        }
        private void MakeButton(TweeningPlugin plugin, string type, string symbol, string color, string assembly = null)
        {
            var pluginName = plugin.ToString();
            if (GUILayout.Button($"Use {pluginName}"))
            {
                if ((assembly == null ? Type.GetType(type) : Assembly.Load(assembly).GetType(type)) != null)
                {
                    SetSymbol(symbol);
                    Debug.LogFormat($"Using: <color={color}>{pluginName}</color>");
                    Debug.Log("Please wait for Script Recompilation. (See loading bar in lower right corner)");
                    Debug.Log("Please try restarting Unity if changes do not reflect in your IDE.");
                }
                else Debug.LogErrorFormat($"<color={color}>{pluginName}</color> is <color=red>NOT</color> found in this project. Please install it first");
            }
        }
        private static List<string> ClearDefineSymbols()
        {
            var definitions = Definitions;
            if (definitions.Contains(DO)) definitions.Remove(DO);
            if (definitions.Contains(LT)) definitions.Remove(LT);
            if (definitions.Contains(TMP)) definitions.Remove(TMP);
            return definitions;
        }
        private static void SetSymbol(string symbol)
        {
            var definitions = ClearDefineSymbols();
            definitions.Add(symbol);
            SaveSymbols(definitions);
        }
        private static void SaveSymbols(List<string> definitions)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", definitions.ToArray()));
        }
    }
#endif
}