using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodCraft.Data {

    /// <summary>
    /// Contains data of the dish - name in menu, score and compound
    /// </summary>
    public struct DishData {

        public Dictionary<IngredientType, int> compound;
        public string name;
        public int score;


        public static bool operator == (DishData dishData1, DishData dishData2) {
            KeyValuePair<IngredientType, int>[] compound1 = dishData1.compound.ToArray();
            KeyValuePair<IngredientType, int>[] compound2 = dishData2.compound.ToArray();

            if (dishData1.name != dishData2.name || dishData1.score != dishData2.score)
                return false;

            for (var i = 0; i < compound1.Length && i < compound2.Length; i++)
                if (compound1[i].Key != compound2[i].Key || compound1[i].Value != compound2[i].Value)
                    return false;

            return true;
        }


        public static bool operator != (DishData data1, DishData data2) {
            return !(data1 == data2);
        }


        public bool Equals (DishData other) {
            return Equals(compound, other.compound) && name == other.name && score == other.score;
        }


        public override bool Equals (object obj) {
            return obj is DishData other && Equals(other);
        }


        public override int GetHashCode () {
            return HashCode.Combine(compound, name, score);
        }

    }

}