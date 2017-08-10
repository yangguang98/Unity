using UnityEngine;
using System.Collections;

public class SaleIcon : MonoBehaviour {

    SpriteRenderer render;
    Tower tower;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();

    }

    public void Load(Tower tower)
    {
        this.tower = tower;
    }

    void OnMouserDown()
    {
        SendMessageUpwards("OnSaleTower", tower,SendMessageOptions.RequireReceiver);
    }
}
