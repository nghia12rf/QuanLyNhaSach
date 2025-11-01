using System.Linq;
using System.Web.Mvc;
using QuanLyNhaSach.Models; 

namespace QuanLyNhaSach.Controllers
{
    public class LayoutController : Controller
    {

        QLSachModel db = new QLSachModel();

        [ChildActionOnly] 
        public ActionResult ChuDePartial()
        {
            var listChuDe = db.CHUDEs.ToList();
            return PartialView(listChuDe);
        }

        [ChildActionOnly]
        public ActionResult NxbPartial()
        {
            var listNXB = db.NHAXUATBANs.ToList();
            return PartialView(listNXB);
        }
    }
}