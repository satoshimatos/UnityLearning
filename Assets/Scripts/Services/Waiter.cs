using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter
{
    public IEnumerator waitForSeconds(int time)
    {
        yield return new WaitForSeconds(time);
    }
}
