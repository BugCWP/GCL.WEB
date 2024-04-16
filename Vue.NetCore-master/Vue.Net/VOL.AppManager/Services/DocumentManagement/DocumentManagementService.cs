using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using VOL.AppManager.IServices.DocumentManagement;
using VOL.Core.Enums;
using VOL.Core.Utilities;
using VOL.Entity.DomainModels;
using VOL.Core.Extensions;
using System.Linq;

namespace VOL.AppManager.Services.DocumentManagement
{
    public class DocumentManagementService : IDocumentManagementService
    {
        static string ECConnectionString = "Data Source=10.10.1.110:1521/ecology;User ID=read2506;Password=read1017";
        SqlSugarClient ECdb = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = ECConnectionString,
            DbType = DbType.Oracle,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        });

        static string PMConnectionString = "Data Source=10.10.14.12:1521/NYGC;User Id=powerpip;Password=powerpip;";
        SqlSugarClient PMdb = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = PMConnectionString,
            DbType = DbType.Oracle,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        });

        string ECSql = @"SELECT * FROM (
  
  SELECT t.REQUESTID,t.HTBH,t.HTMC,h.lastname,
  CASE t.YYLX WHEN 1 THEN '电子用印' ELSE '物理用印' END YYLX,
  CASE  t.YYLX WHEN 1 THEN
    (SELECT LISTAGG(to_char(COMPANYNAME),',') from
    ( SELECT hc.SUBCOMPANYNAME AS COMPANYNAME  FROM ecology.FORMTABLE_MAIN_1300_DT2 dt2
     LEFT JOIN ecology.hrmsubcompany hc ON dt2.QYZT =hc.ID  WHERE dt2.MAINID = t.ID
     UNION 
     SELECT dt3.QYZT AS COMPANYNAME  FROM ecology.FORMTABLE_MAIN_1418_DT3 dt3 WHERE dt3.MAINID = t.ID
     ) )
  ELSE  
  (CASE when t.WFQYZTWB = '' OR  t.WFQYZTWB is NULL
    THEN (select LISTAGG(to_char(t1.QYZT),',') FROM ecology.FORMTABLE_MAIN_1300_DT1 t1 WHERE t.ID = t1.MAINID)
    ELSE t.WFQYZTWB||','||( select LISTAGG(to_char(t1.QYZT),',') FROM ecology.FORMTABLE_MAIN_1300_DT1 t1 WHERE t.ID = t1.MAINID )  
    END 
  )
  end  ZRZ 
  FROM  ecology.formtable_main_1300 t
  LEFT JOIN ecology.workflow_nownode j ON j.requestid=t.requestid
  LEFT JOIN ecology.workflow_nodebase n ON  j.nownodeid=n.id 
   LEFT JOIN 
   (SELECT w.requestid,w.isremark,w.USERID FROM ecology.workflow_currentoperator w 
   LEFT JOIN ecology.workflow_nodebase n ON  n.ID =w.NODEID WHERE  n.NODENAME like '%发起人%' AND w.groupid=0  AND w.preisremark=0
   )wc ON wc.requestid= t.requestid 
   LEFT JOIN  ecology.hrmresource h ON  h.id = wc.USERID WHERE t.GSTT IS NOT null
  
  UNION 
  
  SELECT t.REQUESTID,t.HTBH,t.HTMC,h.lastname,
  CASE t.YYLX WHEN 1 THEN '电子用印' ELSE '物理用印' END YYLX,
  CASE  t.YYLX WHEN 1 THEN
    (SELECT LISTAGG(to_char(COMPANYNAME),',') from
    (SELECT hc.SUBCOMPANYNAME AS COMPANYNAME  FROM ecology.formtable_main_1418_DT2 dt2
     LEFT JOIN ecology.hrmsubcompany hc ON dt2.QYZT =hc.ID  WHERE dt2.MAINID = t.ID
     UNION 
     SELECT dt3.QYZT AS COMPANYNAME  FROM ecology.formtable_main_1418_DT3 dt3 WHERE dt3.MAINID = t.ID
     ) )
  ELSE  
  (CASE when  t.gsm is NULL
    THEN (select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_1418_DT1 t1 WHERE t.ID = t1.MAINID)
    WHEN  t.gsm =0 THEN '协鑫绿能系统科技有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_1418_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.gsm =1 THEN '协鑫(苏州)能源检测技术有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_1418_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.gsm =2 THEN '苏州协鑫清洁能源发展有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_1418_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.gsm =3 THEN '协鑫储能科技（苏州）有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_1418_DT1 t1 WHERE t.ID = t1.MAINID )  
    END 
  )
  end  ZRZ 
  FROM  ecology.formtable_main_1418 t
  LEFT JOIN ecology.workflow_nownode j ON j.requestid=t.requestid
  LEFT JOIN ecology.workflow_nodebase n ON  j.nownodeid=n.id 
   LEFT JOIN 
   (SELECT w.requestid,w.isremark,w.USERID FROM ecology.workflow_currentoperator w 
   LEFT JOIN ecology.workflow_nodebase n ON  n.ID =w.NODEID WHERE  n.NODENAME like '%发起人%' AND w.groupid=0  AND w.preisremark=0
   )wc ON wc.requestid= t.requestid 
   LEFT JOIN  ecology.hrmresource h ON  h.id = wc.USERID  WHERE  t.gsm IS NOT NULL 
  
  UNION
  
    SELECT t.REQUESTID,t.HTBH,t.HTMC,h.lastname,
  CASE t.YYLX WHEN 1 THEN '电子用印' ELSE '物理用印' END YYLX,
  CASE  t.YYLX WHEN 1 THEN
    (SELECT LISTAGG(to_char(COMPANYNAME),',') from
    ( SELECT hc.SUBCOMPANYNAME AS COMPANYNAME  FROM ecology.formtable_main_668_DT2 dt2
     LEFT JOIN ecology.hrmsubcompany hc ON dt2.QYZT =hc.ID  WHERE dt2.MAINID = t.ID
     UNION 
     SELECT dt3.QYZT AS COMPANYNAME  FROM ecology.formtable_main_668_DT3 dt3 WHERE dt3.MAINID = t.ID
     ) )
  ELSE  
  (CASE when  t.GSTT is NULL
    THEN (select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_668_DT1 t1 WHERE t.ID = t1.MAINID)
    WHEN  t.GSTT =0 THEN '协鑫绿能系统科技有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_668_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.GSTT =1 THEN '协鑫(苏州)能源检测技术有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_668_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.GSTT =2 THEN '苏州协鑫清洁能源发展有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_668_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.GSTT =3 THEN '协鑫能源技术有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_668_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.GSTT =4 THEN '协鑫储能科技（苏州）有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_668_DT1 t1 WHERE t.ID = t1.MAINID )  
    END 
  )
  end  ZRZ 
  FROM  ecology.formtable_main_668 t
  LEFT JOIN ecology.workflow_nownode j ON j.requestid=t.requestid
  LEFT JOIN ecology.workflow_nodebase n ON  j.nownodeid=n.id 
   LEFT JOIN 
   (SELECT w.requestid,w.isremark,w.USERID FROM ecology.workflow_currentoperator w 
   LEFT JOIN ecology.workflow_nodebase n ON  n.ID =w.NODEID WHERE  n.NODENAME like '%发起人%' AND w.groupid=0  AND w.preisremark=0
   )wc ON wc.requestid= t.requestid
   LEFT JOIN  ecology.hrmresource h ON  h.id = wc.USERID  WHERE  t.GSTT IS NOT null
  
  union 
  
    SELECT t.REQUESTID,t.HTBH,t.HTMC,h.lastname,
  '物理用印' YYLX,
  (CASE when  t.SQGS  is NULL
    THEN (select LISTAGG(to_char(t1.QYZT),',') FROM ecology.formtable_main_1428_DT1 t1 WHERE t.ID = t1.MAINID)
    ELSE  hc.SUBCOMPANYNAME ||','||( select LISTAGG(to_char(t1.QYZT),',') FROM ecology.formtable_main_1428_DT1 t1 WHERE t.ID = t1.MAINID )  
    END 
  )  ZRZ 
  FROM  ecology.formtable_main_1428 t
  LEFT JOIN ecology.workflow_nownode j ON j.requestid=t.requestid
  LEFT JOIN ecology.workflow_nodebase n ON  j.nownodeid=n.id 
    LEFT JOIN 
   (SELECT w.requestid,w.USERID FROM ecology.workflow_currentoperator w 
   LEFT JOIN ecology.workflow_nodebase n ON  n.ID =w.NODEID WHERE  n.NODENAME like '%发起人%' AND w.groupid=0 AND w.preisremark=0
   )wc ON wc.requestid=t.requestid
   LEFT JOIN  ecology.hrmresource h ON  h.id = wc.USERID 
   LEFT JOIN ecology.hrmsubcompany hc ON t.SQGS =hc.ID WHERE t.SQGS IS NOT NULL 
  
  UNION
  
    SELECT t.REQUESTID,t.HTBH,t.HTMC,h.lastname,
  '物理用印' YYLX,
  (CASE when  t.XMGSM  is NULL
    THEN (select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_1144_DT1 t1 WHERE t.ID = t1.MAINID)
     WHEN  t.XMGSM =0 THEN '四川协鑫绿能工程科技有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_1144_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.XMGSM =1 THEN '内蒙古协鑫高新能源系统科技有限公司' ||','||( select LISTAGG(to_char(t1.DFQYZT),',') FROM ecology.formtable_main_1144_DT1 t1 WHERE t.ID = t1.MAINID )  
    END 
  )  ZRZ 
  FROM  ecology.formtable_main_1144 t
  LEFT JOIN ecology.workflow_nownode j ON j.requestid=t.requestid
  LEFT JOIN ecology.workflow_nodebase n ON  j.nownodeid=n.id 
  LEFT JOIN 
   (SELECT w.requestid,w.USERID FROM ecology.workflow_currentoperator w 
   LEFT JOIN ecology.workflow_nodebase n ON  n.ID =w.NODEID WHERE  n.NODENAME = '发起人' AND w.groupid=0 AND w.preisremark=0
   )wc ON wc.requestid=t.requestid
   LEFT JOIN  ecology.hrmresource h ON  h.id = wc.USERID WHERE  t.XMGSM IS NOT NULL 
   
   UNION
   
     SELECT t.REQUESTID,t.HTBH,t.HTMC,h.lastname,
  CASE t.YYLX WHEN 1 THEN '电子用印' ELSE '物理用印' END YYLX,
  CASE  t.YYLX WHEN 1 THEN
    (SELECT LISTAGG(to_char(COMPANYNAME),',') from
    (SELECT hc.SUBCOMPANYNAME AS COMPANYNAME  FROM ecology.formtable_main_1330_DT2 dt2
     LEFT JOIN ecology.hrmsubcompany hc ON dt2.QYZT =hc.ID  WHERE dt2.MAINID = t.ID
     UNION 
     SELECT dt3.QYZT AS COMPANYNAME  FROM ecology.formtable_main_1330_DT3 dt3 WHERE dt3.MAINID = t.ID
     ) )
  ELSE  
  (CASE when  t.GSTT is NULL
    THEN (select LISTAGG(to_char(t1.QYZT),',') FROM ecology.formtable_main_1330_DT1 t1 WHERE t.ID = t1.MAINID)
    WHEN  t.GSTT =0 THEN '协鑫绿能系统科技有限公司' ||','||( select LISTAGG(to_char(t1.QYZT),',') FROM ecology.formtable_main_1330_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.GSTT =1 THEN '协鑫(苏州)能源检测技术有限公司' ||','||( select LISTAGG(to_char(t1.QYZT),',') FROM ecology.formtable_main_1330_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.GSTT =2 THEN '苏州协鑫清洁能源发展有限公司' ||','||( select LISTAGG(to_char(t1.QYZT),',') FROM ecology.formtable_main_1330_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.GSTT =3 THEN '协鑫能源技术有限公司' ||','||( select LISTAGG(to_char(t1.QYZT),',') FROM ecology.formtable_main_1330_DT1 t1 WHERE t.ID = t1.MAINID )  
    WHEN  t.GSTT =4 THEN '协鑫储能科技（苏州）有限公司' ||','||( select LISTAGG(to_char(t1.QYZT),',') FROM ecology.formtable_main_1330_DT1 t1 WHERE t.ID = t1.MAINID )  
    END 
  )
  end  ZRZ 
  FROM  ecology.formtable_main_1330 t
  LEFT JOIN ecology.workflow_nownode j ON j.requestid=t.requestid
  LEFT JOIN ecology.workflow_nodebase n ON  j.nownodeid=n.id 
  LEFT JOIN 
   (SELECT w.requestid,w.USERID FROM ecology.workflow_currentoperator w 
   LEFT JOIN ecology.workflow_nodebase n ON  n.ID =w.NODEID WHERE  n.NODENAME = '发起人' AND w.groupid=0 AND w.preisremark=0
   )wc ON wc.requestid=t.requestid
   LEFT JOIN  ecology.hrmresource h ON  h.id = wc.USERID WHERE  t.GSTT IS NOT NULL 
  )  WHERE 1=1";

        public DocumentManagementService() { }

        /// <summary>
        /// 加载页面数据
        /// </summary>
        /// <param name="loadSingleParameters"></param>
        /// <returns></returns>
        public PageGridData<DocumentManagerDto> GetPageData(PageDataOptions options)
        {
            options = ValidatePageOptions(options);
            //获取排序字段
            //var sortArr = options.Sort.Split(",").Distinct().ToList();

            PageGridData<DocumentManagerDto> pageGridData = new PageGridData<DocumentManagerDto>();
            if (options.Export)
            {
                //queryable = queryable.GetIQueryableOrderBy(orderbyDic);
                //if (Limit > 0)
                //{
                //    queryable = queryable.Take(Limit);
                //}
                //pageGridData.rows = queryable.ToList();
            }
            else
            {
                if (options.Filter != null && options.Filter.Any())
                {
                    var filterSql = string.Empty;
                    foreach (var filter in options.Filter)
                    {
                        LinqExpressionType expressionType = LinqExpressionType.Equal;
                        if (!filter.DisplayType.IsNullOrEmpty())
                        {
                            expressionType = filter.DisplayType.GetLinqCondition();
                        }
                        switch (expressionType)
                        {
                            case LinqExpressionType.Equal:
                                filterSql += $@" and {filter.Name} = '{filter.Value}'";
                                break;
                            case LinqExpressionType.NotEqual:
                                filterSql += $@" and {filter.Name} != '{filter.Value}'";
                                break;
                            case LinqExpressionType.GreaterThan:
                                filterSql += $@" and {filter.Name} > '{filter.Value}'";
                                break;
                            case LinqExpressionType.LessThan:
                                filterSql += $@" and {filter.Name} < '{filter.Value}'";
                                break;
                            case LinqExpressionType.ThanOrEqual:
                                filterSql += $@" and {filter.Name} >= '{filter.Value}'";
                                break;
                            case LinqExpressionType.LessThanOrEqual:
                                filterSql += $@" and {filter.Name} <= '{filter.Value}'";
                                break;
                            case LinqExpressionType.In:
                                filterSql += $@" and {filter.Name} in '{filter.Value}'";
                                break;
                            case LinqExpressionType.Contains:
                                filterSql += $@" and {filter.Name} like '%{filter.Value}%'";
                                break;
                            case LinqExpressionType.NotContains:
                                filterSql += $@" and {filter.Name} not like '%{filter.Value}%'";
                                break;
                            case LinqExpressionType.StartsWith:
                                filterSql += $@" and {filter.Name} like '{filter.Value}%'";
                                break;
                            case LinqExpressionType.EndsWith:
                                filterSql += $@" and {filter.Name} like '%{filter.Value}'";
                                break;
                        }
                    }
                    ECSql += filterSql;
                }

                if (!options.Sort.IsNullOrEmpty())
                {
                    ECSql += $@" order by {options.Sort} {options.Order}";
                }

                int rowCount = 0;
                var rows = ECdb.SqlQueryable<DocumentManagerDto>(ECSql).ToPageList(options.Page, options.Rows, ref rowCount);

                //填入信息
                var requestIds = rows.Select(e => e.REQUESTID).ToList();
                var PMrows = PMdb.SqlQueryable<DocumentManagerDto>("select * from FINE_DOCUMENT").Where(e => requestIds.Contains(e.REQUESTID)).ToList();

                for (int i = 0; i < rows.Count; i++)
                {
                    if (PMrows != null && PMrows.Count > 0)
                    {
                        var pmrow = PMrows.Where(e => e.REQUESTID == rows[i].REQUESTID).FirstOrDefault();
                        if (pmrow != null)
                        {
                            rows[i].HTBH = pmrow.HTBH.IsNullOrEmpty() ? rows[i].HTBH : pmrow.HTBH;
                            rows[i].HTMC = pmrow.HTMC.IsNullOrEmpty() ? rows[i].HTMC : pmrow.HTMC;
                            rows[i].LASTNAME = pmrow.LASTNAME.IsNullOrEmpty() ? rows[i].LASTNAME : pmrow.LASTNAME;
                            rows[i].YYLX = pmrow.YYLX.IsNullOrEmpty() ? rows[i].YYLX : pmrow.YYLX;
                            rows[i].ZRZ = pmrow.ZRZ.IsNullOrEmpty() ? rows[i].ZRZ : pmrow.ZRZ;
                            rows[i].DZQWS = pmrow.DZQWS;
                            rows[i].DH = pmrow.DH;
                            rows[i].JH = pmrow.JH;
                            rows[i].WJRQ = pmrow.WJRQ;
                            rows[i].ND = pmrow.ND;
                            rows[i].BGQX = pmrow.BGQX;
                            rows[i].MJ = pmrow.MJ;
                            rows[i].YS = pmrow.YS;
                            rows[i].HH = pmrow.HH;
                            rows[i].GDR = pmrow.GDR;
                            rows[i].GDBM = pmrow.GDBM;
                            rows[i].QZ = pmrow.QZ;
                            rows[i].GDFS = pmrow.GDFS;
                            rows[i].KFWZ = pmrow.KFWZ;
                            rows[i].GDFS = pmrow.GDFS;
                            rows[i].KFWZ = pmrow.KFWZ;
                            rows[i].HJH = pmrow.HJH;
                            rows[i].BZ = pmrow.BZ;
                            rows[i].DALH = pmrow.DALH;
                            rows[i].GD = pmrow.GD;
                        }
                        else
                        {
                            rows[i].GD = "否";
                        }
                    }
                    var requestidStr = rows[i].REQUESTID.ToString();
                    rows[i].YZM = requestidStr.Substring(requestidStr.Length - 3, 3);
                }
                pageGridData.rows = rows;
                pageGridData.total = rowCount;
            }
            //GetPageDataOnExecuted?.Invoke(pageGridData);
            return pageGridData;

        }

        /// <summary>
        /// 验证排序与查询字段合法性
        /// </summary>
        /// <param name="options"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        protected PageDataOptions ValidatePageOptions(PageDataOptions options)
        {
            options = options ?? new PageDataOptions();

            List<SearchParameters> searchParametersList = new List<SearchParameters>();
            if (options.Filter != null && options.Filter.Count > 0)
            {
                searchParametersList.AddRange(options.Filter);
            }
            else if (!string.IsNullOrEmpty(options.Wheres))
            {
                try
                {
                    searchParametersList = options.Wheres.DeserializeObject<List<SearchParameters>>();
                    options.Filter = searchParametersList;
                }
                catch { }
            }
            return options;
        }

        /// <summary>
        /// 编辑
        /// 1、明细表必须把主表的主键字段也设置为可编辑
        /// 2、修改、增加只会操作设置为编辑列的数据
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public WebResponseContent Update(SaveModel saveModel)
        {
            DocumentManagerDto entity = new DocumentManagerDto();
            foreach (var data in saveModel.MainData)
            {
                entity.GetType().GetProperty(data.Key).SetValue(entity, data.Key == "REQUESTID" ? int.Parse(data.Value.ToString()) : data.Value);
            }
            var x = PMdb.Storageable(entity).ToStorage();
            var res = 0;
            res += x.AsInsertable.ExecuteCommand();//不存在插入
            res += x.AsUpdateable.ExecuteCommand();//存在更新
            //var res = PMdb.Storageable(entity).DefaultAddElseUpdate().ExecuteCommand();
            if (res > 0) return new WebResponseContent().OK();
            else return new WebResponseContent().Error();
            //return null;
        }


        /// <summary>
        /// 编辑
        /// 1、明细表必须把主表的主键字段也设置为可编辑
        /// 2、修改、增加只会操作设置为编辑列的数据
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public WebResponseContent Submit(DocumentManagerDto dto)
        {
            var requestidStr = dto.REQUESTID.ToString();
            var yzm = requestidStr.Substring(requestidStr.Length - 3, 3);
            if (yzm != dto.YZM)
            {
                return new WebResponseContent().Error("验证码错误");
            }
            dto.GD = "是";
            var x = PMdb.Storageable(dto).ToStorage();
            var res = 0;
            res += x.AsInsertable.ExecuteCommand();//不存在插入
            res += x.AsUpdateable.ExecuteCommand();//存在更新
            //var res = PMdb.Storageable(entity).DefaultAddElseUpdate().ExecuteCommand();
            if (res > 0) return new WebResponseContent().OK();
            else return new WebResponseContent().Error();
            //return null;
        }



        /// <summary>
        /// 编辑
        /// 1、明细表必须把主表的主键字段也设置为可编辑
        /// 2、修改、增加只会操作设置为编辑列的数据
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public WebResponseContent GetOne(DocumentManagerDto dto)
        {
            ECSql += $@" and REQUESTID = {dto.REQUESTID}";
            var ecrow = ECdb.SqlQueryable<DocumentManagerDto>(ECSql).First();

            var pmrow = PMdb.SqlQueryable<DocumentManagerDto>("select * from FINE_DOCUMENT").Where(e => dto.REQUESTID == e.REQUESTID).First();

            if (ecrow == null)
            {
                return new WebResponseContent().Error("未查到此单据，请联系管理员");
            }

            if (pmrow != null)
            {
                ecrow.HTBH = pmrow.HTBH.IsNullOrEmpty() ? ecrow.HTBH : pmrow.HTBH;
                ecrow.HTMC = pmrow.HTMC.IsNullOrEmpty() ? ecrow.HTMC : pmrow.HTMC;
                ecrow.LASTNAME = pmrow.LASTNAME.IsNullOrEmpty() ? ecrow.LASTNAME : pmrow.LASTNAME;
                ecrow.YYLX = pmrow.YYLX.IsNullOrEmpty() ? ecrow.YYLX : pmrow.YYLX;
                ecrow.ZRZ = pmrow.ZRZ.IsNullOrEmpty() ? ecrow.ZRZ : pmrow.ZRZ;
                ecrow.DZQWS = pmrow.DZQWS;
                ecrow.DH = pmrow.DH;
                ecrow.JH = pmrow.JH;
                ecrow.WJRQ = pmrow.WJRQ;
                ecrow.ND = pmrow.ND;
                ecrow.BGQX = pmrow.BGQX;
                ecrow.MJ = pmrow.MJ;
                ecrow.YS = pmrow.YS;
                ecrow.HH = pmrow.HH;
                ecrow.GDR = pmrow.GDR;
                ecrow.GDBM = pmrow.GDBM;
                ecrow.QZ = pmrow.QZ;
                ecrow.GDFS = pmrow.GDFS;
                ecrow.KFWZ = pmrow.KFWZ;
                ecrow.GDFS = pmrow.GDFS;
                ecrow.KFWZ = pmrow.KFWZ;
                ecrow.HJH = pmrow.HJH;
                ecrow.BZ = pmrow.BZ;
                ecrow.DALH = pmrow.DALH;
                ecrow.GD = pmrow.GD;
            }

            return new WebResponseContent().OK("", ecrow);
        }
    }
}
