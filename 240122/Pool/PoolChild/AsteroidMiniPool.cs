using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMiniPool : ObjectPool<AsteroidMini>
{
    protected override void OnGetObject(AsteroidMini component)
    {
        component.Direction = -component.transform.right;
    }
}
