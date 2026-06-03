using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Health,
        AmmoSling,
        AmmoShread,
        SlingNone,
        Sling_1,
        Sling_2,
        Sling_3,
        ShredderNone,
        Shredder_1,
        Shredder_2,
        Shredder_3,
        SpellNone,
        Spell_1,
        Spell_2,
        Spell_3
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType){
            default:
            case ItemType.SlingNone: return 0;
            case ItemType.ShredderNone: return 0;
            case ItemType.SpellNone: return 0;
            case ItemType.AmmoSling: return 30;
            case ItemType.AmmoShread: return 25;
            case ItemType.Sling_1: return 200;
            case ItemType.Sling_2: return 350;
            case ItemType.Sling_3: return 450;
            case ItemType.Shredder_1: return 220;
            case ItemType.Shredder_2: return 380;
            case ItemType.Shredder_3: return 520;
            case ItemType.Spell_1: return 120;
            case ItemType.Spell_2: return 220;
            case ItemType.Spell_3: return 320;
            case ItemType.Health: return 20;
        }
    }

   /* public static Sprite GetSprite(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.SlingNone:  return 
            case ItemType.ShredderNone: return 0;
            case ItemType.SpellNone: return 0;
            case ItemType.AmmoSling: return 30;
            case ItemType.AmmoShread: return 25;
            case ItemType.Sling_1: return 200;
            case ItemType.Sling_2: return 350;
            case ItemType.Sling_3: return 450;
            case ItemType.Shredder_1: return 220;
            case ItemType.Shredder_2: return 380;
            case ItemType.Shredder_3: return 520;
            case ItemType.Spell_1: return 120;
            case ItemType.Spell_2: return 220;
            case ItemType.Spell_3: return 320;
            case ItemType.Health: return 20;
        }
    }*/

}
