using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLogger : MonoBehaviour
{
    public Text logTextUI; // Unity UI Text
    public int maxLines = 10;

    private Queue<string> logLines = new Queue<string>();
    
    public void Log(string message)
    {
        logLines.Enqueue(message);

        if (logLines.Count > maxLines)
        {
            logLines.Dequeue();
        }

        logTextUI.text = string.Join("\n", logLines);
    }

    public void Clear()
    {
        logLines.Clear();
        logTextUI.text = "";
    }
}