<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/appmanager/App_Expert.js此处编写
 -->
 <template>
    <div>
      <view-grid
        ref="grid"
        :columns="columns"
        :detail="detail"
        :editFormFields="editFormFields"
        :editFormOptions="editFormOptions"
        :searchFormFields="searchFormFields"
        :searchFormOptions="searchFormOptions"
        :table="table"
        :extend="extend"
      >
      </view-grid>
    </div>
  </template>
  
  <script>
  import extend from "@/extension/DocumentManagement/DocumentManagementList.js";
  var vueParam = {
    data() {
      return {
        table: {
          key: "REQUESTID",
          footer: "Foots",
          cnName: "用印文档管理",
          name: "DocumentManagement",
          url: "/DocumentManagement/",
          sortName: "REQUESTID",
        },
        extend: extend,
        editFormFields: {
          REQUESTID: "",
          ZRZ:"",
          HTBH: "",
          HTMC: "",
          LASTNAME: "",
          YYLX: "",
          DZQWS: "",
          DH: "",
          JH: "",
          WJRQ: "",
          ND: "",
          BGQX: "",
          MJ: "",
          YS: "",
          HH: "",
          GDR: "",
          GDBM: "",
          QZ: "",
          GDFS: "",
          KFWZ: "",
          HJH: "",
          BZ: "",
          DALH: "",
        },
        editFormOptions: [
          [
            { title: "电子全文数", field: "DZQWS" },
            { title: "责任者", field: "ZRZ" },
          ],
          [
            { title: "合同编号", field: "HTBH" },
            { title: "合同名称", field: "HTMC" },
          ],
          [
            { title: "申请人", field: "LASTNAME" },
            { title: "用印类型", field: "YYLX" },
          ],
          [
            { title: "档号", field: "DH" },
            { title: "件号", field: "JH" },
          ],
          [
            { title: "文件日期", field: "WJRQ" },
            { title: "年度", field: "ND" },
          ],
          [
            { title: "保管期限", field: "BGQX" },
            { title: "密级", field: "MJ" },
          ],
          [
            { title: "页数", field: "YS" },
            { title: "盒号", field: "HH" },
          ],
          [
            { title: "归档人", field: "GDR" },
            { title: "归档部门", field: "GDBM" },
          ],
          [
            { title: "全宗", field: "QZ" },
            { title: "归档分数", field: "GDFS" },
          ],
          [
            { title: "库房位置", field: "KFWZ" },
            { title: "互见号", field: "HJH" },
          ],
          [
            { title: "备注", field: "BZ" },
            { title: "档案类号", field: "DALH" },
          ]
        ],
        searchFormFields: {ZRZ:"", HTBH: "", HTMC: "", LASTNAME: "", YYLX: [] },
        searchFormOptions: [
          [
            { title: "责任者", field: "ZRZ", type: "like" },
            { title: "合同编号", field: "HTBH", type: "like" },
            { title: "合同名称", field: "HTMC" },
            { title: "申请人", field: "LASTNAME" },
          ],
          [
            {
              data: [
                { key: "物理用印", value: "物理用印" },
                { key: "电子用印", value: "电子用印" },
              ],
              title: "用印类型",
              field: "YYLX",
              type: "selectList",
            },
          //   { title: "审核时间", field: "AuditDate", type: "datetime" },
          ],
        ],
        columns: [
          {
            field: "REQUESTID",
            title: "主键ID",
            type: "int",
            width: 80,
            hidden: true,
            readonly: true,
            require: true,
            align: "left",
          },
          {
            field: "ZRZ",
            title: "责任者",
            type: "string",
            width: 500,
            hidden: false,
            align: "left",
          },
          {
            field: "HTBH",
            title: "合同编号",
            type: "string",
            width: 300,
            hidden: false,
            align: "left",
          },
          {
            field: "HTMC",
            title: "合同名称",
            type: "string",
            width: 500,
            align: "left",
            hidden: false,
            sort: true,
          },
          {
            field: "LASTNAME",
            title: "申请人",
            type: "string",
            width: 150,
            hidden: false,
            align: "left",
          },
          {
            field: "YYLX",
            title: "用印类型",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "GD",
            title: "是否已实物归档",
            type: "string",
            width: 150,
            hidden: false,
            align: "left",
          },
          {
            field: "DZQWS",
            title: "电子全文数",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "DH",
            title: "档号",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "JH",
            title: "件号",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "WJRQ",
            title: "文件日期",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "ND",
            title: "年度",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "BGQX",
            title: "保管期限",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "MJ",
            title: "密级",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "YS",
            title: "页数",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "HH",
            title: "盒号",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "GDR",
            title: "归档人",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "GDBM",
            title: "归档部门",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "QZ",
            title: "全宗",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "GDFS",
            title: "归档分数",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "KFWZ",
            title: "库房位置",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "HJH",
            title: "互见号",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "BZ",
            title: "备注",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
          {
            field: "DALH",
            title: "档案类号",
            type: "string",
            width: 100,
            hidden: false,
            align: "left",
          },
        ],
        detail: {
          cnName: "#detailCnName",
          columns: [],
          sortName: "",
          key: "",
        },
      };
    },
  };
  export default vueParam;
  </script>
  