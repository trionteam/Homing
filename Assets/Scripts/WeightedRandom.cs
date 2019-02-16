using UnityEngine;

public interface IWeightedItem<T>
{
    float Weight { get; }

    T Item { get; }
}

public static class WeightedChoice
{
    public static U Choice<T, U>(T[] items) where T: IWeightedItem<U>
    {
        float weightSum = 0.0f;
        foreach (var item in items) weightSum += item.Weight;

        var choice = Random.Range(0.0f, weightSum);
        foreach(var item in items)
        {
            if (choice <= item.Weight) return item.Item;
            choice -= item.Weight;
        }
        throw new System.InvalidOperationException();
    }
}