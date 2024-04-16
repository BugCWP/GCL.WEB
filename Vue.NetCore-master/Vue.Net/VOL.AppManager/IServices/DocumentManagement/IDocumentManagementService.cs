using System;
using System.Collections.Generic;
using System.Text;
using VOL.AppManager.Services.DocumentManagement;
using VOL.Core.BaseProvider;
using VOL.Core.Utilities;
using VOL.Entity.DomainModels;

namespace VOL.AppManager.IServices.DocumentManagement
{
    public partial interface IDocumentManagementService
    {
        /// <summary>
        /// 加载页面数据
        /// </summary>
        /// <param name="loadSingleParameters"></param>
        /// <returns></returns>
        PageGridData<DocumentManagerDto> GetPageData(PageDataOptions options);

        WebResponseContent Update(SaveModel saveModel);
    }
}
