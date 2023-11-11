using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FoodCraft.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FoodCraft.Core {

    /// <summary>
    /// Base class for game management
    /// </summary>
    public static partial class Game {

        private static readonly string IngredientsFullPath =
            Path.Combine(Application.streamingAssetsPath, "ingredients.json");

        private static readonly string SaveDataPath =
            Path.Combine(Application.persistentDataPath, "data.bytes");

        private static readonly IngredientsScoreTable DefaultScoreTable =
            new(50, 40, 30, 20, 10);


        public static void Restart () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        public static void Exit () {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
        }


        public static IngredientsScoreTable GetIngredientsTable () {
            return JsonUtility.FromJson<IngredientsScoreTable>(File.ReadAllText(IngredientsFullPath));
        }


        public static void SaveData (Action<BinaryWriter> callback) {
            using var writer = new BinaryWriter(new FileStream(SaveDataPath, FileMode.Create));
            callback(writer);
        }


        public static void LoadData (Action<BinaryReader> callback) {
            if (!File.Exists(SaveDataPath))
                return;

            using var reader = new BinaryReader(new FileStream(SaveDataPath, FileMode.Open));
            callback(reader);
        }


        public static void GenerateAllCombinations () {
            int depth = -1;
            var ingredients = new IngredientType[5];
            var combinations = new List<DishData>(3125);

            // Generate all combinations recursively and write them into a list
            GenerateCombination(ref depth, ingredients, combinations);
            combinations.Sort((dishData1, dishData2) => dishData2.score - dishData1.score);

            // Remove all replays
            for (var i = 0; i < combinations.Count; i++) {
                for (int j = i + 1; j < combinations.Count - 1; j++) {
                    if (combinations[i] == combinations[j]) {
                        combinations.RemoveAt(j);
                        j--;
                    }
                }
            }

            // Write remaining entries to file
            WriteData(combinations);
        }


        private static void GenerateCombination (
            ref int depth, IngredientType[] ingredients, ICollection<DishData> list
        ) {
            const int maxTypesCount = 5;

            depth++;

            for (var i = 0; i < maxTypesCount; i++) {
                ingredients[depth] = (IngredientType)i;

                if (depth < maxTypesCount - 1) {
                    GenerateCombination(ref depth, ingredients, list);
                }
                else {
                    Dictionary<IngredientType, int> dish = CreateDish(ingredients);
                    list.Add(new DishData {
                        compound = dish,
                        score = ScoreCounter.CalculateTotalScore(dish),
                        name = Menu.FindDishName(dish)
                    });
                }
            }

            depth--;
        }


        private static Dictionary<IngredientType, int> CreateDish (IngredientType[] ingredients) {
            var ingredientsCopy = new IngredientType[ingredients.Length];
            Array.Copy(ingredients, ingredientsCopy, ingredients.Length);

            // Sort ingredients 
            for (var i = 0; i < ingredientsCopy.Length - 1; i++) {
                for (int j = i; j >= 0; j--) {
                    if (ingredientsCopy[j] > ingredientsCopy[j + 1])
                        (ingredientsCopy[j + 1], ingredientsCopy[j]) = (ingredientsCopy[j], ingredientsCopy[j + 1]);
                    else
                        break;
                }
            }

            var dish = new Dictionary<IngredientType, int>();

            foreach (IngredientType ingredientType in ingredientsCopy) {
                if (dish.ContainsKey(ingredientType))
                    dish[ingredientType]++;
                else
                    dish.Add(ingredientType, 1);
            }

            return dish;
        }


        private static void WriteData (List<DishData> combinations) {
            var dishText = new StringBuilder("All combinations: \n");
            var count = 0;

            foreach (DishData data in combinations) {
                var compoundText = new StringBuilder();

                // Write dish compound
                foreach (KeyValuePair<IngredientType, int> valuePair in data.compound)
                    compoundText.Append($"{valuePair.Value} {valuePair.Key}, ");

                compoundText.Remove(compoundText.Length - 2, 2);

                // Write dish in row
                dishText.Append($"{++count}. {data.name} ({compoundText}) [{data.score}]\n");
            }

            File.WriteAllText(Path.Combine(Application.persistentDataPath, "combinations.txt"), dishText.ToString());
        }

    }

}