using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeChecker : MonoBehaviour
{
    private List<Ingredient> ingredients;
    private List<Recipe> recipes;
    private float checkInterval = 3;
    private float elapsedTime = 0;
    private TaskTracker taskTracker;

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<Ingredient>();
        RecipeMenu menu = GameObject.Find("RecipeMenu").GetComponent<RecipeMenu>();
        recipes = menu.recipes;
        taskTracker = GameObject.Find("TaskTracker").GetComponent<TaskTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > checkInterval) {
            Check();
            elapsedTime = 0;
        }
    }

    private void Check() {
        Recipe recipe;
        for (int i = 0; i < recipes.Count; i++) {
            recipe = recipes[i];
            Ingredient ingredient;
            bool fullIngredient = true;
            for (int j = 0; j < recipe.ingredients.Count; j++) {
                ingredient = recipe.ingredients[j];
                List<Ingredient> filtered = ingredients.FindAll(
                        x => x.ingredientType == ingredient.ingredientType
                        && (x.cookedBy == ingredient.cookingMethod
                        || (ingredient.cookingMethod == CookingMethod.Any
                        && x.cookedBy != CookingMethod.RawOrReadyToEat)));
                if (filtered.Count == 0) {
                    fullIngredient = false;
                    break;
                }
            }
            if (fullIngredient) {
                FinishRecipe(recipe);
                break;
            }
        }
    }

    private void FinishRecipe(Recipe recipe) {
        Ingredient ingredient;
        for (int i = 0; i < ingredients.Count; i++) {
            ingredient = ingredients[i];
            Destroy(ingredient.gameObject);
            ingredients.Remove(ingredient);
        }

        Debug.Log($"Cooked {recipe.name}");
        Instantiate(recipe.finishedPrefab, gameObject.transform, false);
        
        if (recipe.name == "Sandwich") {
            taskTracker.sandwichDone = true;
        }
        if (recipe.name == "Fries") {
            taskTracker.friesDone = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Food") {
            return;
        }
        var ingredient = other.gameObject.GetComponent<Ingredient>();
        if (!ingredient) {
            return;
        }
        if (!ingredients.Contains(ingredient)) {
            ingredients.Add(ingredient);
            Check();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Food") {
            return;
        }
        var ingredient = other.gameObject.GetComponent<Ingredient>();
        if (!ingredient) {
            return;
        }
        if (!ingredients.Contains(ingredient)) {
            ingredients.Remove(ingredient);
        }
    }
}
