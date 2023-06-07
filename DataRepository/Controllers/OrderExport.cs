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
    [Route("api/ExportOrder")]
    [ApiController]
    public class OrderExport : Controller
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly CategoryIngredientListsRepository CategoryIngredientsRepository = new CategoryIngredientListsRepository(context);

        public OrderExport()
        {


        }

        [HttpGet]
        [Route("GetOrderById/{Id}")]

        public IActionResult GetOrderById(long id)
        {
            string Filepath = AppDomain.CurrentDomain.BaseDirectory + "Order" + DateOnly.FromDateTime(DateTime.Now).ToString().Replace("/", "-") + ".xls";
            List<CategoryIngredientList> order = CategoryIngredientsRepository.GetSelectedIngredientList(id);
            DataSet Out = ExcelCreator.CreateExcel(order);
                using (var ms = new MemoryStream())
                {
                    return File(Helper.ExportToExcelDownload(Out), "application/vnd.ms-excel", "Order" + DateOnly.FromDateTime(DateTime.Now).ToString().Replace("/", "-") + ".xlsx");
                }
            }
        }
    }
