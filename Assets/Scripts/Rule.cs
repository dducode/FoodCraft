using System;
using System.Collections.Generic;

namespace FoodCraft {

    public struct Rule {

        public IngredientType ingredientsType;
        public Range ingredientsCount;


        public static bool operator == (Rule rule, (IngredientType type, int count) dish) {
            return dish.type == rule.ingredientsType
                   && dish.count >= rule.ingredientsCount.Start.Value
                   && dish.count <= rule.ingredientsCount.End.Value;
        }


        public static bool operator == (Rule rule, KeyValuePair<IngredientType, int> dish) {
            return dish.Key == rule.ingredientsType
                   && dish.Value >= rule.ingredientsCount.Start.Value
                   && dish.Value <= rule.ingredientsCount.End.Value;
        }


        public static bool operator != (Rule rule, (IngredientType type, int count) dish) {
            return !(rule == dish);
        }


        public static bool operator != (Rule rule, KeyValuePair<IngredientType, int> dish) {
            return !(rule == dish);
        }


        public bool Equals (Rule other) {
            return ingredientsType == other.ingredientsType && ingredientsCount.Equals(other.ingredientsCount);
        }


        public override bool Equals (object obj) {
            return obj is Rule other && Equals(other);
        }


        public override int GetHashCode () {
            return HashCode.Combine((int)ingredientsType, ingredientsCount);
        }

    }

}