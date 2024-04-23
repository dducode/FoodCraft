using System;
using System.Collections.Generic;
using System.Text;
using FoodCraft.Core;
using FoodCraft.Data;
using FoodCraft.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FoodCraft.UI {

    public class MainView : UIBehaviour {

        [SerializeField]
        private TextMeshProUGUI score;

        [SerializeField]
        private TextMeshProUGUI lastDish;

        [SerializeField]
        private TextMeshProUGUI bestDish;

        [SerializeField]
        private Button restartButton;


        /// <summary>
        /// Call this to pass dependencies to the class
        /// </summary>
        public void Construct (Player player) {
            player.score.OnValueChanged += score => this.score.text = $"Score: {score}";
            player.lastDishData.OnValueChanged += data => lastDish.text = $"Latest dish: {BuildDishRow(data)}";
            player.bestDishData.OnValueChanged += data => bestDish.text = $"Best dish: {BuildDishRow(data)}";
        }


        protected override void Awake () {
            base.Awake();
            restartButton.onClick.AddListener(Game.Restart);
        }


        private string BuildDishRow (DishData data) {
            var builder = new StringBuilder();

            foreach (KeyValuePair<IngredientType, int> dishData in data.compound) {
                builder.Append($"{dishData.Value} ");

                switch (dishData.Key) {
                    case IngredientType.Meat:
                        builder.Append("meat");
                        break;
                    case IngredientType.Onion:
                        builder.Append("onion");
                        break;
                    case IngredientType.Pepper:
                        builder.Append("pepper");
                        break;
                    case IngredientType.Carrot:
                        builder.Append("carrot");
                        break;
                    case IngredientType.Potato:
                        builder.Append("potato");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                builder.Append(", ");
            }

            builder.Remove(builder.Length - 2, 2);

            return $"{data.name} ({builder}) [{data.score}]";
        }

    }

}