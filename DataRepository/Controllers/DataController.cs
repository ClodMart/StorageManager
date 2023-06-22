using DataRepository.DataModel;
using DataRepository.DataModel.DBDataModel;
using DataRepository.Services;
using DBManager.Interfacce;
using DBManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using System.Text.Json;

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

        public async Task<IActionResult> GetUsedIngredients(string Username, string Password, string? filter = "Tutti", string? query = "NoQuery")
        {

            User CurrentUser;
            try
            {
                CurrentUser = UsersRepository.GetByName(Username);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
            if (CurrentUser.Password == Password)
            {
                List<IngredientViewer> OUT = IngredientsRepo.GetUsedIngredients(filter, query);
                int i = 0;
                StringBuilder json = new StringBuilder();
                json.Append("[");
                foreach (IngredientViewer item in OUT) 
                {
                   i++;
                   json.AppendLine(item.ConvertToJson());
                    if (i < OUT.Count())
                    {
                        json.Append(",");
                    }
                }
                json.Append("]");
                return Ok(json.ToString());
                   
                //return IngredientsRepo.GetUsedIngredients(filter, query);
            }
            return Unauthorized();

        }

        [HttpGet]
        [Route("GetUnUsedIngredients/{filter?}/{query?}")]

        public async Task<IActionResult> GetUnUsedIngredients(string Username, string Password, string? filter = "Tutti", string? query = "NoQuery")
        {
            User CurrentUser;
            try
            {
                CurrentUser = UsersRepository.GetByName(Username);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
            if (CurrentUser.Password == Password)
            {
                List<IngredientViewer> OUT = IngredientsRepo.GetUnUsedIngredients(filter, query);
                int i = 0;
                StringBuilder json = new StringBuilder();
                json.Append("[");
                foreach (IngredientViewer item in OUT)
                {
                    i++;
                    json.AppendLine(item.ConvertToJson());
                    if (i < OUT.Count())
                    {
                        json.Append(",");
                    }
                }
                json.Append("]");
                return Ok(json.ToString());
            }
            return Unauthorized();

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

            //IngredientFormatTemplate NewFormat = Format.GetNewIngredientFormat();
            return IngredientsRepo.AddFormat(Format);
        }

        [HttpPost]
        [Route("UpdateIngredient")]

        public bool UpdateIngredient(IngredientTemplate Ingredient)
        {
            if (!ModelState.IsValid)
                return false;
            try
            {
                
                IngredientsRepo.UpdateIngredient(Ingredient);
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

                IngredientsRepo.UpdateFormat(Format);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
