using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SORecipe: ScriptableObject
{
    public string dishName;

    public List<FAttributeSetup> ingredients;

    public int turns;
    public int reward;

    public Sprite image;
}
