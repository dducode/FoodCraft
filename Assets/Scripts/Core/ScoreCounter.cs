using System;
using System.Collections.Generic;
using FoodCraft.Data;

namespace FoodCraft.Core {

    public static class ScoreCounter {

        private static readonly IngredientsScoreTable Table = Game.GetIngredientsTable();


        public static int CalculateTotalScore (Dictionary<IngredientType, int> dish) {
            float totalScore = 0;

            foreach ((IngredientType ingredientsType, int ingredientsCount) in dish) {
                float multiplier = CalculateMultiplier(ingredientsCount);
                totalScore += CalculateScore(ingredientsType, ingredientsCount, multiplier, Table);
            }

            return (int)totalScore;
        }


        private static float CalculateMultiplier (int ingredientsCount) {
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


        private static float CalculateScore (
            IngredientType ingredientsType, int ingredientsCount, float multiplier, IngredientsScoreTable scoreTable
        ) {
            if (ingredientsCount < 1)
                throw new ArgumentOutOfRangeException(nameof(ingredientsCount));
            if (multiplier < 1)
                throw new ArgumentOutOfRangeException(nameof(multiplier));

            float calculatedScore;

            switch (ingredientsType) {
                case IngredientType.Meat:
                    calculatedScore = scoreTable.meatScores;
                    break;
                case IngredientType.Onion:
                    calculatedScore = scoreTable.onionScores;
                    break;
                case IngredientType.Pepper:
                    calculatedScore = scoreTable.pepperScores;
                    break;
                case IngredientType.Carrot:
                    calculatedScore = scoreTable.carrotScores;
                    break;
                case IngredientType.Potato:
                    calculatedScore = scoreTable.potatoScores;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            calculatedScore *= multiplier * ingredientsCount;
            return calculatedScore;
        }

    }

}