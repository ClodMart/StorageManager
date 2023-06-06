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
            string Filepath = AppDomain.CurrentDomain.BaseDirectory + "Order" + DateOnly.FromDateTime(DateTime.Now) + ".xls";
            List<CategoryIngredientList> order = CategoryIngredientsRepository.GetSelectedIngredientList(id);
            DataSet Out = ExcelCreator.CreateExcel(order);
            ExcelLibrary.DataSetHelper.CreateWorkbook(Filepath, Out);
            FileStream fs = new FileStream(Filepath, FileMode.Open, FileAccess.Read);
            return File(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
