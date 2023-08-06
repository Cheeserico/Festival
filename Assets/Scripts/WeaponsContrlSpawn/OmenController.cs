using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenController : BaseWeapon
{

    private void Update()
    {
        // ‰ñ“]
        transform.Rotate(new Vector3(0, 0, -1000 * Time.deltaTime));
    }

    // “®‚«‚Æ“–‚½‚è”»’è‚Ìˆ—‚ğ’Ç‰Á

    // ƒgƒŠƒK[‚ªÕ“Ë‚µ‚½
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }
}
