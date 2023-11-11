using FoodCraft.Gameplay;
using FoodCraft.UI;
using UnityEngine;

namespace FoodCraft.Services {

    /// <summary>
    /// Use the container to resolve dependencies
    /// </summary>
    public class DIContainer : MonoBehaviour {

        [SerializeField]
        private MainView ui;

        [SerializeField]
        private IngredientsView ingredientsView;

        [SerializeField]
        private Cauldron cauldron;

        [SerializeField]
        private Player player;


        private void Awake () {
            ui.Construct(player);
            player.Construct(cauldron);
            ingredientsView.Construct(cauldron);
        }

    }

}