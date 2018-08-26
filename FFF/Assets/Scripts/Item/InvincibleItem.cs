using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItem : Item
{
    public override void Excute(CharRoot cr)
    {
        cr.CharHealth.TakeInvincible();
    }
}
