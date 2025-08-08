using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TurnPredictionSystem
{
    public static List<BattleUnit> Predict(List<BattleUnit> units, int count)
    {
        List<PredictedUnit> simulated = new List<PredictedUnit>();
        foreach (var unit in units)
        {
            simulated.Add(new PredictedUnit
            {
                Unit = unit,
                Remaining = unit.data.turnCounter
            });
        }

        List<BattleUnit> prediction = new List<BattleUnit>();
        for (int i = 0; i < count; i++)
        {
            simulated = simulated.OrderBy(p => p.Remaining).ToList();
            var next = simulated[0];
            prediction.Add(next.Unit);
            next.Remaining += next.Unit.data.speed;
        }

        return prediction;
    }

    private class PredictedUnit
    {
        public BattleUnit Unit;
        public int Remaining;
    }
}