using System.Linq;
using System.Web.Mvc;
using QuanLyNhaSach.Models;

namespace QuanLyNhaSach.Controllers
{
    public class KhachHangController : Controller
    {
        QLSachModel db = new QLSachModel();

        // GET: DangKy
        public ActionResult DangKy()
        {
            return View();
        }

        // POST: DangKy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Exclude = "MAKH")] KHACHHANG kh) 
        {
            if (ModelState.IsValid)
            {
                
                var check = db.KHACHHANGs.FirstOrDefault(k => k.TAIKHOAN == kh.TAIKHOAN);
                if (check == null)
                {
                    
                    int count = db.KHACHHANGs.Count() + 1;
                    kh.MAKH = "KH" + count.ToString("D3"); 

                    db.KHACHHANGs.Add(kh);
                    db.SaveChanges(); 

                    
                    return RedirectToAction("DangNhap");
                }
                else
                {
                    ViewBag.Error = "Tài khoản này đã tồn tại!";
                    return View();
                }
            }
            return View();
        }

        // GET: DangNhap
        public ActionResult DangNhap()
        {
            return View();
        }

        // POST: DangNhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(FormCollection collection)
        {
            var taiKhoan = collection["TaiKhoan"];
            var matKhau = collection["MatKhau"];

            if (string.IsNullOrEmpty(taiKhoan))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
              
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(k => k.TAIKHOAN == taiKhoan && k.MATKHAU == matKhau);

                if (kh != null)
                {
                    
                    Session["TaiKhoan"] = kh;
                    Session["TenKhachHang"] = kh.TENKH;

                    return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        // GET: DangXuat
        public ActionResult DangXuat()
        {
            Session.Clear(); 
            return RedirectToAction("Index", "Home");
        }
    }
}