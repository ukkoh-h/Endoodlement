using UnityEngine;
using System.Collections.Generic;


public class GameAssets : MonoBehaviour
{

    private static GameAssets _i;


    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }

    }
    public Sprite Health;
    public Sprite Shredder;
    public Sprite Shredder2;
    public Sprite Ammo;
    public Sprite Slingshot;
    public Sprite Slingshot2;
    public Sprite Coins;
    public Sprite SpellBook;
    public Sprite SpellBook2;
}
