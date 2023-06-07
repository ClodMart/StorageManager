using DataRepository.Services;
using DBManager.Interfacce;
using DBManager.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DataRepository.Controllers
{
    [Route("api/ExportOrder/{Username}/{Password}")]
    [ApiController]
    public class OrderExport : ControllerBase
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly CategoryIngredientListsRepository CategoryIngredientsRepository = new CategoryIngredientListsRepository(context);
        private static readonly UsersRepository UsersRepository = new UsersRepository(context);

        public OrderExport()
        {


        }

        [HttpGet]
        [Route("GetOrderById/{Id}")]

        public IActionResult GetOrderById(string Username, string Password, long Id)
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
            if(CurrentUser.Password == Password)
            {
                string Filepath = AppDomain.CurrentDomain.BaseDirectory + "Order" + DateOnly.FromDateTime(DateTime.Now).ToString().Replace("/", "-") + ".xls";
                List<CategoryIngredientList> order = CategoryIngredientsRepository.GetSelectedIngredientList(Id);
                DataSet Out = ExcelCreator.CreateExcel(order);
                using (var ms = new MemoryStream())
                {
                    return File(Helper.ExportToExcelDownload(Out), "application/vnd.ms-excel", "Order" + DateOnly.FromDateTime(DateTime.Now).ToString().Replace("/", "-") + ".xlsx");
                }
            }
            return Unauthorized();
           
            }
        }
    }
