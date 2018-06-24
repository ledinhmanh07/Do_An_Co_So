using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HutechWork.Models;
using System.Text.RegularExpressions;

namespace HutechWork.Controllers
{
    public class AdminController : Controller
    {
        dbQlHutechWorkDataContext db = new dbQlHutechWorkDataContext();
        // GET: Admin
        public bool Kiemtramatkhau(string value)
        {
            Regex isValidInput = new Regex(@"^\w{6,20}");
            if (!isValidInput.IsMatch(value))
                return false;
            return true;
        }
        public ActionResult Index()
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.CHITIETTUYENDUNGs.Where(n => n.TINHTRANG == false).ToList();
            return View(a);
        }
        public ActionResult ChiTiet(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == id);
            return View(a);
        }
        [HttpPost, ActionName("ChiTiet")]
        public ActionResult Duyet(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == id);
            a.TINHTRANG = true;
            db.SubmitChanges();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult Xoa(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == id);
            return View(a);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == id);
            var b = db.PHIEUDANGTUYENs.SingleOrDefault(n => n.MAPDT == id);
            db.CHITIETTUYENDUNGs.DeleteOnSubmit(a);
            db.SubmitChanges();
            db.PHIEUDANGTUYENs.DeleteOnSubmit(b);
            db.SubmitChanges();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult TaikhoanCN()
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = from b in db.TAIKHOAN_CNs select b;
            return View(a);
        }
        public ActionResult ChiTietCN(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.TAIKHOAN_CNs.SingleOrDefault(n => n.MATKCN == id);
            return View(a);
        }
        public ActionResult XoaCN(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.TAIKHOAN_CNs.SingleOrDefault(n => n.MATKCN == id);
            return View(a);
        }
        [HttpPost, ActionName("XoaCN")]
        public ActionResult XacNhanXoaCN(int id)
        { 
            var a = db.TAIKHOAN_CNs.SingleOrDefault(n => n.MATKCN == id);
            var b = db.THONGTINCANHANs.SingleOrDefault(n => n.MATKCN == id);
            var c = db.HOSOXINVIECs.SingleOrDefault(n => n.MATKCN == id);
            var d = db.NOPDONs.SingleOrDefault(n => n.MATKCN == id);
            db.THONGTINCANHANs.DeleteOnSubmit(b);
            db.SubmitChanges();
            if (c != null)
            {
                db.HOSOXINVIECs.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
            if(d != null)
            {
                db.NOPDONs.DeleteOnSubmit(d);
                db.SubmitChanges();
            }
            db.TAIKHOAN_CNs.DeleteOnSubmit(a);
            db.SubmitChanges();
            return RedirectToAction("TaiKhoanCN", "Admin");
        }
        public ActionResult TaiKhoanDN()
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = from b in db.TAIKHOAN_DNs select b;
            return View(a);
        }
        public ActionResult ChiTietDN(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.TAIKHOAN_DNs.SingleOrDefault(n => n.MATKDN == id);
            return View(a);
        }
        public ActionResult XoaDN(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.TAIKHOAN_DNs.SingleOrDefault(n => n.MATKDN == id);
            return View(a);
        }
        [HttpPost, ActionName("XoaDN")]
        public ActionResult XacNhanXoaDN(int id)
        {
            var a = db.TAIKHOAN_DNs.SingleOrDefault(n => n.MATKDN == id);
            var b = db.TTDOANHNGHIEPs.SingleOrDefault(n => n.MATKDN == id);
            var c= db.PHIEUDANGTUYENs.Where(n => n.MATKDN == id).ToList();
            db.TTDOANHNGHIEPs.DeleteOnSubmit(b);
            db.SubmitChanges();
            if (c != null)
            {
                foreach (var item in c)
                {
                    var d = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == item.MAPDT);
                    db.CHITIETTUYENDUNGs.DeleteOnSubmit(d);
                    db.SubmitChanges();
                    db.PHIEUDANGTUYENs.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
            }

            db.TAIKHOAN_DNs.DeleteOnSubmit(a);
            db.SubmitChanges();
            return RedirectToAction("TaiKhoanDN", "Admin");
        }
        public ActionResult QuanLyDT()
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.CHITIETTUYENDUNGs.ToList();
            return View(a);
        }
        public ActionResult ChiTietDT(int id)
        {
            if (Session["ADMIN"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            var a = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == id);
            return View(a);
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection form)
        {
            var tendn = form["TenDN"];
            var matkhau = form["Matkhau"];
            if (Kiemtramatkhau(tendn) == false)
                ViewData["Loi1"] = "Tên đăng nhập từ 6-20 kí tự (a-z, A-Z, kí tự _) !!";
            else
            {
                if (Kiemtramatkhau(matkhau) == false)
                    ViewData["Loi2"] = "Mật khẩu từ 6-20 kí tự (a-z, A-Z, kí tự _) !!";
                else
                {
                    ADMIN tk = db.ADMINs.SingleOrDefault(n => n.ID == tendn && n.PASS == matkhau);
                    if (tk != null)
                    {
                        ViewBag.Thongbao = "Đăng nhập thành công !!";
                        Session["ADMIN"] = tk.ID;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng !!";
                }
            }
            return this.DangNhap();
        }
        public ActionResult DangXuat()
        {
            Session["ADMIN"] = null;
            return RedirectToAction("DangNhap", "Admin");
        }
    }
}