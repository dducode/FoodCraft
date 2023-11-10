using UnityEngine;
using UnityEngine.EventSystems;

namespace FoodCraft.Gameplay {

    public class Bowl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        [SerializeField]
        private Ingredient spawnableIngredient;

        private Ingredient m_capturedIngredient;


        public void OnPointerDown (PointerEventData eventData) {
            Ingredient ingredient = Instantiate(spawnableIngredient);

            if (Camera.main != null) {
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                ingredient.transform.position = position;
            }

            m_capturedIngredient = ingredient;
            m_capturedIngredient.OnPointerDown(eventData);
        }


        public void OnPointerUp (PointerEventData eventData) {
            if (m_capturedIngredient != null)
                m_capturedIngredient.OnPointerUp(eventData);
        }

    }

}