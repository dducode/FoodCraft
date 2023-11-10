#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FoodCraft.Core {

    public static partial class Game {

        [MenuItem("Project/" + nameof(OpenDataFolder))]
        private static void OpenDataFolder () {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }


        [MenuItem("Project/" + nameof(CreateIngredientsJson))]
        private static void CreateIngredientsJson () {
            CreateStreamingAssetsFolderIfNotExist();
            File.WriteAllText(IngredientsFullPath, JsonUtility.ToJson(DefaultScoreTable));
            AssetDatabase.Refresh();
        }


        [MenuItem("Project/" + nameof(CreateIngredientsJson), true)]
        private static bool CreateIngredientsJsonValidate () {
            return !File.Exists(IngredientsFullPath);
        }


        private static void CreateStreamingAssetsFolderIfNotExist () {
            if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath))) {
                AssetDatabase.CreateFolder("Assets", "StreamingAssets");
                AssetDatabase.Refresh();
            }
        }

    }

}
#endif