using HutechWork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HutechWork.Controllers
{
    public class NhaTuyenDungController : Controller
    {
        dbQlHutechWorkDataContext db = new dbQlHutechWorkDataContext();
        // GET: NhaTuyenDung
        public ActionResult Index()
        {
            Session["nganhnghe"] = "";
            Session["diadiem"] = "";
            return View();
        }

        [HttpGet]
        public ActionResult Timkiemhoso(int? page)
        {
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (Session["nganhnghe"].ToString().Length != 0)
            {
                if (Session["diadiem"].ToString().Length != 0)
                {
                    var dt =
                       from a in db.TAIKHOAN_CNs
                       where a.THONGTINCANHAN.MATHANHPHO.ToString() == Session["diadiem"].ToString()
                       && a.HOSOXINVIEC.MANGANH.ToString() == Session["nganhnghe"].ToString()
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var dt =
                       from a in db.TAIKHOAN_CNs
                       where a.HOSOXINVIEC.MANGANH.ToString() == Session["nganhnghe"].ToString()
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
            }
            else
            {
                if (Session["diadiem"].ToString().Length != 0)
                {
                    var dt =
                       from a in db.TAIKHOAN_CNs
                       where a.THONGTINCANHAN.MATHANHPHO.ToString() == Session["diadiem"].ToString()
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var dt =
                       from a in db.TAIKHOAN_CNs
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
            }
        }
        [HttpPost]
        public ActionResult Timkiemhoso(int? page, FormCollection collection)
        {
            Session["nganhnghe"] = collection["nganhnghe"];
            Session["diadiem"] = collection["diadiem"];
            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (collection["nganhnghe"].ToString().Length != 0)
            {
                if (collection["diadiem"].ToString().Length != 0)
                {
                    var dt =
                       from a in db.TAIKHOAN_CNs
                       where a.THONGTINCANHAN.MATHANHPHO.ToString() == collection["diadiem"].ToString()
                       && a.HOSOXINVIEC.MANGANH.ToString() == collection["nganhnghe"].ToString()
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var dt =
                       from a in db.TAIKHOAN_CNs
                       where a.HOSOXINVIEC.MANGANH.ToString() == collection["nganhnghe"].ToString()
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
            }
            else
            {
                if (collection["diadiem"].ToString().Length != 0)
                {
                    var dt =
                       from a in db.TAIKHOAN_CNs
                       where a.THONGTINCANHAN.MATHANHPHO.ToString() == collection["diadiem"].ToString()
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var dt =
                       from a in db.TAIKHOAN_CNs                       
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
            }
        }
        public ActionResult ChitietHS(int id)
        {
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            TAIKHOAN_CN tk = db.TAIKHOAN_CNs.SingleOrDefault(n => n.MATKCN.ToString() == id.ToString());
            return View(tk);
        }
        public bool Kiemtra(string value)
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
            if (Kiemtra(tendn) == false)
                ViewData["Loi1"] = "Mật khẩu từ 6-20 kí tự (a-z, A-Z, kí tự _) !!";
            else
            {
                if (Kiemtra(matkhau) == false)
                    ViewData["Loi2"] = "Mật khẩu từ 6-20 kí tự (a-z, A-Z, kí tự _) !!";
                else
                {
                    TAIKHOAN_DN tk = db.TAIKHOAN_DNs.SingleOrDefault(n => n.ID == tendn && n.PASS == matkhau);
                    if (tk != null)
                    {
                        ViewBag.Thongbao = "Đăng nhập thành công !!";
                        Session["TaikhoanDN"] = tk.MATKDN;
                        return RedirectToAction("Index", "NhaTuyenDung");
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
        public ActionResult DangKy(FormCollection form, TAIKHOAN_DN tk)
        {
            var tendn = form["TenDN"];
            var matkhau = form["Matkhau"];
            var tendoanhnghiep = form["tendoanhnghiep"];
            var diadiem = form["diadiem"];
            var nhaplaimatkhau = form["Nhaplaimatkhau"];
            if (Kiemtra(tendn) == false)
                ViewData["Loi3"] = "Mật khẩu từ 6-20 kí tự (a-z, A-Z, kí tự _) !!";
            else
            {
                TAIKHOAN_DN tk1 = db.TAIKHOAN_DNs.SingleOrDefault(n => n.ID == tendn);
                if (tk1 == null)
                {
                    if (Kiemtra(matkhau) == false)
                        ViewData["Loi4"] = "Mật khẩu từ 6-20 kí tự (a-z, A-Z, kí tự _) !!";
                    else
                    {
                        if (String.Compare(matkhau, nhaplaimatkhau, false) != 0)
                            ViewData["Loi5"] = "Mật khẩu nhập lại không trùng !!";
                        else
                        {
                            if (String.IsNullOrEmpty(tendoanhnghiep) == true)
                            {
                                ViewData["Loi6"] = "Tên doanh nghiệp không được bỏ trống !!";
                            }
                            else
                            {
                                tk.ID = tendn;
                                tk.PASS = matkhau;
                                db.TAIKHOAN_DNs.InsertOnSubmit(tk);
                                db.SubmitChanges();
                                TTDOANHNGHIEP a = new TTDOANHNGHIEP();
                                a.MATKDN = db.TAIKHOAN_DNs.SingleOrDefault(n => n.ID == tendn && n.PASS == matkhau).MATKDN;
                                a.TENDN = tendoanhnghiep;
                                a.MATHANHPHO = int.Parse(diadiem);
                                db.TTDOANHNGHIEPs.InsertOnSubmit(a);
                                db.SubmitChanges();
                                return RedirectToAction("DangNhap", "NhaTuyenDung");
                            }
                        }
                    }
                }
                else
                {
                    ViewData["Loi3"] = "Tài khoản này đã được đăng ký!!";
                }
            }
            return this.DangKy();
        }
        public ActionResult DangXuat()
        {
            Session["TaikhoanDN"] = null;
            return RedirectToAction("Index", "NhaTuyenDung");
        }

        public ActionResult Thongtindoanhnghiep()
        {
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            var tk1 = db.TTDOANHNGHIEPs.SingleOrDefault(n => n.MATKDN == int.Parse(Session["TaikhoanDN"].ToString()));
            return View(tk1);
        }
        public ActionResult ChiTiet()
        {
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            var tk1 = db.TTDOANHNGHIEPs.SingleOrDefault(n => n.MATKDN == int.Parse(Session["TaikhoanDN"].ToString()));
            return View(tk1);
        }
        [HttpPost, ActionName("ChiTiet")]
        [ValidateInput(false)]
        public ActionResult ChiTiet(FormCollection fr, HttpPostedFileBase fileUpload)
        {
            var a = fr["thongtin"];
            var b = fr["tendn"];
            var c = fr["quymo"];
            var d = fr["diachi"];
            var e = fr["phucloi"];
            var f = fr["thanhpho"];
            int tp = db.THANHPHOs.SingleOrDefault(n => n.TENTHANHPHO == f).MATHANHPHO;
            var tk1 = db.TTDOANHNGHIEPs.SingleOrDefault(n => n.MATKDN == int.Parse(Session["TaikhoanDN"].ToString()));
            if (fileUpload == null)
            {
                tk1.THONGTINDN = a;
                tk1.TENDN = b;
                tk1.QUYMODN = int.Parse(c);
                tk1.DIACHI = d;
                tk1.PHUCLOIDN = e;
                if (tp != 0)
                {
                    tk1.MATHANHPHO = tp;
                }
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
                    tk1.THONGTINDN = a;
                    tk1.TENDN = b;
                    tk1.QUYMODN = int.Parse(c);
                    tk1.DIACHI = d;
                    tk1.PHUCLOIDN = e;
                    if (tp != 0)
                    {
                        tk1.MATHANHPHO = tp;
                    }
                    tk1.LOGO = fileName;
                    db.SubmitChanges();
                }
            }
            return RedirectToAction("Index", "NhaTuyenDung");
        }
        public ActionResult Diadiem()
        {
            var matp = db.TTDOANHNGHIEPs.SingleOrDefault(n => n.MATKDN == int.Parse(Session["TaikhoanDN"].ToString())).MATHANHPHO;
            var thanhpho = from tp in db.THANHPHOs where tp.MATHANHPHO != matp select tp;
            return PartialView(thanhpho);
        }
        public ActionResult Diadiem1(int id)
        {
            var matp = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == id).MATHANHPHO;
            var thanhpho = from tp in db.THANHPHOs where tp.MATHANHPHO != matp select tp;
            return PartialView(thanhpho);
        }
        public ActionResult NganhNghe1(int id)
        {
            var mann = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == id).MANGANH;
            var nn = from tp in db.NGANHs where tp.MANGANH != mann select tp;
            return PartialView(nn);
        }
        public ActionResult DangTuyen()
        {
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DangTuyen(FormCollection fr)
        {
            var tieude = fr["tieude"];
            var chucdanh = fr["chucdanh"];
            var capbac = fr["capbac"];
            var nganhnghe = fr["nganhnghe"];
            var thanhpho = fr["thanhpho"];
            var diachi1 = fr["diachi"];
            var mucluongtoithieu = fr["mucluongtoithieu"];
            var mucluongtoida = fr["mucluongtoida"];
            var mota = fr["mota"];
            var yeucau = fr["yeucau"];
            var kynang = fr["kynang"];
            var nguoilienhe = fr["nguoilienhe"];
            var email = fr["email"];
            var thoihan = String.Format("{0:MM/dd/yyyy}", fr["thoihan"]);
            PHIEUDANGTUYEN pdt = new PHIEUDANGTUYEN();
            pdt.MATKDN = int.Parse(Session["TaikhoanDN"].ToString());
            pdt.NGAYDANGTIN = DateTime.Now;
            pdt.THOIHANDANGTIN = DateTime.Parse(thoihan);
            db.PHIEUDANGTUYENs.InsertOnSubmit(pdt);
            db.SubmitChanges();
            PHIEUDANGTUYEN mapdt = db.PHIEUDANGTUYENs.OrderByDescending(a => a.MAPDT).Take(1).Single();
            CHITIETTUYENDUNG ct = new CHITIETTUYENDUNG();
            ct.MAPDT = mapdt.MAPDT;
            ct.KYNANG = kynang;
            ct.EMAILLIENHE = email;
            ct.CAPBAC = capbac;
            ct.CHUCDANH = chucdanh;
            ct.DIACHILAMVIEC = diachi1;
            ct.MOTACV = mota;
            ct.MUCLUONGTOIDA = Double.Parse(mucluongtoida);
            ct.MUCLUONGTOITHIEU = Double.Parse(mucluongtoithieu);
            ct.NGUOILIENHE = nganhnghe;
            ct.TIEUDE = tieude;
            ct.YEUCAUCV = yeucau;
            ct.MANGANH = int.Parse(nganhnghe);
            ct.MATHANHPHO = int.Parse(thanhpho);
            ct.TINHTRANG = false;
            db.CHITIETTUYENDUNGs.InsertOnSubmit(ct);
            db.SubmitChanges();
            return RedirectToAction("Index", "NhaTuyenDung");
        }
        public ActionResult Vieclamdadang()
        {
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            List<PHIEUDANGTUYEN> ct = db.PHIEUDANGTUYENs.OrderByDescending(a => a.NGAYDANGTIN).Where(b => b.MATKDN.ToString() == Session["TaikhoanDN"].ToString() && b.CHITIETTUYENDUNG.TINHTRANG == true).ToList();
            return PartialView(ct);
        }
        public ActionResult Vieclamchuadang()
        {
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            List<PHIEUDANGTUYEN> ct = db.PHIEUDANGTUYENs.OrderByDescending(a => a.NGAYDANGTIN).Where(b => b.MATKDN.ToString() == Session["TaikhoanDN"].ToString() && b.CHITIETTUYENDUNG.TINHTRANG == false).ToList();
            return PartialView(ct);
        }
        public ActionResult Hosoxettuyen(int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            var ds = db.NOPDONs.OrderByDescending(a => a.NGAYNOPDON).Where(b => b.PHIEUDANGTUYEN.MATKDN.ToString() == Session["TaikhoanDN"].ToString()).ToList();                
            return View(ds.ToPagedList(pageNum, pageSize));
        }
        public ActionResult ChiTietDT(int id)
        {
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            var sp = from s in db.PHIEUDANGTUYENs where s.MAPDT == id select s;
            return View(sp.Single());
        }
        public ActionResult SuaDT(int id)
        {
            if (Session["TaikhoanDN"] == null)
            {
                return RedirectToAction("DangNhap", "NhaTuyenDung");
            }
            var a = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == id);
            return View(a);
        }
        [HttpPost, ActionName("SuaDT")]
        public ActionResult SuaDT(int id, FormCollection fr)
        {
            var a = db.CHITIETTUYENDUNGs.SingleOrDefault(n => n.MAPDT == id);
            a.TIEUDE = fr["tieude"];
            a.CHUCDANH = fr["chucdanh"];
            a.CAPBAC = fr["capbac"];
            a.DIACHILAMVIEC = fr["diachi"];
            var nganhnghe = fr["nganhnghe"];
            int tp1 = db.NGANHs.SingleOrDefault(n => n.TENNGANH == nganhnghe).MANGANH;
            a.MANGANH = tp1;
            var thanhpho = fr["thanhpho"];
            int tp = db.THANHPHOs.SingleOrDefault(n => n.TENTHANHPHO == thanhpho).MATHANHPHO;
            a.MATHANHPHO = tp;
            a.MUCLUONGTOITHIEU = Double.Parse(fr["mucluongtoithieu"]);
            a.MUCLUONGTOIDA = Double.Parse(fr["mucluongtoida"]);
            a.MOTACV = fr["mota"];
            a.YEUCAUCV = fr["yeucau"];
            a.KYNANG = fr["kynang"];
            a.NGUOILIENHE = fr["nguoilienhe"];
            a.EMAILLIENHE = fr["email"];
            var b = db.PHIEUDANGTUYENs.SingleOrDefault(n => n.MAPDT == id);
            b.THOIHANDANGTIN = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", fr["thoihan"]));
            db.SubmitChanges();
            return RedirectToAction("Thongtindoanhnghiep", "NhaTuyenDung");
        }
    }
}