using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodCraft.Gameplay {

    public class Cauldron : MonoBehaviour {

        public event Action<Cauldron> OnPutIngredient;
        public event Action<Dictionary<IngredientType, int>> OnCook;

        public List<Ingredient> Ingredients { get; } = new();

        [SerializeField]
        private GameObject lid;


        public void Cook (float cookingTime) {
            StartCoroutine(Cooking(cookingTime));
        }


        private void OnTriggerEnter2D (Collider2D collider) {
            if (!collider.isTrigger && collider.TryGetComponent(out Ingredient ingredient)) {
                Ingredients.Add(ingredient);
                OnPutIngredient?.Invoke(this);
                Destroy(ingredient.gameObject);
            }
        }


        private IEnumerator Cooking (float cookingTime) {
            lid.SetActive(true);

            var dish = new Dictionary<IngredientType, int>();

            foreach (Ingredient ingredient in Ingredients) {
                IngredientType ingredientType = ingredient.IngredientType;
                if (dish.ContainsKey(ingredientType))
                    dish[ingredientType]++;
                else
                    dish.Add(ingredientType, 1);
            }

            yield return new WaitForSeconds(cookingTime);
            lid.SetActive(false);
            OnCook?.Invoke(dish);
            Ingredients.Clear();
        }

    }

}