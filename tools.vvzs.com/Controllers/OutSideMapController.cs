using System;
using System.Text;
using System.Web.Mvc;
using RPoney;
using tools.vvzs.com.BLL;
using tools.vvzs.com.Model.Entity;
using PublicEnum = tools.vvzs.com.Model.PublicEnum;

namespace tools.vvzs.com.Controllers
{
    public class OutSideMapController : BaseController
    {
        private Lazy<OutSideMapBll> outSideMapBll = new Lazy<OutSideMapBll>();
        // GET: OutSideMap
        public ActionResult Index(OutSideMapEntity model)
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(OutSideMapEntity model)
        {
            var entity = new OutSideMapEntity()
            {
                OutSideUrl = model.OutSideUrl,
                OutSideUrlMd5 = model.OutSideUrl.Md5(),
                UrlType = (int)PublicEnum.OutSideMapUrlTypeEnum.TaoBao
            };
            if (outSideMapBll.Value.Add(entity))
            {
                return Json(new
                {
                    Code = 200,
                    Data = new
                    {
                        Url = $"{Request.Url.Host}/OutSideMap/Detail?product={entity.OutSideUrlMd5}"
                    }
                });
            }
            else
            {
                return Json(new
                {
                    Code = 300,
                    Msg = "转换失败"
                });
            }
        }

        public ActionResult Detail(string product)
        {
            try
            {
                var entity = outSideMapBll.Value.GetByOutSideUrlMd5(product);
                var htmlText = RPoney.Utilty.Http.RequestHelper.HttpGet(entity.OutSideUrl, Encoding.Default);
                ViewBag.HtmlText = htmlText;
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}