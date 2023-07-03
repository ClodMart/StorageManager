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

        public ActionResult PostNewIngredient(string Username, string Password, IngredientTemplate Ingredient)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Ingredient NewIngredient = Ingredient.GetNewIngredient();
            return Ok(IngredientsRepo.AddIngredient(NewIngredient));                
        }

        [HttpPost]
        [Route("PostNewFormat")]

        public ActionResult PostNewFormat(string Username, string Password, IngredientFormatTemplate Format)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //IngredientFormatTemplate NewFormat = Format.GetNewIngredientFormat();
            return Ok(IngredientsRepo.AddFormat(Format));
        }

        #endregion
        
        #region Post Update
        [HttpPost]
        [Route("UpdateIngredient")]

        public ActionResult UpdateIngredient(string Username, string Password, IngredientTemplate Ingredient)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                
                IngredientsRepo.UpdateIngredient(Ingredient);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }            
        }

        [HttpPost]
        [Route("UpdateFormat")]

        public ActionResult UpdateFormat(string Username, string Password, IngredientFormatTemplate Format)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {

                IngredientsRepo.UpdateFormat(Format);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion

        #region Post Delete
        [HttpPost]
        [Route("DeleteFormat/{id}")]

        public ActionResult DeleteFormat(long id)
        {
            try
            {
                IngredientsRepo.DeleteFormat(new IngredientFormatTemplate(IngredientsRepo.GetFormatFromId(id)));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("DeleteIngredient/{id}")]
        public ActionResult DeleteIngredient(long id)
        {
            try
            {
                IngredientsRepo.DeleteIngredient(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
