using System;

namespace FoodCraft.Data {

    /// <summary>
    /// Contains the value of each ingredient
    /// </summary>
    [Serializable]
    public struct IngredientsScoreTable {

        public int meatScores;
        public int onionScores;
        public int pepperScores;
        public int carrotScores;
        public int potatoScores;


        public IngredientsScoreTable (
            int meatScores,
            int onionScores,
            int pepperScores,
            int carrotScores,
            int potatoScores
        ) {
            this.meatScores = meatScores;
            this.onionScores = onionScores;
            this.pepperScores = pepperScores;
            this.carrotScores = carrotScores;
            this.potatoScores = potatoScores;
        }

    }

}