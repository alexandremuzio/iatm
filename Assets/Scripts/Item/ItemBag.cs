﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class ItemBag {

    class ItemItem
    {
        public string Name {get; private set;}
        public Action<Player> Effect {get; private set;}

        public ItemItem(string name, Action<Player> effect)
        {
            Name = name;
            Effect = effect;
        }
    }
    
    private static Dictionary<string, Action<Player>> PossibleItemActions = new Dictionary<string, Action<Player>>
    {
        {"SmallAmmo", (p) => p.Weapon.AddAmmo(5)},
        {"MediumAmmo", (p) => p.Weapon.AddAmmo(10)},
        {"BigAmmo", (p) => p.Weapon.AddAmmo(20)},
        {"SayMonster", (p) => p.SayName("I am the monster")},        
    };

    private List<ItemItem> itemActions = new List<ItemItem>();

    [SerializeField]
    List<string> possibleKeys;

    private System.Random rnd;

    [SerializeField]
    private int maxSize = 5;

    private int _size;

    public ItemBag()
    {
        rnd = new System.Random();
        _size = rnd.Next(maxSize);
        for (int i = 0; i < _size; i++)
        {
            int sel = rnd.Next(PossibleItemActions.Count);     
            possibleKeys = new List<string>(PossibleItemActions.Keys);
            var selectedKey = possibleKeys[sel];
            itemActions.Add(new ItemItem(selectedKey, PossibleItemActions[selectedKey]));
        }
        
    }

    public void Open(Transform transform)
    {
        foreach(var di in itemActions)
        {
            Item.Create(di.Name, transform, di.Effect);
        }
    }
}
