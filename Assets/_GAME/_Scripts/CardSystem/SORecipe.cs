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

        int points = ingredients.Count;
        
        // For each recipe ingredient
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredientsUsed.Any(x => x.Attribute == ingredients[i].Attribute))
            {
                var used = ingredientsUsed.First(x => x.Attribute == ingredients[i].Attribute || x.Attribute == ECardAttribute.CA_Wild);

                if (used.Value >= ingredients[i].Value)
                {
                    points--;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return points == 0;
    }
}
