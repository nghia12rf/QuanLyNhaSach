using System.Linq;
using System.Net;
using System.Web.Mvc;
using QuanLyNhaSach.Models; // Đảm bảo đã using thư mục Models

namespace QuanLyNhaSach.Controllers
{
    public class SachController : Controller
    {
        
        QLSachModel db = new QLSachModel();

        
        public ActionResult SachTheoChuDe(string id)
        {
            
            var chuDe = db.CHUDEs.SingleOrDefault(c => c.MACHUDE == id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }

            
            var listSach = db.SACHes.Where(s => s.MACHUDE == id).OrderBy(s => s.TENSACH).ToList();

            
            ViewBag.TenChuDe = chuDe.TENCHUDE;

            if (listSach.Count == 0)
            {
                ViewBag.ThongBao = "Không có sách nào thuộc chủ đề này.";
            }

            return View(listSach);
        }

        
        public ActionResult SachTheoNXB(string id)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(c => c.MANXB == id);
            if (nxb == null)
            {
                return HttpNotFound();
            }
            var listSach = db.SACHes.Where(s => s.MANXB == id).OrderBy(s => s.TENSACH).ToList();
            ViewBag.TenNXB = nxb.TENNXB;
            if (listSach.Count == 0)
            {
                ViewBag.ThongBao = "Không có sách nào thuộc nhà xuất bản này.";
            }
            return View(listSach);
        }

        public ActionResult XemChiTiet(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // 1. Lấy sách chính
            SACH sach = db.SACHes.Find(id);

            if (sach == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.SachCungChuDe = db.SACHes
                .Where(s => s.MACHUDE == sach.MACHUDE && s.MASACH != id)
                .Take(4) 
                .ToList();

            
            ViewBag.SachCungNXB = db.SACHes
                .Where(s => s.MANXB == sach.MANXB && s.MASACH != id)
                .Take(4) 
                .ToList();
            

            return View(sach); 
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