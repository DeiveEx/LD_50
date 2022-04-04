using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SORecipe : ScriptableObject
{
    public string dishName;

    public List<FAttributeSetup> ingredients;

    public int turns;
    public int reward;

    public Sprite image;


    // Gameplay Stuff
    public List<FAttributeSetup> ingredientsUsed;


    // Functions

    public void AddIngredient(FAttributeSetup ingredient)
    {
        for (int i = 0; i < ingredientsUsed.Count; i++)
        {
            if (ingredientsUsed[i].Attribute == ingredient.Attribute)
            {
                var newAttribute = ingredientsUsed[i];

                newAttribute.Value += ingredient.Value;

                ingredientsUsed[i] = newAttribute;
                return;
            }
        }
        ingredientsUsed.Add(ingredient);
    }

    [ContextMenu("CheckRecipeComplete")]
    bool CheckRecipeComplete()
    {

        // For each recipe ingredient
        for (int i = 0; i < ingredientsUsed.Count; i++)
        {
            if (ingredientsUsed.Count == 0)
            {
                Debug.Log("No Ingredient used");
                return false;
            }

            bool ingredientFound = false;

            //For Each Ingredient Used
            for (int k = 0; k < ingredientsUsed.Count; k++)
            {
                if (ingredientsUsed[i].Attribute != ingredientsUsed[k].Attribute)
                {
                    continue;
                }

                if (ingredientsUsed[i].Value <= ingredientsUsed[k].Value)
                {
                    Debug.Log("Enought " + ingredientsUsed[i].Attribute);
                    ingredientFound = true;
                    break;
                }
                else
                {
                    Debug.Log("Not Enought Ingredients: " + ingredientsUsed[i].Attribute);
                    return false;
                }
            }

            if (!ingredientFound)
            {
                //No Match
                Debug.Log("Ingredient Missing: " + ingredientsUsed[i].Attribute);
                return false;
            }
        }

        Debug.Log("Recipe Completed");
        return true;
    }
}
