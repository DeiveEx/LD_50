using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class SORecipe : ScriptableObject
{
    public string dishName;

    public List<FAttributeSetup> ingredients;

    public int turns;
    public int reward;

    public Sprite image;

    public bool CheckRecipeComplete(List<FAttributeSetup> ingredientsUsed)
    {
        if (ingredientsUsed.Count == 0)
        {
            Debug.Log("No Ingredient used");
            return false;
        }

        // For each recipe ingredient
        for (int i = 0; i < ingredients.Count; i++)
        {
            // var used = ingredientsUsed.FirstOrDefault(x => x.Attribute == ingredients[i].Attribute);
            // if (used != null)
            // {
            //     
            // }
            // else
            // {
            //     return false;
            // }
        }

        return false;
    }
}
