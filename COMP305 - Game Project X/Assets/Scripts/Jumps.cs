using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumps : MonoBehaviour
{
    public int jump;
    public int numOfJumps;
    public SpriteRenderer[] jumps;
    public Sprite jumpOn;
    public Sprite jumpOff;

    private void Update()
    {
        for (int i = 0; i < jumps.Length; i++)
        {
            if(i < jump)
            {
                jumps[i].sprite = jumpOn;
            }else
            {
                jumps[i].sprite = jumpOff;
            }
            if(i < numOfJumps)
            {
                jumps[i].enabled = true;
            } else
            {
                jumps[i].enabled = false;
            }
        }
    }
}
