using System;
using System.Collections.Generic;
using Godot;
public class WeightedEnemyTable
{
    private List<Enemy> enemys = new();
    private int _weightSum = 0;

    public void AddEnemy(Enemy enemy)
    {
        enemys.Add(enemy);
        _weightSum += enemy.Weight;
    }

    public string PickEnemy()
    {
        double totalWeight = _weightSum / 10;
        double randomValue = new Random().NextDouble() * totalWeight;

        double accumulatedWeight = 0;
        foreach (var enemy in enemys)
        {
            accumulatedWeight += enemy.Weight / 10;
            if (randomValue <= accumulatedWeight)
            {
                return enemy.Name;
            }
        }
        GD.Print(enemys[0].Name);
        return enemys[0].Name;
    }
}