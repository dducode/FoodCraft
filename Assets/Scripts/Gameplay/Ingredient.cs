using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace FoodCraft.Gameplay {

    [RequireComponent(typeof(Rigidbody2D))]
    public class Ingredient : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        public IngredientType IngredientType => ingredientType;
        public Sprite Icon => icon;

        [SerializeField]
        private IngredientType ingredientType;

        [SerializeField]
        private Sprite icon;

        private Rigidbody2D m_rb;
        private Camera m_mainCamera;
        private bool m_followCursor;


        private void Awake () {
            m_rb = GetComponent<Rigidbody2D>();
            Assert.IsTrue(Camera.main != null);
            m_mainCamera = Camera.main;
        }


        private void FixedUpdate () {
            if (m_followCursor) {
                Vector3 position = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                m_rb.MovePosition(position);
                m_rb.angularVelocity = 0;
            }
        }


        public void OnPointerDown (PointerEventData eventData) {
            m_rb.velocity = Vector2.zero;
            m_followCursor = true;
        }


        public void OnPointerUp (PointerEventData eventData) {
            m_rb.AddForce(eventData.delta / Time.deltaTime);
            m_followCursor = false;
        }

    }

}