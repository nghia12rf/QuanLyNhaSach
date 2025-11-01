using System.Linq;
using System.Web.Mvc;
using QuanLyNhaSach.Models; // <-- THÊM USING NÀY

namespace QuanLyNhaSach.Controllers
{
    public class HomeController : Controller
    {
        // Khởi tạo DbContext
        QLSachModel db = new QLSachModel();

        public ActionResult Index()
        {
            
            var listSach = db.SACHes.OrderByDescending(s => s.NGAYCAPNHAT).ToList();

            

            return View(listSach); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}