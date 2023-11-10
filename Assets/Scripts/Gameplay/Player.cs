using System;
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

        private IngredientsScoreTable m_scoreTable;
        private Dictionary<Rule, string> m_menu;


        public void Construct (Cauldron cauldron) {
            cauldron.OnPutIngredient += OnPutIngredient;
            cauldron.OnCook += OnCook;
        }


        private void Awake () {
            m_scoreTable = Game.GetIngredientsTable();
            m_menu = Game.GetIngredientsMenu();
        }


        private void Start () {
            LoadData();
        }


        private void Update () {
            if (Input.GetKey(KeyCode.R))
                Game.Restart();
            if (Input.GetKey(KeyCode.L))
                LoadData();
        }


        private void OnPutIngredient (Cauldron cauldron) {
            List<Ingredient> ingredients = cauldron.Ingredients;

            if (ingredients.Count == 5)
                cauldron.Cook(cookingTime);
        }


        private void OnCook (Dictionary<IngredientType, int> dish) {
            int totalScore = CalculateTotalScore(dish);
            score.Value += totalScore;

            lastDishData.Value = new DishData {
                dish = dish,
                score = totalScore,
                name = FindDishNameInMenu(dish)
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


        private string FindDishNameInMenu (Dictionary<IngredientType, int> dish) {
            foreach (KeyValuePair<IngredientType, int> ingredientsData in dish)
                foreach (KeyValuePair<Rule, string> menuRow in m_menu)
                    if (menuRow.Key == ingredientsData)
                        return menuRow.Value;

            return !dish.ContainsKey(IngredientType.Meat) ? "Овощное рагу" : "Суп";
        }


        private int CalculateTotalScore (Dictionary<IngredientType, int> dish) {
            float totalScore = 0;

            foreach ((IngredientType ingredientsType, int ingredientsCount) in dish) {
                float multiplier = CalculateMultiplier(ingredientsCount);
                totalScore += CalculateScore(ingredientsType, ingredientsCount, multiplier);
            }

            return (int)totalScore;
        }


        private float CalculateMultiplier (int ingredientsCount) {
            if (ingredientsCount < 1)
                throw new ArgumentOutOfRangeException(nameof(ingredientsCount));

            float multiplier;

            switch (ingredientsCount) {
                case 1 or 2:
                    multiplier = 2;
                    break;
                case 3:
                    multiplier = 1.5f;
                    break;
                case 4:
                    multiplier = 1.25f;
                    break;
                default:
                    multiplier = 1;
                    break;
            }

            return multiplier;
        }


        private float CalculateScore (IngredientType ingredientsType, int ingredientsCount, float multiplier) {
            if (ingredientsCount < 1)
                throw new ArgumentOutOfRangeException(nameof(ingredientsCount));
            if (multiplier < 1)
                throw new ArgumentOutOfRangeException(nameof(multiplier));

            float calculatedScore;

            switch (ingredientsType) {
                case IngredientType.Meat:
                    calculatedScore = m_scoreTable.meatScores;
                    break;
                case IngredientType.Onion:
                    calculatedScore = m_scoreTable.onionScores;
                    break;
                case IngredientType.Pepper:
                    calculatedScore = m_scoreTable.pepperScores;
                    break;
                case IngredientType.Carrot:
                    calculatedScore = m_scoreTable.carrotScores;
                    break;
                case IngredientType.Potato:
                    calculatedScore = m_scoreTable.potatoScores;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            calculatedScore *= multiplier * ingredientsCount;
            return calculatedScore;
        }

    }

}