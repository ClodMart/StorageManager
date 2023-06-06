using DataRepository.Services;
using DBManager.Interfacce;
using DBManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            string Filepath = AppDomain.CurrentDomain.BaseDirectory + "Order" + DateOnly.FromDateTime(DateTime.Now).ToString().Replace("/","-") + ".xls";
            List<CategoryIngredientList> order = CategoryIngredientsRepository.GetSelectedIngredientList(id);
            DataSet Out = ExcelCreator.CreateExcel(order);
            //ExcelLibrary.DataSetHelper.CreateWorkbook(Filepath, Out);
            using (var ms = new MemoryStream())
            {
                ExcelLibrary.DataSetHelper.CreateWorkbook(ms, Out);
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Order" + DateOnly.FromDateTime(DateTime.Now).ToString().Replace("/", "-") + ".xlsx");
            }

            //FileStream fs = new FileStream(Filepath, FileMode.Open, FileAccess.Read);
            //return File(fs, "application/vnd.ms-excel");
        }
    }
}
