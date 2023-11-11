using System.Collections.Generic;
using FoodCraft.Core;
using FoodCraft.Data;
using UnityEngine;

namespace FoodCraft.Gameplay {

    public class Player : MonoBehaviour {

        public ReactValue<int> score;
        public ReactValue<DishData> lastDishData;
        public ReactValue<DishData> bestDishData;

        [SerializeField]
        private float cookingTime = 2;


        /// <summary>
        /// Call this to pass dependencies to the class
        /// </summary>
        public void Construct (Cauldron cauldron) {
            cauldron.OnPutIngredient += OnPutIngredient;
            cauldron.OnCook += OnCook;
        }


        private void Start () {
            LoadData();
        }


        private void Update () {
            if (Input.GetKey(KeyCode.R))
                Game.Restart();
            if (Input.GetKey(KeyCode.Escape))
                Game.Exit();
            if (Input.GetKey(KeyCode.L))
                LoadData();
            if (Input.GetKey(KeyCode.T))
                Game.GenerateAllCombinations();
        }


        private void OnPutIngredient (Cauldron cauldron) {
            List<Ingredient> ingredients = cauldron.Ingredients;

            if (ingredients.Count == 5)
                cauldron.Cook(cookingTime);
        }


        private void OnCook (Dictionary<IngredientType, int> dish) {
            int totalScore = ScoreCounter.CalculateTotalScore(dish);
            score.Value += totalScore;

            lastDishData.Value = new DishData {
                compound = dish,
                score = totalScore,
                name = Menu.FindDishName(dish)
            };

            if (lastDishData.Value.score > bestDishData.Value.score)
                bestDishData.Value = lastDishData.Value;

            SaveData();
        }


        private void SaveData () {
            Game.SaveData(writer => {
                writer.Write(score.Value);
                writer.Write(lastDishData.Value);
                writer.Write(bestDishData.Value);
            });
        }


        private void LoadData () {
            Game.LoadData(reader => {
                score.Value = reader.ReadInt32();
                lastDishData.Value = reader.ReadDishData();
                bestDishData.Value = reader.ReadDishData();
            });
        }

    }

}