﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HutechWork.Models;
using PagedList;

namespace HutechWork.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        dbQlHutechWorkDataContext db = new dbQlHutechWorkDataContext();
        public ActionResult Index()
        {            
            List<PHIEUDANGTUYEN> ct = db.PHIEUDANGTUYENs.OrderByDescending(a => a.NGAYDANGTIN).Take(4).ToList();
            return View(ct);
        }

        [HttpGet]
        public ActionResult TimKiem(int? page)
        {            
            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (Session["nganhnghe"].ToString().Length != 0)
            {
                if (Session["diadiem"].ToString().Length != 0)
                {
                    var dt =
                       from a in db.PHIEUDANGTUYENs
                       where a.CHITIETTUYENDUNG.MATHANHPHO.ToString() == Session["diadiem"].ToString()
                       && a.CHITIETTUYENDUNG.MANGANH.ToString() == Session["nganhnghe"].ToString()
                       && a.CHITIETTUYENDUNG.TINHTRANG == true
                       && (a.CHITIETTUYENDUNG.TIEUDE.ToString().Contains(Session["ten"].ToString()) || a.TAIKHOAN_DN.TTDOANHNGHIEP.TENDN.ToString().Contains(Session["ten"].ToString()))
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var dt =
                       from a in db.PHIEUDANGTUYENs
                       where a.CHITIETTUYENDUNG.MANGANH.ToString() == Session["nganhnghe"].ToString()
                       && a.CHITIETTUYENDUNG.TINHTRANG == true
                       && (a.CHITIETTUYENDUNG.TIEUDE.ToString().Contains(Session["ten"].ToString()) || a.TAIKHOAN_DN.TTDOANHNGHIEP.TENDN.ToString().Contains(Session["ten"].ToString()))
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
                       from a in db.PHIEUDANGTUYENs
                       where a.CHITIETTUYENDUNG.MATHANHPHO.ToString() == Session["diadiem"].ToString()
                       && a.CHITIETTUYENDUNG.TINHTRANG == true
                       && (a.CHITIETTUYENDUNG.TIEUDE.ToString().Contains(Session["ten"].ToString()) || a.TAIKHOAN_DN.TTDOANHNGHIEP.TENDN.ToString().Contains(Session["ten"].ToString()))
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var dt =
                       from a in db.PHIEUDANGTUYENs
                       where (a.CHITIETTUYENDUNG.TIEUDE.ToString().Contains(Session["ten"].ToString()) || a.TAIKHOAN_DN.TTDOANHNGHIEP.TENDN.ToString().Contains(Session["ten"].ToString()))
                       && a.CHITIETTUYENDUNG.TINHTRANG == true
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
            }
        }
        [HttpPost]
        public ActionResult TimKiem(int? page, FormCollection collection)
        {               
            Session["ten"] = collection["ten"];       
            Session["nganhnghe"] = collection["nganhnghe"];
            Session["diadiem"] = collection["diadiem"];
            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (collection["nganhnghe"].ToString().Length != 0)
            {
                if (collection["diadiem"].ToString().Length != 0)
                {
                    //Session["ten"] = "mot hai";
                    var dt =
                       from a in db.PHIEUDANGTUYENs
                       where a.CHITIETTUYENDUNG.MATHANHPHO.ToString() == collection["diadiem"].ToString()
                       && a.CHITIETTUYENDUNG.MANGANH.ToString() == collection["nganhnghe"].ToString()
                       && a.CHITIETTUYENDUNG.TINHTRANG == true
                       && (a.CHITIETTUYENDUNG.TIEUDE.ToString().Contains(collection["ten"].ToString()) || a.TAIKHOAN_DN.TTDOANHNGHIEP.TENDN.ToString().Contains(collection["ten"].ToString()))
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var dt =
                       from a in db.PHIEUDANGTUYENs
                       where a.CHITIETTUYENDUNG.MANGANH.ToString() == collection["nganhnghe"].ToString()
                       && a.CHITIETTUYENDUNG.TINHTRANG == true
                       && (a.CHITIETTUYENDUNG.TIEUDE.ToString().Contains(collection["ten"].ToString()) || a.TAIKHOAN_DN.TTDOANHNGHIEP.TENDN.ToString().Contains(collection["ten"].ToString()))
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
                       from a in db.PHIEUDANGTUYENs
                       where a.CHITIETTUYENDUNG.MATHANHPHO.ToString() == collection["diadiem"].ToString()
                       && a.CHITIETTUYENDUNG.TINHTRANG == true
                       && (a.CHITIETTUYENDUNG.TIEUDE.ToString().Contains(collection["ten"].ToString()) || a.TAIKHOAN_DN.TTDOANHNGHIEP.TENDN.ToString().Contains(collection["ten"].ToString()))
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var dt =
                       from a in db.PHIEUDANGTUYENs
                       where (a.CHITIETTUYENDUNG.TIEUDE.ToString().Contains(collection["ten"].ToString()) || a.TAIKHOAN_DN.TTDOANHNGHIEP.TENDN.ToString().Contains(collection["ten"].ToString()))
                        && a.CHITIETTUYENDUNG.TINHTRANG == true
                       select a;
                    ViewBag.data = dt;
                    return View(dt.ToPagedList(pageNum, pageSize));
                }
            }          
        }
        public ActionResult Nganhnghe()
        {
            var nganhnghe = from nn in db.NGANHs select nn;
            return PartialView(nganhnghe);
        }
        public ActionResult Diadiem()
        {
            var thanhpho = from tp in db.THANHPHOs select tp;
            return PartialView(thanhpho);
        }
        public ActionResult Tatcacongviec()
        {
            Session["ten"] = "";
            Session["nganhnghe"] = "";
            Session["diadiem"] = "";
            return RedirectToAction("TimKiem", "TrangChu");
        }
        public ActionResult ChitietCv(int id)
        {
            var sp = from s in db.PHIEUDANGTUYENs where s.MAPDT == id && s.CHITIETTUYENDUNG.TINHTRANG == true select s;
            if (Session["TaikhoanCN"] == null)
                ViewData["1"] = null;
            else
            {
                var b = from s in db.NOPDONs where s.MAPDT == id select s.MATKCN;
                if (b == null)
                    ViewData["1"] = null;
                else
                {
                    foreach (var item in b.ToList())
                    {
                        if (String.Compare(item.ToString(), Session["TaikhoanCN"].ToString(), false) == 0)
                            ViewData["1"] = "2";
                    }
                }
            }
            return View(sp.Single());
        }
        public ActionResult Nopdon(int id, string strURL)
        {
            NOPDON nd = new NOPDON();
            if (Session["TaikhoanCN"] == null || Session["TaikhoanCN"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            else
            {               
                nd.MATKCN = int.Parse(Session["TaikhoanCN"].ToString());
                nd.MAPDT = id;
                nd.NGAYNOPDON = DateTime.Now;
                nd.TINHTRANG = false;
                db.NOPDONs.InsertOnSubmit(nd);
                db.SubmitChanges();
            }        
            return Redirect(strURL);
        }
    }
}