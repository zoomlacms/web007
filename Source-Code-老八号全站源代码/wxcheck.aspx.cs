using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User.Addon;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;

public partial class wxcheck : System.Web.UI.Page
{
    //进入该页即开始推广,自动创建用户,关联APPID
    protected void Page_Load(object sender, EventArgs e)
    {
        int AppID = DataConverter.CLng(Request.QueryString["appid"]);
        B_User buser = new B_User();
        B_UserAPP appBll = new B_UserAPP();
        int bindid = DataConverter.CLng(Session["WX_BindID"]);
        int puid = DataConverter.CLng(Session["WX_PUserID"]);
        string rurl = Request["r"];
        Session["WX_OpenID"] = "";

        buser.CheckLogin();
        //if (puid < 1) { function.WriteErrMsg("推荐用户不存在"); }
        M_WX_APPID appMod = new M_WX_APPID();//自行置入缓存,使用时取出
        appMod = WxPayApi.Pay_GetByID(AppID);
		
        string code = Request["code"];
        string access_token = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appMod.APPID + "&secret=" + appMod.Secret + "&code=" + code + "&grant_type=authorization_code");
        JObject obj = (JObject)JsonConvert.DeserializeObject(access_token);
        JObject userobj = (JObject)JsonConvert.DeserializeObject(APIHelper.GetWebResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj["access_token"] + "&openid=" + obj["openid"] + "&lang=zh_CN"));        
        string openID = obj["openid"].ToString();
		Session["WX_OpenID"]=openID;
		Response.Redirect(rurl);
    }
}