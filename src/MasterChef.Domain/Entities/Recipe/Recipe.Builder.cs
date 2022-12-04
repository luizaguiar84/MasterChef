using System;

namespace MasterChef.Domain.Entities.Recipe;

public partial class Recipe
{
    
    public class Builder : IRecipeBuilder
    {
        public Recipe Instance { get; set; }

        public Builder()
        {
            this.Instance = new Recipe();
        }

        public Builder(Recipe recipe)
        {
            this.Instance = recipe;
        }

        public IRecipeBuilder WithCreateDate(DateTime createDate)
        {
            Instance.CreateDate = createDate;
            return this;
        }

        public IRecipeBuilder WithLastChange(DateTime lastChange)
        {
            Instance.LastChange = lastChange;
            return this;
        }

        public IRecipeBuilder WithTitle(string title)
        {
            Instance.Title = title;
            return this;
        }

        public IRecipeBuilder WithDescription(string description)
        {
            Instance.Description = description;
            return this;
        }

        public IRecipeBuilder WithWayOfPrepare(string wayOfPrepare)
        {
            Instance.WayOfPrepare = wayOfPrepare;
            return this;
        }

        public IRecipeBuilder WithPicture(string picture)
        {
            Instance.Picture = picture;
            return this;
        }
    }

    
    public interface IRecipeBuilder
    {
        IRecipeBuilder WithCreateDate(DateTime createDate);
        IRecipeBuilder WithLastChange(DateTime lastChange);
        IRecipeBuilder WithTitle(string title);
        IRecipeBuilder WithDescription(string description);
        IRecipeBuilder WithWayOfPrepare(string wayOfPrepare);
        IRecipeBuilder WithPicture(string picture);
        
        //public IList<Ingredient> Ingredients { get; set; }
        //public IList<Tag> Tags { get; set; }
        
   
    }
}