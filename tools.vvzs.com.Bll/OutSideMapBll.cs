using System;
using tools.vvzs.com.Bll;
using tools.vvzs.com.DAL;
using tools.vvzs.com.Model.Entity;

namespace tools.vvzs.com.BLL
{
    //外部地址映射表
    public class OutSideMapBll : EntityBll<OutSideMapEntity>
    {
        public OutSideMapBll() : base(new OutSideMapDal()) { }

        private readonly Lazy<OutSideMapDal> _outSideMapDal = new Lazy<OutSideMapDal>();

        public OutSideMapEntity GetByOutSideUrlMd5(string outSideUrlMd5)
        {
            return _outSideMapDal.Value.GetByOutSideUrlMd5(outSideUrlMd5);
        }
    }
}