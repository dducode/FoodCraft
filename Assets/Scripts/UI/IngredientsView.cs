using System.Collections.Generic;
using FoodCraft.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FoodCraft.UI {

    public class IngredientsView : UIBehaviour {

        [SerializeField]
        private Image[] ingredientsRow;

        [SerializeField]
        private Sprite defaultIcon;


        public void Construct (Cauldron cauldron) {
            cauldron.OnPutIngredient += UpdateView;
            cauldron.OnCook += ClearView;
        }


        private void UpdateView (Cauldron cauldron) {
            List<Ingredient> ingredients = cauldron.Ingredients;

            for (var i = 0; i < ingredientsRow.Length; i++)
                ingredientsRow[i].sprite = i < ingredients.Count ? ingredients[i].Icon : defaultIcon;
        }


        private void ClearView (Dictionary<IngredientType, int> dish) {
            foreach (Image image in ingredientsRow)
                image.sprite = defaultIcon;
        }

    }

}