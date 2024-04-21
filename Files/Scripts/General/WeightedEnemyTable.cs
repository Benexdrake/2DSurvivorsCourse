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
        _weightSum = enemy.Weight;
    }

    public string PickEnemy()
    {
        var rand = new Random();
        var choosenWeight = rand.Next(1, _weightSum + 1);
        
        foreach(var enemy in enemys)
        {
            if(enemy.Weight == choosenWeight)
            {
                return enemy.Name;
            }
        }
        return enemys[0].Name;
    }
}