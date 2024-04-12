using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using System;
using VOL.Core.Middleware;
using VOL.Entity.DomainModels;
using VOL.AppManager.IServices.DocumentManagement;
using Newtonsoft.Json;
using VOL.Core.Utilities;

namespace VOL.WebApi.Controllers.DocumentManagement
{
    [Route("api/DocumentManagement")]
    public class DocumentManagementController
    {
        protected IDocumentManagementService Service;
        public DocumentManagementController()
        {
        }
        public DocumentManagementController(IDocumentManagementService service)
        {
            Service = service;
        }

        [ActionLog("查询")]
        [HttpPost, Route("GetPageData")]
        public virtual ActionResult GetPageData([FromBody] PageDataOptions loadData)
        {
            return JsonNormal(InvokeService("GetPageData", new object[] { loadData }));
        }

        /// <summary>
        /// 编辑支持主子表
        /// [ModelBinder(BinderType =(typeof(ModelBinder.BaseModelBinder)))]可指定绑定modelbinder
        /// </summary>
        /// <param name="saveDataModel"></param>
        /// <returns></returns>
        [ActionLog("编辑")]
        [HttpPost, Route("Update")]
        public virtual ActionResult Update([FromBody] SaveModel saveModel)
        {
            return Json(InvokeService("Update", new object[] { saveModel }) as WebResponseContent);
        }

        /// <summary>
        /// 调用service方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private object InvokeService(string methodName, object[] parameters)
        {
            return Service.GetType().GetMethod(methodName).Invoke(Service, parameters);
        }
        /// <summary>
        /// 调用service方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="types">为要调用重载的方法参数类型：new Type[] { typeof(SaveDataModel)</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private object InvokeService(string methodName, Type[] types, object[] parameters)
        {
            return Service.GetType().GetMethod(methodName, types).Invoke(Service, parameters);
        }

        /// <summary>
        /// 2020.11.21增加json原格式返回数据(默认是驼峰格式)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serializerSettings"></param>
        /// <returns></returns>
        protected JsonResult JsonNormal(object data, JsonSerializerSettings serializerSettings = null, bool formateDate = true)
        {
            serializerSettings = serializerSettings ?? new JsonSerializerSettings();
            serializerSettings.ContractResolver = null;
            if (formateDate)
            {
                serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }
            serializerSettings.Converters.Add(new LongCovert());
            return Json(data, serializerSettings);
        }
    }
}
