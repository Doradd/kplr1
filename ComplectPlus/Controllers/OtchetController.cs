using ComplectPlus.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using ComplectPlus.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace ComplectPlus.Controllers
{
    public class OtchetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OtchetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OtchetController
        public ActionResult Index(int? id)
        {
            ViewBag.ComponentId = id;

            return View();
        }

        [HttpPost]
        public ActionResult Import(IFormFile fileExcel)
        {
            using (XLWorkbook workbook = new XLWorkbook(fileExcel.OpenReadStream()))
            {
                List<CategoryRef> categoryRefs = new List<CategoryRef>();

                List<ComponentRef> componentRefs = new List<ComponentRef>();

                foreach (IXLWorksheet worksheet in workbook.Worksheets)
                {


                   

                    if (worksheet.Name == "Component")
                    {
                        foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                        {
                            Сomponent component = new Сomponent();

                            var range = worksheet.RangeUsed();

                            var table = range.AsTable();
                                              
                            component.ComponentsName = row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "ComponentsName").RangeAddress.FirstAddress.ColumnNumber).Value.ToString();
                            
                            component.YearRelease = Int32.Parse(row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "YearRelease").RangeAddress.FirstAddress.ColumnNumber).Value.ToString());

                            component.CategoryId = Int32.Parse(row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "CategoryId").RangeAddress.FirstAddress.ColumnNumber).Value.ToString());

                            component.ManufacturerId = Int32.Parse(row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "ManufacturerId").RangeAddress.FirstAddress.ColumnNumber).Value.ToString());

                            component.Description = row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "Description").RangeAddress.FirstAddress.ColumnNumber).Value.ToString();

                            _context.Components.Add(component);

                            _context.SaveChanges();


                            componentRefs.Add(new ComponentRef { ComponentSubd = component.ComponentId, ComponentExcel = int.Parse(row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "ComponentId").RangeAddress.FirstAddress.ColumnNumber).Value.ToString()) }); ;


                        }
                    }
                    if (worksheet.Name == "Category")
                    {
                        foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                        {
                            Category category = new Category();
                            var range = worksheet.RangeUsed();

                            var table = range.AsTable();
                            int ComponentId = componentRefs.FirstOrDefault(c => c.ComponentExcel == int.Parse(row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "ComponentId").RangeAddress.FirstAddress.ColumnNumber).Value.ToString())).ComponentSubd;

                            category.CategoryName = row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "CategoryNam").RangeAddress.FirstAddress.ColumnNumber).Value.ToString();


                            _context.Categories.Add(category);

                            _context.SaveChanges();

                            categoryRefs.Add(new CategoryRef { CategorySubd = category.CategoryId, CategoryExcel = int.Parse(row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "CategoryId").RangeAddress.FirstAddress.ColumnNumber).Value.ToString()) }); ;
                        }
                    }

                    if (worksheet.Name == "Storage")
                    {
                        foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                        {
                            Storage storage = new Storage();

                            var range = worksheet.RangeUsed();

                            var table = range.AsTable();

                            int ComponentId = componentRefs.FirstOrDefault(c => c.ComponentExcel == int.Parse(row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "ComponentId").RangeAddress.FirstAddress.ColumnNumber).Value.ToString())).ComponentSubd;

                            storage.ComponentId = ComponentId;

                            storage.Quantity = Int32.Parse(row.Cell(table.FindColumn(c => c.FirstCell().Value.ToString() == "Quantity").RangeAddress.FirstAddress.ColumnNumber).Value.ToString());

                            _context.Storages.Add(storage);

                            _context.SaveChanges();


                        }
                    }


                }

                return RedirectToAction(nameof(Index));

            }
        }

        public ActionResult ExportFile(int? id)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Component");

                worksheet.Cell(1, 1).Value = "ComponentId";

                worksheet.Cell(1, 2).Value = "ComponentsName";

                worksheet.Cell(1, 3).Value = "YearRelease";

                worksheet.Cell(1, 4).Value = "CategoryId";

                worksheet.Cell(1, 5).Value = "ManufacturerId";

                worksheet.Cell(1, 6).Value = "Description";

                int i = 2;

                foreach (Сomponent item in _context.Components)
                {
                    worksheet.Cell(i, 1).Value = item.ComponentId;
                    worksheet.Cell(i, 2).Value = item.ComponentsName;
                    worksheet.Cell(i, 3).Value = item.YearRelease;
                    worksheet.Cell(i, 4).Value = item.CategoryId;
                    worksheet.Cell(i, 5).Value = item.ManufacturerId;
                    worksheet.Cell(i, 6).Value = item.Description;
                    i++;

                }

                var worksheet1 = workbook.Worksheets.Add("Category");

                worksheet1.Cell(1, 1).Value = "CategoryId";

                worksheet1.Cell(1, 2).Value = "CategoryName";

                

                int i1 = 2;

                foreach (Category item in _context.Categories)
                {
                    worksheet1.Cell(i1, 1).Value = item.CategoryId;
                    worksheet1.Cell(i1, 2).Value = item.CategoryName;
                    i1++;


                }

                var worksheet2 = workbook.Worksheets.Add("Storage");

                worksheet2.Cell(1, 1).Value = "StorageId";

                worksheet2.Cell(1, 2).Value = "ComponentId";

                worksheet2.Cell(1, 3).Value = "Quantity";


                int i2 = 2;

                foreach (Storage item in _context.Storages)
                {
                    worksheet2.Cell(i2, 1).Value = item.StorageId;
                    worksheet2.Cell(i2, 2).Value = item.ComponentId;
                    worksheet2.Cell(i2, 3).Value = item.Quantity;


                    i2++;


                }



                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreedsheetml.sheet")
                    {
                        FileDownloadName = $"fullexport_{DateTime.UtcNow.ToLongDateString()}.xlsx"
                    };
                }
            }

        }
        // GET: OtchetController/Details/5
        public ActionResult Export(int? id)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Otchet");
                worksheet.Cell(1, 1).Value = "Комплектующие";
                worksheet.Cell(1, 2).Value = "Производитель";

                worksheet.Row(1).Style.Font.Bold = true;

                var otch = _context.Components.Where(d => d.ComponentId == id).Select(d => new StorageOtch {nm = d.ComponentsName, kol = d.Manufacturers.ManufacturerPName });
                int i = 2;
                foreach (StorageOtch item in otch)
                {
                    worksheet.Cell(i, 1).Value = item.nm;
                    worksheet.Cell(i, 2).Value = item.kol;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreedsheetml.sheet")
                    {
                        FileDownloadName = $"otchet_{DateTime.UtcNow.ToLongDateString()}.xlsx"
                    };
                }
            }
        }
    }
}

