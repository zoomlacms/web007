using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.Common;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model;
using Newtonsoft.Json;
using System.Web.Security;
using ZoomLa.Components;
using ZoomLa.Model.Plat;
using ZoomLa.BLL.Plat;
using Newtonsoft.Json.Linq;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.BLL.User.Addon;
using ZoomLa.Model.User;

public partial class test_GetWiKiToken : System.Web.UI.Page
{
    B_WX_User wxuserBll = new B_WX_User();
    B_WX_ReplyMsg rpBll = new B_WX_ReplyMsg();
    B_WX_APPID appBll = new B_WX_APPID();
    B_User buser = new B_User();
    B_UserAPP uappBll = new B_UserAPP();
    M_UserAPP uappMod = new M_UserAPP();
    string baseUrl = SiteConfig.SiteInfo.SiteUrl;
    M_WX_APPID appMod = new M_WX_APPID();//自行置入缓存,使用时取出
    public int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        appMod = WxPayApi.Pay_GetByID(AppID);
        if (!IsPostBack)
        {
            string code = Request.QueryString["code"];
            string uid = Request.QueryString["uid"];
            if(!string.IsNullOrEmpty(uid))
            {
                M_UserInfo mu = buser.GetUserByUserID(DataConverter.CLng(uid));
                GetHeadImg(mu);
            }
            else
            {
                if (buser.CheckLogin())
                {
                    M_UserInfo mu = buser.GetLogin();
                    GetHeadImg(mu);
                }
                else
                {
                    if (Session["access_token"] == null || Session["openid"] == null)
                    {
                        string access_token = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appMod.APPID + "&secret=" + appMod.Secret + "&code=" + code + "&grant_type=authorization_code");
                        JObject obj = (JObject)JsonConvert.DeserializeObject(access_token);
                        string userstr = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj["access_token"] + "&openid=" + obj["openid"] + "&lang=zh_CN");
                        Session["access_token"] = obj["access_token"].ToString();
                        Session["openid"] = obj["openid"].ToString();
                        Response.Write(userstr);
                    }
                    else
                    {
                        string userstr = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + Session["access_token"].ToString() + "&openid=" + Session["openid"].ToString() + "&lang=zh_CN");
                        JObject obj = (JObject)JsonConvert.DeserializeObject(userstr);
                        if (obj["errcode"] != null)
                        {
                            string access_token = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appMod.APPID + "&secret=" + appMod.Secret + "&code=" + code + "&grant_type=authorization_code");
                            JObject obj1 = (JObject)JsonConvert.DeserializeObject(access_token);
                            userstr = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj1["access_token"] + "&openid=" + obj1["openid"] + "&lang=zh_CN");
                            Session["access_token"] = obj1["access_token"].ToString();
                            Session["openid"] = obj1["openid"].ToString();
                        }
                        Response.Write(userstr);
                    }
                }
            }
        }
    }
    protected void GetHeadImg(M_UserInfo mu)
    {
        int appid = 1;
        if (AppID > 0)
            appid = AppID;
        WxAPI wxapi = WxAPI.Code_Get(appid);

        if (mu == null || mu.IsNull || mu.UserID < 1)
        {
        }
        else
        {
            uappMod = uappBll.SelModelByUid(mu.UserID, "wechat");
            M_WX_User wuserMod = new M_WX_User();
            if(uappMod != null)
            {
                try
                {
                    wuserMod = wxapi.GetWxUserModel(uappMod.OpenID);
                    if (string.IsNullOrEmpty(wuserMod.Name))
                    {
                        wxapi.GetToken();
                        wuserMod = wxapi.GetWxUserModel(uappMod.OpenID);
                    }
                }
                catch (Exception ex)
                {
                    wxapi.GetToken();
                    wuserMod = wxapi.GetWxUserModel(uappMod.OpenID);
                    throw ex;
                }
                if (wuserMod != null)
                    Response.Write("{\"nickname\":\"" + wuserMod.Name + "\",\"headimgurl\":\"" + wuserMod.HeadImgUrl + "\"}");
            }
        }
    }
}