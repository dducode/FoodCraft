using System;
using System.Collections.Generic;
using System.IO;
using FoodCraft.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FoodCraft.Core {

    public static partial class Game {

        private static readonly string IngredientsFullPath =
            Path.Combine(Application.streamingAssetsPath, "ingredients.json");

        private static readonly string SaveDataPath =
            Path.Combine(Application.persistentDataPath, "data.bytes");

        private static readonly IngredientsScoreTable DefaultScoreTable =
            new(50, 40, 30, 20, 10);

        private static readonly Dictionary<Rule, string> DefaultMenu = new() {
            {
                new Rule {
                    ingredientsType = IngredientType.Meat,
                    ingredientsCount = new Range(5, 5)
                },
                "Мясо в собственном соку"
            }, {
                new Rule {
                    ingredientsType = IngredientType.Meat,
                    ingredientsCount = new Range(4, 4)
                },
                "Мясо с гарниром"
            }, {
                new Rule {
                    ingredientsType = IngredientType.Meat,
                    ingredientsCount = new Range(2, 3)
                },
                "Рагу"
            }, {
                new Rule {
                    ingredientsType = IngredientType.Onion,
                    ingredientsCount = new Range(4, 5)
                },
                "Луковый суп"
            }, {
                new Rule {
                    ingredientsType = IngredientType.Potato,
                    ingredientsCount = new Range(4, 5)
                },
                "Картофельное пюре"
            }
        };


        public static void Restart () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        public static IngredientsScoreTable GetIngredientsTable () {
            return JsonUtility.FromJson<IngredientsScoreTable>(File.ReadAllText(IngredientsFullPath));
        }


        public static Dictionary<Rule, string> GetIngredientsMenu () {
            return DefaultMenu;
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

    }

}