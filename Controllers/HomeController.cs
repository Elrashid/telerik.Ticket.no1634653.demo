using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using telerik.Ticket.no1634653.demo.Models;

namespace telerik.Ticket.no1634653.demo.Controllers
{

    public static class DataStore
    { 

        public static List<Location> locations = new List<Location>() {
        new Location(){ LocationId=500, LocationName="5th Flower",ParentId=null},
        new Location(){ LocationId=600, LocationName="6th Flower",ParentId=null},
        new Location(){ LocationId=501, LocationName="Office 501",ParentId=500},
        new Location(){ LocationId=502, LocationName="Office 502",ParentId=500},
        new Location(){ LocationId=601, LocationName="Office 601",ParentId=600},
        new Location(){ LocationId=602, LocationName="Office 602",ParentId=600},
        };
        public static List<FixAsset> fixAssets = new List<FixAsset>() { 
        new FixAsset(){ AssetId=1,LocationId=501, AssetName="Desktop Computer",ParentId=null},
        new FixAsset(){ AssetId=50102,LocationId=501, AssetName="LCD Screen",ParentId=50101},
        new FixAsset(){ AssetId=50103,LocationId=501, AssetName="Computer Tower",ParentId=50101},
        };
         
    }
    public class HomeController : Controller
    {
  

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        public JsonResult LocationId_DropDown_Read(int? id)
        {

            var employees = from e in DataStore.locations
                            where (id.HasValue ? e.ParentId == id : e.ParentId == null)
                            select new  
                            {
                                Id = e.LocationId.ToString(),
                                Text = e.LocationName  ,
                                HasChildren = (from q in DataStore.locations
                                               where (q.ParentId == e.LocationId)
                                               select q
                                               ).Count() > 0
                            };

            return Json(employees.ToList());

        }


        public JsonResult FixAsset_All([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var result = DataStore.fixAssets.ToTreeDataSourceResult(request,
                e => e.AssetId,
                e => e.ParentId,
                e => id.HasValue ? e.ParentId == id : e.ParentId == null,
                e => e
            );

            return Json(result);
        }



        public JsonResult FixAsset_Destroy([DataSourceRequest] DataSourceRequest request, FixAsset modle)
        {
            if (ModelState.IsValid)
            {
                DataStore.fixAssets.RemoveAll(r => r.AssetId == modle.AssetId);
            }

            return Json(new[] { modle }.ToTreeDataSourceResult(request, ModelState));
        }

        public JsonResult FixAsset_Create([DataSourceRequest] DataSourceRequest request, FixAsset modle)
        {
            if (ModelState.IsValid)
            {
                DataStore.fixAssets.Add(modle);

            }

            return Json(new[] { modle }.ToTreeDataSourceResult(request, ModelState));
        }

        public JsonResult FixAsset_Update([DataSourceRequest] DataSourceRequest request, FixAsset modle)
        {
            if (ModelState.IsValid)
            {
                DataStore.fixAssets.RemoveAll(r=>r.AssetId == modle.AssetId);
                DataStore.fixAssets.Add(modle);
            }

            return Json(new[] { modle }.ToTreeDataSourceResult(request, ModelState));
        }



        public JsonResult Location_All([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var result = DataStore.locations.ToTreeDataSourceResult(request,
                e => e.LocationId,
                e => e.ParentId,
                e => id.HasValue ? e.ParentId == id : e.ParentId == null,
                e => e
            );

            return Json(result);
        }



        public JsonResult Location_Destroy([DataSourceRequest] DataSourceRequest request, Location modle)
        {
            if (ModelState.IsValid)
            {
                DataStore.locations.RemoveAll(r => r.LocationId == modle.LocationId);
            }

            return Json(new[] { modle }.ToTreeDataSourceResult(request, ModelState));
        }

        public JsonResult Location_Create([DataSourceRequest] DataSourceRequest request, Location modle)
        {
            if (ModelState.IsValid)
            {
                DataStore.locations.Add(modle);

            }

            return Json(new[] { modle }.ToTreeDataSourceResult(request, ModelState));
        }

        public JsonResult Location_Update([DataSourceRequest] DataSourceRequest request, Location modle)
        {
            if (ModelState.IsValid)
            {
                DataStore.locations.RemoveAll(r => r.LocationId == modle.LocationId);
                DataStore.locations.Add(modle);
            }

            return Json(new[] { modle }.ToTreeDataSourceResult(request, ModelState));
        }


    }
}