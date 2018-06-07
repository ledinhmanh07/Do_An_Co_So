using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HutechWork.Models;
using System.IO;

namespace HutechWork.Controllers
{
    public class NguoiDungController : Controller
    {
        dbQlHutechWorkDataContext db = new dbQlHutechWorkDataContext();
        // GET: NguoiDung
        public bool Kiemtramatkhau(string value)
        {
            Regex isValidInput = new Regex(@"^\w{6,20}");
            if (!isValidInput.IsMatch(value))
                return false;
            return true;
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
                    TAIKHOAN_CN tk = db.TAIKHOAN_CNs.SingleOrDefault(n => n.MASV == tendn && n.PASS == matkhau);
                    if(tk != null)
                    {
                        ViewBag.Thongbao = "Đăng nhập thành công !!";
                        Session["TaikhoanCN"] = tk.MATKCN;
                        return RedirectToAction("Index", "TrangChu");
                    }
                    else
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng !!";
                }
            }
            return this.DangNhap();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection form,TAIKHOAN_CN tk)
        {
            var tendn = form["TenDN"];
            var matkhau = form["Matkhau"];
            var nhaplaimatkhau = form["Nhaplaimatkhau"];
            if (Kiemtramatkhau(tendn) == false)
                ViewData["Loi3"] = "Tên đăng nhập từ 6-20 kí tự (a-z, A-Z, kí tự _) !!";
            else
            {
                TAIKHOAN_CN tk1 = db.TAIKHOAN_CNs.SingleOrDefault(n => n.MASV == tendn);
                if (tk1 == null)
                {
                    if (Kiemtramatkhau(matkhau) == false)
                        ViewData["Loi4"] = "Mật khẩu từ 6-20 kí tự (a-z, A-Z, kí tự _) !!";
                    else
                    {
                        if (String.Compare(matkhau, nhaplaimatkhau, false) != 0)
                            ViewData["Loi5"] = "Mật khẩu nhập lại không trùng !!";
                        else
                        {
                            tk.MASV = tendn;
                            tk.PASS = matkhau;
                            db.TAIKHOAN_CNs.InsertOnSubmit(tk);
                            db.SubmitChanges();
                            return RedirectToAction("DangNhap", "NguoiDung");
                        }
                    }
                }
                else
                {
                    ViewData["Loi3"] = "Tên đăng nhập này đã đăng ký tài khoản !!";
                }
           }
        return this.DangKy();
        }      
        public ActionResult DangXuat()
        {
            Session["TaikhoanCN"] = null;
            return RedirectToAction("Index", "TrangChu");
        }
        public ActionResult Chitietcanhan()
        {
            TAIKHOAN_CN tk = db.TAIKHOAN_CNs.SingleOrDefault(n => n.MATKCN.ToString() == Session["TaikhoanCN"].ToString());
            return View(tk);
        }
        public ActionResult Diadiem()
        {
            var matp = db.THONGTINCANHANs.SingleOrDefault(n => n.MATKCN == int.Parse(Session["TaikhoanCN"].ToString())).MATHANHPHO;
            var thanhpho = from tp in db.THANHPHOs where tp.MATHANHPHO != matp select tp;
            return PartialView(thanhpho);
        }

        //Sửa thông tin cá nhân
        [HttpGet]
        public ActionResult Suathongtin()
        {
            THONGTINCANHAN tt = db.THONGTINCANHANs.SingleOrDefault(n => n.MATKCN.ToString() == Session["TaikhoanCN"].ToString());
            return View(tt);
        }
        [HttpPost]
        public ActionResult Suathongtin(FormCollection fr, HttpPostedFileBase fileUpload)
        {
            var ho = fr["ho"];
            var ten = fr["ten"];
            var gender = fr["gender"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", fr["ngaysinh"]);
            var email = fr["email"];
            var sdt = fr["sdt"];
            var thanhpho = fr["thanhpho"];
            var diachi = fr["diachi"];
            int tp = db.THANHPHOs.SingleOrDefault(n => n.TENTHANHPHO == thanhpho).MATHANHPHO;
            var tk1 = db.THONGTINCANHANs.SingleOrDefault(n => n.MATKCN == int.Parse(Session["TaikhoanCN"].ToString()));
            if (fileUpload == null)
            {
                tk1.HO = ho;
                tk1.TEN = ten;
                tk1.GIOITINH = gender;
                tk1.NGAYSINH = DateTime.Parse(ngaysinh);
                tk1.EMAIL = email;
                tk1.SDT = sdt;
                tk1.MATHANHPHO = tp;
                tk1.DIACHI = diachi;
                db.SubmitChanges();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/img"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    tk1.HO = ho;
                    tk1.TEN = ten;
                    tk1.GIOITINH = gender;
                    tk1.NGAYSINH = DateTime.Parse(ngaysinh);
                    tk1.EMAIL = email;
                    tk1.SDT = sdt;
                    tk1.MATHANHPHO = tp;
                    tk1.DIACHI = diachi;                    
                    tk1.HINHDD = fileName;
                    db.SubmitChanges();
                }
            }
                return RedirectToAction("Chitietcanhan", "NguoiDung");
        }

        public ActionResult Nganh()
        {
            var manganh = db.HOSOXINVIECs.SingleOrDefault(n => n.MATKCN == int.Parse(Session["TaikhoanCN"].ToString())).MANGANH;
            var nganh = from tp in db.NGANHs where tp.MANGANH != manganh select tp;
            return PartialView(nganh);
        }
        //Sửa hồ sơ cá nhân
        [HttpGet]
        public ActionResult Suahoso()
        {
            HOSOXINVIEC tt = db.HOSOXINVIECs.SingleOrDefault(n => n.MATKCN.ToString() == Session["TaikhoanCN"].ToString());
            return View(tt);
        }
        [HttpPost]
        public ActionResult Suahoso(FormCollection fr)
        {
            var nganhnghe = fr["nganhnghe"];
            var hocvan = fr["hocvan"];
            var kinhnghiem = fr["kinhnghiem"];
            var kynang = fr["kynang"];
            var ngoaingu = fr["ngoaingu"];
            float luongmm = float.Parse(fr["luongmm"]);
            int nn = db.NGANHs.SingleOrDefault(n => n.TENNGANH == nganhnghe).MANGANH;
            var tk1 = db.HOSOXINVIECs.SingleOrDefault(n => n.MATKCN == int.Parse(Session["TaikhoanCN"].ToString()));
            tk1.MANGANH = nn;
            tk1.HOCVAN = hocvan;
            tk1.KINHNGHIEM = kinhnghiem;
            tk1.KYNANG = kynang;
            tk1.NGOAINGU = ngoaingu;
            tk1.LUONGMM = luongmm;
            db.SubmitChanges();
            return RedirectToAction("Chitietcanhan", "NguoiDung");
        }
    }
}