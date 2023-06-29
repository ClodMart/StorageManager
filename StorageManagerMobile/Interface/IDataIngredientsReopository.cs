using StorageManagerMobile.DataModels;
using StorageManagerMobile.DataModels.DBDataModel;
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

        public Task<long> AddIngredient(IngredientTemplate Ing);

        public Task<bool> UpdateIngredientViewer(IngredientViewer Ingredient);

    }
}
