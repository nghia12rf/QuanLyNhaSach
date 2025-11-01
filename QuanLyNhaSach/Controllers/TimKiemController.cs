using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using QuanLyNhaSach.Models;

namespace QuanLyNhaSach.Controllers
{
    public class TimKiemController : Controller
    {
        QLSachModel db = new QLSachModel();

        [ChildActionOnly]
        public ActionResult TimKiemPartial()
        {
            ViewBag.ListChuDe = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TENCHUDE), "MACHUDE", "TENCHUDE");
            return PartialView();
        }

        public ActionResult KetQuaTimKiem(string sTuKhoa, string MaChuDe, List<string> gia)
        {
            
            var ketQua = db.SACHes.AsQueryable();

            
            if (!string.IsNullOrEmpty(sTuKhoa))
            {
                ketQua = ketQua.Where(s => s.TENSACH.Contains(sTuKhoa));
            }

            
            if (!string.IsNullOrEmpty(MaChuDe))
            {
                ketQua = ketQua.Where(s => s.MACHUDE == MaChuDe);
            }

            
            if (gia != null && gia.Count > 0)
            {
                
                var tempKetQua = db.SACHes.Where(s => false); // Dòng mới

                foreach (var priceRange in gia)
                {
                    switch (priceRange)
                    {
                        
                        case "1": // 0 - 10.000
                            tempKetQua = tempKetQua.Union(ketQua.Where(s => s.GIABAN >= 0 && s.GIABAN <= 10000));
                            break;
                        case "2": // 11.000 - 20.000
                            tempKetQua = tempKetQua.Union(ketQua.Where(s => s.GIABAN >= 11000 && s.GIABAN <= 20000));
                            break;
                        case "3": // 21.000 - 40.000
                            tempKetQua = tempKetQua.Union(ketQua.Where(s => s.GIABAN >= 21000 && s.GIABAN <= 40000));
                            break;
                        case "4": // Lớn hơn 40.000
                            tempKetQua = tempKetQua.Union(ketQua.Where(s => s.GIABAN > 40000));
                            break;
                    }
                }

                
                ketQua = tempKetQua;
            }

            
            var listSach = ketQua.OrderBy(s => s.TENSACH).ToList();

           
            ViewBag.TuKhoa = sTuKhoa;
            ViewBag.Gia = gia ?? new List<string>();

            
            if (!string.IsNullOrEmpty(MaChuDe))
            {
                ViewBag.TenChuDe = db.CHUDEs.Find(MaChuDe)?.TENCHUDE;
            }
            else
            {
                ViewBag.TenChuDe = "Tất cả";
            }

            if (listSach.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sách nào phù hợp.";
            }

            return View(listSach);
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
