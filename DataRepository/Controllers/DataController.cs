using DataRepository.DataModel;
using DataRepository.DataModel.DBDataModel;
using DataRepository.Services;
using DBManager.Interfacce;
using DBManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DataRepository.Controllers
{
    [Route("api/DataController/{Username}/{Password}")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly CategoryIngredientListsRepository CategoryIngredientsRepository = new CategoryIngredientListsRepository(context);
        private static readonly UsersRepository UsersRepository = new UsersRepository(context);
        private static IngredientsDataModel IngredientsRepo = IngredientsDataModel.Instance; 

        public DataController()
        {


        }

        [HttpGet]
        [Route("GetUsedIngredients/{filter?}/{query?}")]

        public IEnumerable<IngredientViewer> GetUsedIngredients(string Username, string Password, string? filter = "Tutti", string? query = "NoQuery")
        {
            User CurrentUser;
            try
            {
                CurrentUser = UsersRepository.GetByName(Username);
            }
            catch (Exception ex)
            {
                return null;
            }
            if (CurrentUser.Password == Password)
            {
                IEnumerable<IngredientViewer> OUT = IngredientsRepo.GetUsedIngredients(filter, query);
                return OUT;
                //return IngredientsRepo.GetUsedIngredients(filter, query);
            }
            return null;

        }

        [HttpGet]
        [Route("GetUnUsedIngredients/{filter?}/{query?}")]

        public IEnumerable<IngredientViewer> GetUnUsedIngredients(string Username, string Password, string? filter = "Tutti", string? query = "NoQuery")
        {
            User CurrentUser;
            try
            {
                CurrentUser = UsersRepository.GetByName(Username);
            }
            catch (Exception ex)
            {
                return null;
            }
            if (CurrentUser.Password == Password)
            {
                return IngredientsRepo.GetUnUsedIngredients(filter, query);
            }
            return null;

        }

        [HttpPost]
        [Route("PostNewIngredient")]

        public long PostNewIngredient(IngredientTemplate Ingredient)
        {
            if (!ModelState.IsValid)
                return -1;

            Ingredient NewIngredient = Ingredient.GetNewIngredient();
            return IngredientsRepo.AddIngredient(NewIngredient);                
        }

        [HttpPost]
        [Route("PostNewFormat")]

        public long PostNewFormat(IngredientFormatTemplate Format)
        {
            if (!ModelState.IsValid)
                return -1;

            IngredientsFormat NewFormat = Format.GetNewIngredientFormat();
            return IngredientsRepo.AddFormat(NewFormat);
        }

        [HttpPost]
        [Route("UpdateIngredient")]

        public bool UpdateIngredient(IngredientTemplate Ingredient)
        {
            if (!ModelState.IsValid)
                return false;
            try
            {
                
                IngredientsRepo.UpdateIngredient(Ingredient.GetNewIngredient());
                return true;
            }
            catch
            {
                return false;
            }            
        }

        [HttpPost]
        [Route("UpdateFormat")]

        public bool UpdateFormat(IngredientFormatTemplate Format)
        {
            if (!ModelState.IsValid)
                return false;
            try
            {

                IngredientsRepo.UpdateFormat(Format.GetNewIngredientFormat());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
