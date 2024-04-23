using System;
using System.Collections.Generic;

namespace FoodCraft.Core {

    public static class Menu {

        private static readonly Dictionary<Rule, string> Data = new() {
            {
                new Rule {
                    ingredientsType = IngredientType.Meat,
                    ingredientsCount = new Range(5, 5)
                },
                "Meat in its own juice"
            }, {
                new Rule {
                    ingredientsType = IngredientType.Meat,
                    ingredientsCount = new Range(4, 4)
                },
                "Meat with side dish"
            }, {
                new Rule {
                    ingredientsType = IngredientType.Meat,
                    ingredientsCount = new Range(2, 3)
                },
                "Stew"
            }, {
                new Rule {
                    ingredientsType = IngredientType.Onion,
                    ingredientsCount = new Range(4, 5)
                },
                "Onion soup"
            }, {
                new Rule {
                    ingredientsType = IngredientType.Potato,
                    ingredientsCount = new Range(4, 5)
                },
                "Mashed potatoes"
            }
        };


        public static string FindDishName (Dictionary<IngredientType, int> dish) {
            foreach (KeyValuePair<IngredientType, int> ingredientsData in dish)
                foreach (KeyValuePair<Rule, string> menuRow in Data)
                    if (menuRow.Key == ingredientsData)
                        return menuRow.Value;

            return !dish.ContainsKey(IngredientType.Meat) ? "Vegetable stew" : "Soup";
        }

    }

}