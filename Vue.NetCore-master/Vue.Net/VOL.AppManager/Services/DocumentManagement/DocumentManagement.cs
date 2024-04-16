using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace VOL.AppManager.Services.DocumentManagement
{
    [SugarTable("FINE_DOCUMENT")]
    public class DocumentManagerDto
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long REQUESTID { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string HTBH { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string HTMC { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string LASTNAME { get; set; }

        /// <summary>
        /// 用印类型
        /// </summary>
        public string YYLX { get; set; }

        /// <summary>
        /// 责任者
        /// </summary>
        public string ZRZ { get; set; }

        /// <summary>
        /// 电子全文数
        /// </summary>
        public string DZQWS { get; set; }

        /// <summary>
        /// 档号
        /// </summary>
        public string DH { get; set; }

        /// <summary>
        /// 件号
        /// </summary>
        public string JH { get; set; }

        /// <summary>
        /// 文件日期
        /// </summary>
        public string WJRQ { get; set; }


        /// <summary>
        /// 年度
        /// </summary>
        public string ND { get; set; }

        /// <summary>
        /// 保管期限
        /// </summary>
        public string BGQX { get; set; }

        /// <summary>
        /// 密级
        /// </summary>
        public string MJ { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public string YS { get; set; }

        /// <summary>
        /// 盒号
        /// </summary>
        public string HH { get; set; }

        /// <summary>
        /// 归档人
        /// </summary>
        public string GDR { get; set; }

        /// <summary>
        /// 归档部门
        /// </summary>
        public string GDBM { get; set; }

        /// <summary>
        /// 全宗
        /// </summary>
        public string QZ { get; set; }

        /// <summary>
        /// 归档分数
        /// </summary>
        public string GDFS { get; set; }

        /// <summary>
        /// 库房位置
        /// </summary>
        public string KFWZ { get; set; }

        /// <summary>
        /// 互见号
        /// </summary>
        public string HJH { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }

        /// <summary>
        /// 档案类号
        /// </summary>
        public string DALH { get; set; }

        /// <summary>
        /// 是否已经实物归档
        /// </summary>
        public string GD { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        //[SugarColumn(IsIgnore = true)]
        public string YZM { get; set; }
    }
}
