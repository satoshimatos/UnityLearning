using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugAI : Enemy
{
    const float speed = .5f;

    private void FixedUpdate()
    {
        if (HitWall()) {
            if (direction == -1) {
                direction = 1;
            } else {
                direction = -1;
            }
        }
        ConstantMoveSideways(direction, speed);
        Flip(direction);
    }
}
