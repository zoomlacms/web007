<%@ WebHandler Language="C#" Class="user" %>

using System;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using Newtonsoft.Json;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
public class user :API_Base,IHttpHandler {
    public void ProcessRequest (HttpContext context) {
        throw new Exception("接口关闭");
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Pragma, Cache-Control, Authorization ");
        HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
        retMod.retcode = M_APIResult.Failed;
        HttpPostedFile file = HttpContext.Current.Request.Files[0];
        try
        {
            switch (Action)
            {
                case "upload"://保存上传的文件
                    {
                        //SafeSC.SaveFile("/UploadFiles/", file, "test.jpg");
                    }
                    break;
                default:
                    {
                        retMod.retmsg = "file.[" + Action + "]接口不存在";
                    }
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; }
        RepToClient(retMod);
    }
    public bool IsReusable { get { return false; } }
}