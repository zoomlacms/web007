using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class wxpromo : System.Web.UI.Page
{
    int UserID { get { return DataConvert.CLng(Request["UserID"]); } }
    int BindID { get { return DataConvert.CLng(Request["BindID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (UserID < 1) { function.WriteErrMsg("推广链接错误,用户不存在"); }
        Session["WX_PUserID"] = UserID;
        Session["WX_BindID"] = BindID;
        Session["WX_R"] = Request["r"];
        int appid = DataConvert.CLng(Request.QueryString["appid"]);
        M_WX_APPID appMod = WxPayApi.Pay_GetByID(appid);
        string url = HttpUtility.UrlEncode(SiteConfig.SiteInfo.SiteUrl + "/wxback.aspx?appid=" + appid);
        Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appMod.APPID + "&redirect_uri=" + url + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect");
    }
}