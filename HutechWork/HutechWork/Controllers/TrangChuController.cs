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
        [HttpGet]
        public ActionResult Index()
        {
            List<PHIEUDANGTUYEN> ct = db.PHIEUDANGTUYENs.OrderByDescending(a => a.NGAYDANGTIN).Take(4).ToList();
            return View(ct);
        }
        //[HttpPost]
        //public ActionResult Index(FormCollection collection)
        //{
        //    string ten = collection["ten"];
        //    return RedirectToAction("TimKiem", "TrangChu");
        //}

        [HttpGet]
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
    }
}