using System;
using System.Collections.Generic;
using Godot;
public class WeightTable
{
    private List<(PackedScene, int)> list = new();
    private int _weightSum = 0;

    public void Add(PackedScene enemy, int weight)
    {
        list.Add((enemy, weight));
        _weightSum += weight;
    }

    public PackedScene Pick()
    {
        var choosenWeight = GD.RandRange(1, _weightSum);
        int iterationSum = 0;

        foreach (var item in list)
        {
            iterationSum += item.Item2;
            GD.Print(item.Item2);
            if(choosenWeight <= iterationSum)
                return item.Item1;
        }
        return Pick();
    }
}