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
            // 1. Lấy tất cả sách (dưới dạng IQueryable để lọc)
            var ketQua = db.SACHes.AsQueryable();

            // 2. Lọc theo Từ khóa (Tên sách)
            if (!string.IsNullOrEmpty(sTuKhoa))
            {
                ketQua = ketQua.Where(s => s.TENSACH.Contains(sTuKhoa));
            }

            // 3. Lọc theo Chủ đề
            if (!string.IsNullOrEmpty(MaChuDe))
            {
                ketQua = ketQua.Where(s => s.MACHUDE == MaChuDe);
            }

            // 4. Lọc theo Khoảng giá (Logic OR)
            if (gia != null && gia.Count > 0)
            {
                // === SỬA 1: BẮT ĐẦU BẰNG MỘT QUERY RỖNG TỪ DATABASE ===
                // Dòng cũ (gây lỗi): var tempKetQua = Enumerable.Empty<SACH>().AsQueryable();
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

                // === SỬA 3: GÁN KẾT QUẢ, KHÔNG CẦN '.Distinct()' VÌ 'Union' ĐÃ LÀM ===
                ketQua = tempKetQua;
            }

            // Dòng này bây giờ sẽ chạy đúng
            var listSach = ketQua.OrderBy(s => s.TENSACH).ToList();

            // Lưu lại các tiêu chí tìm kiếm để hiển thị
            ViewBag.TuKhoa = sTuKhoa;
            ViewBag.Gia = gia ?? new List<string>();

            // Lấy Tên chủ đề để hiển thị
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