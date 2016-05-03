using UnityEngine;
using System.Collections;

public abstract class Station : MonoBehaviour
{
    // player currently manning this station
    protected Player currentPlayer;

    public void StartManning(Player p)
    {
        if (currentPlayer != null)
            StopManning();

        currentPlayer = p;
        OnStartManning(currentPlayer);
    }

    public void StopManning()
    {
        if (currentPlayer != null)
        {
            OnStopManning();
            currentPlayer = null;
        }
    }

    public bool IsManned()
    {
        return (currentPlayer != null);
    }

    // called whenever a player starts manning this station
    protected abstract void OnStartManning(Player player);

    // called whenever a player stops manning this station
    // (currentPlayer is still accessible)
    protected abstract void OnStopManning();
}
