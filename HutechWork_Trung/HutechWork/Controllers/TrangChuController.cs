using System;
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
            List<PHIEUDANGTUYEN> ct = (from cttd in db.PHIEUDANGTUYENs select cttd).OrderByDescending(a => a.NGAYDANGTIN).Take(1).ToList();
            return View(ct);
        }
        //public ActionResult TimKiem(int? page)
        //{
        //    int pageSize = 2;
        //    //Tao bien so trang
        //    int pageNum = (page ?? 1);
        //    List<PHIEUDANGTUYEN> ct = (from cttd in db.PHIEUDANGTUYENs select cttd).ToList();
        //    return View(ct.ToPagedList(pageNum, pageSize));
        //}
        public ActionResult TimKiem(int? page)
        {
            var dt =
                from a in db.PHIEUDANGTUYENs
                join b in db.CHITIETTUYENDUNGs on a.MAPDT equals b.MAPDT
                where b.MATHANHPHO.ToString() == Session["diadiem"].ToString() && b.MANGANH.ToString() == Session["nganhnghe"].ToString()
                select a;
            int pageSize = 2;
            int pageNum = (page ?? 1);
            return View(dt.ToPagedList(pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult TimKiem(int? page, FormCollection collection)
        {
            ViewBag.ten = collection["ten"];
            Session["nganhnghe"] = collection["nganhnghe"];
            Session["diadiem"] = collection["diadiem"];
            var dt =
                from a in db.PHIEUDANGTUYENs
                join b in db.CHITIETTUYENDUNGs on a.MAPDT equals b.MAPDT

                where b.MATHANHPHO.ToString() == collection["diadiem"] && b.MANGANH.ToString() == collection["nganhnghe"].ToString()
                select a;
            int pageSize = 2;
            int pageNum = (page ?? 1);
            return View(dt.ToPagedList(pageNum, pageSize));
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
        public ActionResult ChitietCv(int id)
        {
            var sp = from s in db.PHIEUDANGTUYENs where s.MAPDT == id select s;
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
                nd.TINHTRANG = false;
                db.NOPDONs.InsertOnSubmit(nd);
                db.SubmitChanges();
            }        
            return Redirect(strURL);
        }
    }
}