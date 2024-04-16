<template>
    <div style="padding: 15px 20px 15px 5px">
      <div class="pre-text">{{ text }}</div>
      <vol-form
        ref="form"
        :labelWidth="80"
        :load-key="false"
        :formFields="fields"
        :formRules="formOptions"
      >
      </vol-form>
  
      <div class="form-btns">
        <el-button type="primary" @click="submit" icon="el-icon-check" size="mini"
          >提交</el-button
        >
        <el-button
          type="primary"
          @click="reset"
          plain
          icon="el-icon-refresh-right"
          size="mini"
          >重置</el-button
        >
      </div>
    </div>
  </template>
  
  <script>
  // 使用方式：
  // 1、新建一个vue页面，把此页面内容复制进去
  // 2、router->index.js配置路由，页面上输入地址即可看到数据(也可以把菜单配置上)
  // 3、或者参照表单设计页面做动态页面
  //**表单设计器的table下载还在开发中
  
  import VolForm from "@/components/basic/VolForm";
  export default {
    components: { "vol-form": VolForm },
    data() {
      return {
        text: "",
        tabsModel: "0",
        REQUESTID:'',
        fields: {
          HTBH: null,
          HTMC: null,
          LASTNAME: null,
          YYLX: null,
          ZRZ: null,
          YZM: null,
          REQUESTID: null,
        },
        formOptions: [
          [
            {
              field: "HTBH",
              title: "合同编号",
              type: "text",
              required: true,
              readonly: false,
              colSize: null,
            },
            {
              field: "HTMC",
              title: "合同名称",
              type: "text",
              required: true,
              readonly: false,
              colSize: null,
            },
            {
              field: "LASTNAME",
              title: "申请人",
              type: "text",
              required: false,
              readonly: true,
              colSize: null,
            },
          ],
          [
            {
              field: "YYLX",
              title: "用印类型",
              type: "text",
              required: false,
              readonly: false,
              colSize: null,
            },
            {
              field: "ZRZ",
              title: "责任者",
              type: "text",
              required: true,
              readonly: false,
              colSize: null,
            },
            {
              field: "YZM",
              title: "验证码",
              type: "text",
              required: true,
              readonly: false,
              colSize: null,
            },
          ],
        ],
        tables: [],
        tabs: [],
      };
    },
    created() {
      let that = this;
      // 监听页面的keyup事件
      document.onkeyup = function(e) {
        if(e.key!='Enter'){
          that.REQUESTID+=e.key;
        }else{
           console.log(that.REQUESTID);
           that.fields.REQUESTID=that.REQUESTID;
           that.getOne();
           that.REQUESTID='';
        }
  
      };
    },
    methods: {
      submit() {
        this.http.post("api/DocumentManagement/Submit", this.fields, true).then((result) => {
          console.log(result);
          if(result.status){
              this.$Message.success("提交成功");
          }else{
              this.$Message.error(result.message);
          }
        });
      },
      reset() {
        this.$refs.form.reset();
        this.$Message.success("表单已重置");
      },
      download() {
        this.$Message.info("111");
      },
      getOne(){
          let that = this;
          this.http.post("api/DocumentManagement/GetOne", that.fields, true).then((result) => {
                 console.log(result);
                 that.fields. HTBH= result.data.htbh;
                 that.fields.HTMC=result.data.htmc;
                 that.fields.LASTNAME=result.data.lastname;
                 that.fields.YYLX= result.data.yylx;
                 that.fields.ZRZ= result.data.zrz;
                 that.fields.YZM= result.data.yzm;
                 that.fields.REQUESTID= result.data.requestid
          });
      }
    },
  };
  
  VolForm;
  </script>
  <style lang="less" scoped>
  .form-btns {
    text-align: center;
  }
  .tables {
    padding-left: 15px;
    .table-item {
      padding: 10px;
    }
    .table-header {
      display: flex;
      margin-bottom: 8px;
    }
    .header-text {
      position: relative;
      bottom: -9px;
      flex: 1;
      font-weight: bold;
    }
    .header-btns {
      text-align: right;
    }
  }
  </style>
  