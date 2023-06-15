using StorageManagerMobile.DataModels;
using StorageManagerMobile.ViewModels.Groupings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.Interface
{
    public interface IDataIngredientsReopository
    {
        public Task<List<IngredientViewerViewModel>> GetUsedIngredientsAsync(string filter, string query);

        public Task<List<IngredientViewerViewModel>> GetUnUsedIngredientsAsync(string filter, string query);

        public Task<long> AddIngredient(IngredientViewer Ingredient);

        public Task<bool> UpdateIngredient(IngredientViewer Ingredient);

    }
}
