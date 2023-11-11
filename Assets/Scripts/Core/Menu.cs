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


        public static string FindDishName (Dictionary<IngredientType, int> dish) {
            foreach (KeyValuePair<IngredientType, int> ingredientsData in dish)
                foreach (KeyValuePair<Rule, string> menuRow in Data)
                    if (menuRow.Key == ingredientsData)
                        return menuRow.Value;

            return !dish.ContainsKey(IngredientType.Meat) ? "Овощное рагу" : "Суп";
        }

    }

}