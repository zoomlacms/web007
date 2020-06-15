using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User.Addon;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;

public partial class wxback : System.Web.UI.Page
{
    //进入该页即开始推广,自动创建用户,关联APPID
    protected void Page_Load(object sender, EventArgs e)
    {
        int AppID = DataConverter.CLng(Request.QueryString["appid"]);
        B_User buser = new B_User();
        B_UserAPP appBll = new B_UserAPP();
        int bindid = DataConverter.CLng(Session["WX_BindID"]);
        int puid = DataConverter.CLng(Session["WX_PUserID"]);
        string rurl = DataConvert.CStr(Session["WX_R"]);
        //if (puid < 1) { function.WriteErrMsg("推荐用户不存在"); }
        M_WX_APPID appMod = new M_WX_APPID();//自行置入缓存,使用时取出
        appMod = WxPayApi.Pay_GetByID(AppID);
		
		if (string.IsNullOrEmpty(rurl))
        {
            string url = HttpUtility.UrlEncode(SiteConfig.SiteInfo.SiteUrl + "/User/");
            rurl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appMod.APPID + "&redirect_uri=" + url + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
        }
		
        string code = Request["code"];
        string access_token = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appMod.APPID + "&secret=" + appMod.Secret + "&code=" + code + "&grant_type=authorization_code");
        JObject obj = (JObject)JsonConvert.DeserializeObject(access_token);
        JObject userobj = (JObject)JsonConvert.DeserializeObject(APIHelper.GetWebResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj["access_token"] + "&openid=" + obj["openid"] + "&lang=zh_CN"));        
        string openID = obj["openid"].ToString();
        M_UserAPP umod = appBll.SelModelByOpenID(openID, "wechat");
        M_UserInfo mu = new M_UserInfo();
        M_UserInfo pmu = new M_UserInfo();
        if (umod != null)
        {
            if (bindid > 0) { function.WriteErrMsg("您的公众号已绑定了用户["+ umod.UserID+ "]！","/User/"); }
            mu = buser.SelReturnModel(umod.UserID);
            pmu = buser.SelReturnModel(puid);
            if (DataConvert.CLng(mu.ParentUserID) <= 0 && pmu != null && pmu.UserID > 0)
            {
                mu.ParentUserID = pmu.UserID.ToString();
                buser.UpdateByID(mu);
            }
            
            buser.SetLoginState(mu);
            Response.Redirect(rurl);
        }
        else //新用户则注册
        {
            if (bindid > 0)
            {
                M_UserInfo bindMod = buser.GetUserByUserID(bindid);
                if (bindMod != null && bindMod.UserID > 0)
                {
                    umod = appBll.SelModelByUid(bindMod.UserID, "wechat");
                    if (umod != null)
                    {
                        function.WriteErrMsg("该用户已经绑定了微信号["+umod.ID+"]["+bindMod.UserID+"]，不能重复绑定！");
                    }
                    else
                    {
                        umod = new M_UserAPP();
                        umod.UserID = bindMod.UserID;
                        umod.SourcePlat = "wechat";
                        umod.OpenID = openID;
                        appBll.Insert(umod);
                        try
                        {
                            string vpath = SiteConfig.SiteOption.UploadDir + "User/" + bindMod.UserName + bindMod.UserID + "/wxheadimg.jpg";
                            HttpHelper.DownFile(userobj["headimgurl"].ToString(), vpath);
                            bindMod.UserFace = vpath;
                        }
                        catch (Exception ex)
                        {

                        }
                        bindMod.TrueName = userobj["nickname"].ToString();
                        buser.UpdateByID(bindMod);
                        buser.SetLoginState(bindMod);
                        function.WriteSuccessMsg("绑定成功，即将进入会员中心！", "/User/");
                    }
                }
            }
            else
            {
                umod = new M_UserAPP();
                mu.UserName = "wx" + DateTime.Now.ToString("yyyyMMddHHmmss") + function.GetRandomString(2).ToLower();
                mu.UserPwd = StringHelper.MD5(function.GetRandomString(6));
                mu.Email = function.GetRandomString(10) + "@wx.com";
                mu.Question = function.GetRandomString(5);
                mu.Answer = function.GetRandomString(5);
                mu.ParentUserID = puid.ToString();
                mu.TrueName = userobj["nickname"].ToString();
                try
                {
                    string vpath = SiteConfig.SiteOption.UploadDir + "User/" + mu.UserName + mu.UserID + "/wxheadimg.jpg";
                    HttpHelper.DownFile(userobj["headimgurl"].ToString(), vpath);
                    mu.UserFace = vpath;
                }
                catch (Exception ex)
                {

                }
                mu.GroupID = 1;
                mu.UserID = buser.Add(mu);

                umod.UserID = mu.UserID;
                umod.SourcePlat = "wechat";
                umod.OpenID = openID;
                appBll.Insert(umod);
                M_Uinfo mubase = new M_Uinfo();
                mubase.UserId = mu.UserID;
                buser.AddBase(mubase);
                //推广人得积分
                if (puid > 0)
                {
                    pmu = buser.SelReturnModel(puid);
                    if (!pmu.IsNull)
                    {
                        buser.ChangeVirtualMoney(pmu.UserID, new M_UserExpHis()
                        {
                            score = 20,
                            ScoreType = (int)M_UserExpHis.SType.Point,
                            detail = "新用户[" + mu.UserName + "]注册,获得推广积分:" + 20
                        });
                    }
                }
                buser.SetLoginState(mu);
                Response.Redirect(rurl);
            }
        }
    }
}