using HutechWork.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechWork.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        dbQlHutechWorkDataContext db = new dbQlHutechWorkDataContext();
        public ActionResult Index()
        {
            List<PHIEUDANGTUYEN> ct = (from cttd in db.PHIEUDANGTUYENs select cttd).ToList();
            return View(ct);
        }
        public ActionResult TimKiem(int? page)
        {
            int pageSize = 20;
            //Tao bien so trang
            int pageNum = (page ?? 1);
            List<PHIEUDANGTUYEN> ct = (from cttd in db.PHIEUDANGTUYENs select cttd).ToList();
            return View(ct.ToPagedList(pageNum, pageSize));
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