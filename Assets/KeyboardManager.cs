using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public CatTile[] catTiles;

    // Start is called before the first frame update
    void Start()
    {

        foreach (var key in catTiles)
        {
            if (key != null)
            {

                key.gameObject.AddComponent<BoxCollider2D>();

            }
        }

    }

}
