using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishActor : MonoBehaviour
{
    public int TurnsToCook
    {
        get
        {
            return recipe != null ? recipe.turns : 9;
        }
    }

    public SORecipe recipe;

    [SerializeField] List<FAttributeSetup> ingredientsUsed;

    [SerializeField] FAttributeSetup test;

    // Functions

    public void AddIngredient(FAttributeSetup ingredient)
    {
        for (int i = 0; i < ingredientsUsed.Count; i++)
        {
            if(ingredientsUsed[i].Attribute == ingredient.Attribute)
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
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            if(ingredientsUsed.Count == 0)
            {
                print("No Ingredient used");
                return false;
            }

            bool ingredientFound = false;

            //For Each Ingredient Used
            for (int k = 0; k < ingredientsUsed.Count; k++)
            {
                if(recipe.ingredients[i].Attribute != ingredientsUsed[k].Attribute )
                {
                    continue;
                }

                if (recipe.ingredients[i].Value <= ingredientsUsed[k].Value)
                {
                    Debug.Log("Enought " + recipe.ingredients[i].Attribute);
                    ingredientFound = true;
                    break;
                }
                else
                {
                    Debug.Log("Not Enought Ingredients: " + recipe.ingredients[i].Attribute);
                    return false;
                }
            }

            if (!ingredientFound)
            {
                //No Match
                print("Ingredient Missing: " + recipe.ingredients[i].Attribute);
                return false;
            }
        }

        print("Recipe Completed");
        return true;
    }

}
