using System.Collections.Generic;
using System.IO;
using FoodCraft.Data;

namespace FoodCraft.Core {

    public static class BinaryHandlersExtensions {

        public static void Write (this BinaryWriter writer, DishData data) {
            writer.Write(data.name);
            writer.Write(data.score);
            writer.Write(data.compound.Count);

            foreach (KeyValuePair<IngredientType, int> keyValuePair in data.compound) {
                writer.Write((int)keyValuePair.Key);
                writer.Write(keyValuePair.Value);
            }
        }


        public static DishData ReadDishData (this BinaryReader reader) {
            var data = new DishData {
                name = reader.ReadString(),
                score = reader.ReadInt32()
            };

            int count = reader.ReadInt32();
            var dish = new Dictionary<IngredientType, int>();

            for (var i = 0; i < count; i++)
                dish.Add((IngredientType)reader.ReadInt32(), reader.ReadInt32());

            data.compound = dish;

            return data;
        }

    }

}