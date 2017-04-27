using System;

namespace tools.vvzs.com.Model.Entity
{
    /// <summary>
    ///外部地址映射表
    /// </summary>	
    [Serializable]
    public class OutSideMapEntity : IIdentityEntity
    {

        /// <summary>
        /// 自增主键
        /// </summary>		
        public long Id { get; set; }
        /// <summary>
        /// 外部地址
        /// </summary>		
        public string OutSideUrl { get; set; }
        /// <summary>
        /// 外部地址MD5值
        /// </summary>		
        public string OutSideUrlMd5 { get; set; }
        /// <summary>
        /// 地址类型 1:淘宝
        /// </summary>		
        public int UrlType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreatedTime { get; set; }
    }
}

