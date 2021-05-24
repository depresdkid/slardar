using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour
{
    public Text health;
    public Text deshReload;

    void Update()
    {
        try
        {
            // обновляет hp
            if (Player.player.isAlive)
            {
                health.text = $"Health = {UI.playerHealth}";
            }
            else
                health.text = $"Вы погибли";
            //обновляет дэш
            if (UI.deshReload <= 0)
            {
                deshReload.fontSize = 28;
                deshReload.fontStyle = FontStyle.Bold;
                deshReload.text = "DESH IS READY";
            }
            else {
                deshReload.fontStyle = FontStyle.Normal;
                deshReload.fontSize = 25;
                deshReload.text = $"Desh reload: {UI.deshReload}";
            }            
        }
        catch (System.Exception)
        {

        }
    }
}
