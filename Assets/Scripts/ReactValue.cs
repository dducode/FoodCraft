using System;

namespace FoodCraft {

    public struct ReactValue<T> {

        public T Value {
            get => m_value;
            set {
                m_value = value;
                OnValueChanged?.Invoke(m_value);
            }
        }

        public event Action<T> OnValueChanged;
        private T m_value;

    }

}