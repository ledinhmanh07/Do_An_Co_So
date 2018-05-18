﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HutechWork.Models;
namespace HutechWork.Controllers
{
    public class NguoiDungController : Controller
    {
        dbQlHutechWorkDataContext db = new dbQlHutechWorkDataContext();
        // GET: NguoiDung
        public bool KiemtraMssv(string value)
        {
            Regex isValidInput = new Regex(@"^\d{10,11}$");
            if (!isValidInput.IsMatch(value))
                return false;
            return true;
        }
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
            if (KiemtraMssv(tendn) == false)
                ViewData["Loi1"] = "Mã số sinh viên không hợp lệ !!";
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
                        Session["TaikhoanCN"] = tk;
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
            if (KiemtraMssv(tendn) == false)
                ViewData["Loi3"] = "Mã số sinh viên không hợp lệ !!";
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
                    ViewData["Loi3"] = "Mã số sinh viên này đã đăng ký tài khoản !!";
                }
           }
        return this.DangKy();
        }      
    }
}