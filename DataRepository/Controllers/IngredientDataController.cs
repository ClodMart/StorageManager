using DataRepository.DataModel;
using DataRepository.DataModel.DBDataModel;
using DataRepository.Services;
using DBManager.Interfacce;
using DBManager.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Text.Json;

namespace DataRepository.Controllers
{
    [Route("api/DataController/{Username}/{Password}")]
    [ApiController]
    public class IngredientDataController : ControllerBase
    {
        private static readonly StorageManagerDBContext context = new StorageManagerDBContext();
        private static readonly CategoryIngredientListsRepository CategoryIngredientsRepository = new CategoryIngredientListsRepository(context);
        private static readonly UsersRepository UsersRepository = new UsersRepository(context);
        private static IngredientsDataModel IngredientsRepo = IngredientsDataModel.Instance; 

        public IngredientDataController()
        {


        }

        #region GetRoutes

        [HttpGet]
        [Route("GetUsedIngredients/{filter?}/{query?}")]

        public ActionResult<List<IngredientViewer>> GetUsedIngredients(string Username, string Password, string? filter = "Tutti", string? query = "NoQuery")
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
                return Ok(JsonConvert.SerializeObject(OUT));                  
            }
            return Unauthorized();

        }

        [HttpGet]
        [Route("GetUnUsedIngredients/{filter?}/{query?}")]

        public ActionResult<List<IngredientViewer>> GetUnUsedIngredients(string Username, string Password, string? filter = "Tutti", string? query = "NoQuery")
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
                return Ok(JsonConvert.SerializeObject(OUT));
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("GetIngredientByName/{name}")]
        public ActionResult<IngredientTemplate> GetIngredientByName(string Username, string Password, string name)
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
                IngredientTemplate OUT = IngredientsRepo.GetIngredientByName(name);
                return Ok(JsonConvert.SerializeObject(OUT));
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("GetFormatsFromIngredientId/{id}")]
        public ActionResult<List<IngredientFormatTemplate>> GetFormatsFromIngredientId(string Username, string Password, long id)
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
                List<IngredientFormatTemplate> OUT = IngredientsRepo.GetFormatsByIngredientID(id);
                return Ok(JsonConvert.SerializeObject(OUT));
            }
            return Unauthorized();

        }
        #endregion

        #region Post Add routes
        [HttpPost]
        [Route("PostNewIngredient")]

        public long PostNewIngredient(string Username, string Password, IngredientTemplate Ingredient)
        {
            if (!ModelState.IsValid)
                return -1;

            Ingredient NewIngredient = Ingredient.GetNewIngredient();
            return IngredientsRepo.AddIngredient(NewIngredient);                
        }

        [HttpPost]
        [Route("PostNewFormat")]

        public long PostNewFormat(string Username, string Password, IngredientFormatTemplate Format)
        {
            if (!ModelState.IsValid)
                return -1;

            //IngredientFormatTemplate NewFormat = Format.GetNewIngredientFormat();
            return IngredientsRepo.AddFormat(Format);
        }

        #endregion
        
        #region Post Update
        [HttpPost]
        [Route("UpdateIngredient")]

        public bool UpdateIngredient(string Username, string Password, IngredientTemplate Ingredient)
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

        public bool UpdateFormat(string Username, string Password, IngredientFormatTemplate Format)
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
        #endregion

        #region Post Delete
        [HttpPost]
        [Route("DeleteFormat/{id}")]

        public bool DeleteFormat(long id)
        {
            try
            {
                IngredientsRepo.DeleteFormat(new IngredientFormatTemplate(IngredientsRepo.GetFormatFromId(id)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        [Route("DeleteIngredient/{id}")]
        public bool DeleteIngredient(long id)
        {
            try
            {
                IngredientsRepo.DeleteIngredient(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
