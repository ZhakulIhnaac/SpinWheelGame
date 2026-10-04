using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace Utilities
{
    public static class CollectionExtensions
    {
        private static Random _random = new Random();

        public static int GetRandomIndex<T>(this IReadOnlyList<T> collection)
        {
            return _random.Next(collection.Count);
        }
        
        public static T GetRandomElement<T>(this IReadOnlyList<T> collection)
        {
            return collection.Count > 0 ? collection[collection.GetRandomIndex()] : default;
        }
        
        public static int GetWeightedRandomIndex(this IReadOnlyList<float> weights)
        {
            var totalWeight = 0f;

            for (int i = 0; i < weights.Count; i++)
            {
                totalWeight += weights[i];
            }

            var randomValue = (float)_random.NextDouble() * totalWeight;
            var cumulativeWeight = 0f;

            for (int i = 0; i < weights.Count; i++)
            {
                cumulativeWeight += weights[i];

                if (randomValue < cumulativeWeight)
                {
                    return i;
                }
            }

            throw new System.InvalidOperationException("Weights must contain at least one positive value");
        }

        public static void Shuffle<T>(this IList<T> collection)
        {
            int n = collection.Count();  
            while (n > 1) {  
                n--;  
                int k = _random.Next(n + 1);  
                (collection[k], collection[n]) = (collection[n], collection[k]);
            }
        }
    }
}
