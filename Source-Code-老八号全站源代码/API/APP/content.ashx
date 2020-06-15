<%@ WebHandler Language="C#" Class="user" %>

using System;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using Newtonsoft.Json;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using ZoomLa.Common;
using ZoomLa.BLL.Helper;
public class user :API_Base,IHttpHandler {
    B_User buser = new B_User();
    B_User_API buapi = new B_User_API();
    public void ProcessRequest (HttpContext context) {
        throw new Exception("接口关闭");
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Pragma, Cache-Control, Authorization ");
        HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
        retMod.retcode = M_APIResult.Failed;
        try
        {
            switch (Action)
            {
                case "list":
                    {
                        string nodeIds = "1,2,10,26";
                        PageSetting setting = new PageSetting();
                        setting.cpage = DataConvert.CLng(Req("cpage"), 1);
                        setting.psize = DataConvert.CLng(Req("psize")) == 0 ? 20 : DataConvert.CLng(Req("psize"));
                        setting.t1 = "ZL_CommonModel";
                        setting.t2 = "ZL_C_Article";
                        setting.pk = "A.GeneralID";
                        setting.on = "A.ItemID=B.ID";
                        setting.where = "A.NodeID IN (" + nodeIds + ") AND A.Status=99";
                        setting.where += " AND A.ModelID=2 AND A.TopImg !='' AND A.TopImg IS NOT NULL";
                        setting.order = "A.CreateTime DESC";
                        setting.fields = "A.GeneralID,A.Title,A.NodeID,A.Inputer,A.TopImg,A.CreateTime";
                        setting.fields += ",B.synopsis,B.content";

                        DBCenter.SelPage(setting);
                        retMod.retcode = M_APIResult.Success;
                        retMod.result = JsonConvert.SerializeObject(setting.dt);
                        retMod.result = ImageDeal(retMod.result);//对内容做处理
                        retMod.page = new M_API_Page(setting);
                    }
                    break;
                default:
                    {
                        retMod.retmsg = "[" + Action + "]接口不存在";
                    }
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; }
        RepToClient(retMod);
    }
    private const string siteUrl = "https://www.ziti163.com";
    private static string ImageDeal(string content) { return content.Replace("/UploadFiles/", siteUrl + "/UploadFiles/"); }
    public bool IsReusable { get { return false; } }
}