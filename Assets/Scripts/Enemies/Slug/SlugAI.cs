using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugAI : Enemy
{
    const float speed = 0.5f;

    private void FixedUpdate()
    {
        WalkOnWallsAndCeiling(speed);
    }
}
